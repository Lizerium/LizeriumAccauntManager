/*
 * Author: Nikolay Dvurechensky
 * Site: https://dvurechensky.pro/
 * Gmail: dvurechenskysoft@gmail.com
 * Last Updated: 14 октября 2026 11:02:58
 * Version: 1.0.192
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
