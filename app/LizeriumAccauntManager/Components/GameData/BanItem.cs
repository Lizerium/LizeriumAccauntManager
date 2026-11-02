/*
 * Author: Nikolay Dvurechensky
 * Site: https://dvurechensky.pro/
 * Gmail: dvurechenskysoft@gmail.com
 * Last Updated: 02 ноября 2026 09:39:13
 * Version: 1.0.211
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
