/*
 * Author: Nikolay Dvurechensky
 * Site: https://dvurechensky.pro/
 * Gmail: dvurechenskysoft@gmail.com
 * Last Updated: 08 мая 2026 06:52:32
 * Version: 1.0.34
 */

using System;
using System.Text;
using System.Net.Sockets;
using System.Threading;

namespace Root.Services
{
    /// <summary>
    /// Сервис приёма сообщений FLHook
    /// </summary>
    public class FLHookEventSocketService : IFLHookEventSocketService
    {
        /// If true then this socket will enter event mode
        IFLHookListener eventListener = null;

        /// <summary>
        /// Thread for receiving event mode data.
        /// </summary>
        Thread receiveEventThread = null;

        /// <summary>
        /// Thread for checker the configuration and resetting
        /// the socket if necessary\.
        /// </summary>
        Thread cfgCheckerThread = null;

        /// <summary>
        /// The logging interface to report diagnostic messages on.
        /// </summary>
        ILogRecorder log = null;

        /// <summary>
        /// The receive buffer.
        /// </summary>
        string stReplyBuf = "";

        /// <summary>
        /// If true write debug log entries
        /// </summary>
        decimal debug = 0;

        /// <summary>
        /// The connection to FLHook
        /// </summary>
        TcpClient rxSocket = null;

        /// <summary>
        /// The stream to read/write on the socket.
        /// </summary>
        NetworkStream rxStream = null;

        /// <summary>
        /// Synchronisation object.
        /// </summary>
        Object locker = new Object();

        /// <summary>
        /// Start the log
        /// </summary>
        /// <param name="eventListener"></param>
        public FLHookEventSocketService(IFLHookListener eventListener, ILogRecorder log)
        {
            this.eventListener = eventListener;
            this.log = log;
            if (this.eventListener != null)
            {
                receiveEventThread = new Thread(new ThreadStart(ReceiveEventThread));
                receiveEventThread.Name = "FLEventReceive";
                receiveEventThread.IsBackground = true;
                receiveEventThread.Start();

                cfgCheckerThread = new Thread(new ThreadStart(ConfigurationCheckerThread));
                cfgCheckerThread.Name = "FLEventReceiveCfgChecker";
                cfgCheckerThread.IsBackground = true;
                cfgCheckerThread.Start();
            }
        }

        ~FLHookEventSocketService()
        {
            Shutdown();
        }

        /// <summary>
        /// Shutdown the socket.
        /// </summary>
        public void Shutdown()
        {
            eventListener = null;

            if (cfgCheckerThread != null)
            {
                cfgCheckerThread.Abort();
                cfgCheckerThread = null;
            }

            // Close the socket to terminate any blocking reads,
            // terminate the thread and wait for it to shutdown.
            if (receiveEventThread != null)
            {
                CloseSocket();
                receiveEventThread.Abort();
                receiveEventThread = null;
            }
        }

        /// <summary>
        /// Close the socket;
        /// </summary>
        private void CloseSocket()
        {
            lock (locker)
            {
                if (rxSocket != null)
                {
                    rxSocket.Close();
                    rxSocket = null;
                }
            }
        }

        /// <summary>
        /// Send a command to the network stream to flhook.
        /// </summary>
        /// <param name="stream">The stream to write to.</param>
        /// <param name="command">The command to send.</param>
        private void SendCommand(NetworkStream stream, string command, bool unicode)
        {
            byte[] txBuf;
            if (unicode)
                txBuf = Encoding.Unicode.GetBytes(command + "\n");
            else
                txBuf = Encoding.ASCII.GetBytes(command + "\n");
            if (debug > 1) log.AddLog("flhook tx: " + command.Trim());
            stream.Write(txBuf, 0, txBuf.Length);
        }

        /// <summary>
        /// Waits for a reply line from flhook. Horrible socket polling
        /// but meh, it works.
        /// </summary>
        /// <param name="stream">The stream to read from.</param>
        /// <returns>The reply</returns>
        private string ReceiveReply(NetworkStream stream, bool unicode)
        {
            // Receive the response.
            byte[] rxBuf = new byte[1024];

            DateTime abortReadTime = DateTime.Now.AddSeconds(5);
            while (true)
            {
                // If we've found a line break then return the line.
                int endOfLine = stReplyBuf.IndexOf("\n");
                if (endOfLine >= 0)
                {
                    char[] delims = { '\n', '\r' };
                    string reply = stReplyBuf.Substring(0, endOfLine).TrimEnd(delims);
                    stReplyBuf = stReplyBuf.Remove(0, endOfLine + 1);
                    return reply;
                }

                // Stop waiting for a reply if we have waited too long.
                if (DateTime.Now > abortReadTime)
                    return null;

                // Otherwise read some bytes from the stream.
                int rxBytes = stream.Read(rxBuf, 0, rxBuf.Length);
                if (rxBytes > 0)
                {
                    string reply;
                    if (unicode)
                        reply = Encoding.Default.GetString(rxBuf, 0, rxBytes);
                    else
                        reply = Encoding.Default.GetString(rxBuf, 0, rxBytes);
                    if (debug > 1) log.AddLog("flhook rx: " + reply.Trim());
                    stReplyBuf += reply;
                }
            }
        }

        /// <summary>
        /// Thread for checking the configuration and resetting
        /// the socket if necessary.
        /// </summary>
        private void ConfigurationCheckerThread()
        {
            string server = "";
            int port = 0;
            string login = "";
            bool unicode = false;

            while (true)
            {
                lock (AppSettings.Default)
                {
                    debug = AppSettings.Default.setDebug;

                    // Check for settings changes.
                    bool changed = false;
                    if (server != AppSettings.Default.setFLHookIP)
                        changed = true;
                    else if (port != Convert.ToInt32(AppSettings.Default.setFLHookPort))
                        changed = true;
                    else if (login != AppSettings.Default.setFLHookPassword)
                        changed = true;
                    else if (unicode != AppSettings.Default.setFLHookUnicode)
                        changed = true;

                    // Notify the receiver to reset it's configuration
                    if (changed && receiveEventThread != null)
                    {
                        server = AppSettings.Default.setFLHookIP;
                        port = Convert.ToInt32(AppSettings.Default.setFLHookPort);
                        login = AppSettings.Default.setFLHookPassword;
                        unicode = AppSettings.Default.setFLHookUnicode;
                        debug = AppSettings.Default.setDebug;

                        // Close the socket to terminate any blocking read and
                        // interrupt the thread.
                        receiveEventThread.Interrupt();
                        CloseSocket();
                    }
                }

                Thread.Sleep(10000);
            }
        }

        /// <summary>
        /// The event receiver thread. This thread never exits.
        /// </summary>
        private void ReceiveEventThread()
        {
            string server = "";
            int port = 0;
            string login = "";
            bool unicode = false;
            while (true)
            {
                try
                {
                    Thread.Sleep(10000);

                    lock (AppSettings.Default)
                    {
                        server = AppSettings.Default.setFLHookIP;
                        port = Convert.ToInt32(AppSettings.Default.setFLHookPort);
                        login = AppSettings.Default.setFLHookPassword;
                        unicode = AppSettings.Default.setFLHookUnicode;
                        debug = AppSettings.Default.setDebug;
                    }

                    // Do nothing if flhook is disabled.
                    if (port == 0)
                        break;

                    // Establish the socket connection
                    stReplyBuf = "";

                    CloseSocket();
                    lock (locker)
                    {
                        rxSocket = new TcpClient(server, port);
                        rxSocket.ReceiveTimeout = 5000;
                        rxStream = rxSocket.GetStream();
                    }

                    if (debug > 0) log.AddLog("flhook: opened connection");

                    // Wait for the welcome message.
                    if (ReceiveReply(rxStream, unicode) != "Welcome to FLHack, please authenticate")
                        throw new Exception("no login message");

                    // Send the pass and wait for OK
                    SendCommand(rxStream, String.Format("pass {0}", login), unicode);
                    if (ReceiveReply(rxStream, unicode) != "OK")
                        throw new Exception("no pass ok message");

                    // ASk hook to enter event mode.
                    SendCommand(rxStream, "eventmode", unicode);
                    if (ReceiveReply(rxStream, unicode) != "OK")
                        throw new Exception("no eventmode ok message");

                    if (debug > 0) log.AddLog("flhook: login complete");

                    // Loop receiving events
                    byte[] rxBuf = new byte[1000];
                    rxSocket.ReceiveTimeout = 0;

                    // Otherwise read some bytes from the stream.
                    string reply = ReceiveReply(rxStream, unicode);
                    while (reply != null)
                    {
                        string[] keys;
                        string[] values;
                        FLHookSocketService.ParseLine(reply, out keys, out values);
                        eventListener.ReceiveFLHookEvent(keys[0], keys, values, reply);

                        reply = ReceiveReply(rxSocket.GetStream(), unicode);
                    }
                }
                catch (ThreadAbortException)
                {
                    if (debug > 0) log.AddLog("flhook: shutdown connection");
                }
                catch (ThreadInterruptedException)
                {
                    if (debug > 0) log.AddLog("flhook: configuration reset");
                }
                catch (Exception ex)
                {
                    if (debug > 0) log.AddLog("flhook: '" + ex.Message + "'");
                }
                finally
                {
                    if (debug > 0) log.AddLog("flhook: closing connection");
                    stReplyBuf = "";
                    try
                    {
                        CloseSocket();
                    }
                    catch { }
                }
            }
        }
    }
}