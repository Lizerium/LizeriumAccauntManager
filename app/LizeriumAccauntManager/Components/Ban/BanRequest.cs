/*
 * Author: Nikolay Dvurechensky
 * Site: https://dvurechensky.pro/
 * Gmail: dvurechenskysoft@gmail.com
 * Last Updated: 19 мая 2026 10:16:19
 * Version: 1.0.45
 */

using System;

namespace Root.Components
{
    public struct BanRequest
    {
        public string AccDir { get; set; }
        public string AaccID { get; set; }
        public string BanReason { get; set; }
        public DateTime BanStart { get; set; }
        public DateTime BanEnd { get; set; }
    }
}
