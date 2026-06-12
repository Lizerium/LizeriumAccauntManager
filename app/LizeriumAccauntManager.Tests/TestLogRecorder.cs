/*
 * Author: Nikolay Dvurechensky
 * Site: https://dvurechensky.pro/
 * Gmail: dvurechenskysoft@gmail.com
 * Last Updated: 12 июня 2026 06:53:12
 * Version: 1.0.69
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
