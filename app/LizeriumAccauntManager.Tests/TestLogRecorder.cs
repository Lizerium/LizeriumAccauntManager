/*
 * Author: Nikolay Dvurechensky
 * Site: https://dvurechensky.pro/
 * Gmail: dvurechenskysoft@gmail.com
 * Last Updated: 30 апреля 2026 09:20:20
 * Version: 1.0.26
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
