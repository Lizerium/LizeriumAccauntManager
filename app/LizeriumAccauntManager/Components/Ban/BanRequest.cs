/*
 * Author: Nikolay Dvurechensky
 * Site: https://dvurechensky.pro/
 * Gmail: dvurechenskysoft@gmail.com
 * Last Updated: 04 августа 2026 06:53:34
 * Version: 1.0.122
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
