/*
 * Author: Nikolay Dvurechensky
 * Site: https://dvurechensky.pro/
 * Gmail: dvurechenskysoft@gmail.com
 * Last Updated: 29 июня 2026 06:53:38
 * Version: 1.0.86
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
