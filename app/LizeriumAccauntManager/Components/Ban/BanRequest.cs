/*
 * Author: Nikolay Dvurechensky
 * Site: https://dvurechensky.pro/
 * Gmail: dvurechenskysoft@gmail.com
 * Last Updated: 03 июня 2026 15:17:16
 * Version: 1.0.60
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
