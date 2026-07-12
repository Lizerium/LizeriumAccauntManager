/*
 * Author: Nikolay Dvurechensky
 * Site: https://dvurechensky.pro/
 * Gmail: dvurechenskysoft@gmail.com
 * Last Updated: 12 июля 2026 06:53:06
 * Version: 1.0.99
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
