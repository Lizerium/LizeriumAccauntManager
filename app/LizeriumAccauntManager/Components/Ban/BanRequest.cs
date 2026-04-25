/*
 * Author: Nikolay Dvurechensky
 * Site: https://dvurechensky.pro/
 * Gmail: dvurechenskysoft@gmail.com
 * Last Updated: 25 апреля 2026 08:11:28
 * Version: 1.0.21
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
