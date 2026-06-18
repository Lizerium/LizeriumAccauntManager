/*
 * Author: Nikolay Dvurechensky
 * Site: https://dvurechensky.pro/
 * Gmail: dvurechenskysoft@gmail.com
 * Last Updated: 18 июня 2026 06:53:05
 * Version: 1.0.75
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
