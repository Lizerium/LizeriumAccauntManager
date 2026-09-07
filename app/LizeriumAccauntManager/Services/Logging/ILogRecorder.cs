/*
 * Author: Nikolay Dvurechensky
 * Site: https://dvurechensky.pro/
 * Gmail: dvurechenskysoft@gmail.com
 * Last Updated: 07 сентября 2026 08:16:58
 * Version: 1.0.156
 */

namespace Root.Services
{
    public interface ILogRecorder
    {
        /// <summary>
        /// Add an entry to the diagnostics log.
        /// </summary>
        /// <param name="entry">The log entry</param>
        void AddLog(string entry);
    }
}
