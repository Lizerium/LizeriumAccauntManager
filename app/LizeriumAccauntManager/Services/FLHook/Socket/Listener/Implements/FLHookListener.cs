/*
 * Author: Nikolay Dvurechensky
 * Site: https://dvurechensky.pro/
 * Gmail: dvurechenskysoft@gmail.com
 * Last Updated: 09 апреля 2026 10:58:05
 * Version: 1.0.3
 */

using System;
using System.Collections.Generic;

namespace Root.Services;

public class FLHookListener : IFLHookListener
{
    private IDataBaseService DataBaseService { get; set; }
    private ILogRecorder LogRecorder { get; set; }


    public FLHookListener(IDataBaseService dataBaseService, ILogRecorder logRecorder) 
    { 
        this.DataBaseService = dataBaseService;
        this.LogRecorder = logRecorder;
    }

    /// <summary>
    /// Карта идентификаторов клиентов в accDir, чтобы мы могли пересканировать акки при переезде игроков.
    /// </summary>
    public Dictionary<int, string> ClientIdAccDirs = new Dictionary<int, string>();

    /// <summary>
    /// Если текущее время больше, чем указанное здесь, перепроверьте счет.
    /// </summary>
    public Dictionary<string, DateTime> PendingAccDirsToCheck { get; set; } = new Dictionary<string, DateTime>();

    /// <summary>
    /// Событие обновления UI
    /// </summary>
    public event Action<string, string[], string[], string>? HookEventProcessed;

    /// <summary>
    /// Receive an event from the flhook command socket.
    /// </summary>
    /// <param name="type">The command type from the keys[0] field</param>
    /// <param name="keys">An array of parameter keys.</param>
    /// <param name="values">An array of parameter values.</param>
    /// <param name="eventLine">The unparsed event line</param>
    public void ReceiveFLHookEvent(string type, string[] keys, string[] values, string eventLine)
    {
        try
        {
            if (type == "login")
            {
                int id = Convert.ToInt32(values[3]);
                string charname = values[1];
                string accDir = values[2];
                string ip = values[4];

                ClientIdAccDirs[id] = accDir;

                // Record the IP address information.
                DataBaseService.Model.AddIPListRow(accDir, ip, DateTime.Now);

                // Rescan the account to ensure the database is in an accurate state.
                PendingAccDirsToCheck[accDir] = DateTime.Now.AddSeconds(5);
            }
            else if (type == "disconnect"
                || type == "baseenter"
                || type == "launch"
                || type == "spawn")
            {
                int id = Convert.ToInt32(values[2]);
                if (ClientIdAccDirs.ContainsKey(id))
                {
                    string accDir = ClientIdAccDirs[id];
                    // Rescan the account to ensure the database is in an accurate state.
                    PendingAccDirsToCheck[accDir] = DateTime.Now.AddSeconds(5);
                }
            }

            // 🔔 Сообщаем наружу, что пришло событие (если нужно что-то в UI обновить)
            HookEventProcessed?.Invoke(type, keys, values, eventLine);
        }
        catch (Exception ex)
        {
            // логируем ошибку
            LogRecorder.AddLog("Error '" + ex.Message + "' when processing flhook event '" + eventLine + "'");
        }
    }
}
