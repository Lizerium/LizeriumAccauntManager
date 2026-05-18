/*
 * Author: Nikolay Dvurechensky
 * Site: https://dvurechensky.pro/
 * Gmail: dvurechenskysoft@gmail.com
 * Last Updated: 18 мая 2026 12:50:00
 * Version: 1.0.44
 */

using Root.Services;

namespace DSAccountManager.Tests
{
    public class TestLogRecorder : ILogRecorder
    {
        /// <summary>
        /// Сообщения тестового лога
        /// </summary>
        public List<string> Messages { get; set; } = new List<string>();

        public void AddLog(string entry)
        {
            Messages.Add(entry);
            Console.WriteLine(entry);
        }
    }
}
