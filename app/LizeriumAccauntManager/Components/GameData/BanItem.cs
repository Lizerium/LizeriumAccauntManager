/*
 * Author: Nikolay Dvurechensky
 * Site: https://dvurechensky.pro/
 * Gmail: dvurechenskysoft@gmail.com
 * Last Updated: 13 июля 2026 06:53:00
 * Version: 1.0.100
 */

using System;

namespace Root.Components
{
    public class BanItem
    {
        public string AccDir { get; set; }
        public string AccID { get; set; }
        public string BanReason { get; set; }
        public DateTime BanStart { get; set; }
        public DateTime BanEnd { get; set; }
    }
}
