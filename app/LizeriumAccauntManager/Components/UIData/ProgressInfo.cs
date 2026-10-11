/*
 * Author: Nikolay Dvurechensky
 * Site: https://dvurechensky.pro/
 * Gmail: dvurechenskysoft@gmail.com
 * Last Updated: 11 октября 2026 11:52:28
 * Version: 1.0.190
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
