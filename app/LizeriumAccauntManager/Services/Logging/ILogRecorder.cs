/*
 * Author: Nikolay Dvurechensky
 * Site: https://dvurechensky.pro/
 * Gmail: dvurechenskysoft@gmail.com
 * Last Updated: 12 июля 2026 06:53:06
 * Version: 1.0.99
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
