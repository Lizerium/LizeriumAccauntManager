/*
 * Author: Nikolay Dvurechensky
 * Site: https://dvurechensky.pro/
 * Gmail: dvurechenskysoft@gmail.com
 * Last Updated: 17 мая 2026 11:11:45
 * Version: 1.0.43
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
