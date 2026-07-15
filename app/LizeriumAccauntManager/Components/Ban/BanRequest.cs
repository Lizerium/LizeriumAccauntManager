/*
 * Author: Nikolay Dvurechensky
 * Site: https://dvurechensky.pro/
 * Gmail: dvurechenskysoft@gmail.com
 * Last Updated: 15 июля 2026 11:54:48
 * Version: 1.0.102
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
