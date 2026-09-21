/*
 * Author: Nikolay Dvurechensky
 * Site: https://dvurechensky.pro/
 * Gmail: dvurechenskysoft@gmail.com
 * Last Updated: 21 сентября 2026 09:35:21
 * Version: 1.0.170
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
