/*
 * Author: Nikolay Dvurechensky
 * Site: https://dvurechensky.pro/
 * Gmail: dvurechenskysoft@gmail.com
 * Last Updated: 16 июня 2026 06:53:05
 * Version: 1.0.73
 */

namespace Root.Components
{
    public struct ProgressInfo
    {
        public int Percent {  get; }
        public string Message { get; }

        public ProgressInfo(int percent, string message)
        {
            Percent = percent;
            Message = message;
        }
    }
}
