/*
 * Author: Nikolay Dvurechensky
 * Site: https://dvurechensky.pro/
 * Gmail: dvurechenskysoft@gmail.com
 * Last Updated: 02 сентября 2026 06:55:30
 * Version: 1.0.151
 */

using System;

namespace Root.Components
{
    public class StatPlayer
    {
        public string Name { get; set; }
        public string OnLineTime { get; set; }
        public uint Money { get; set; }
        public uint Rank { get; set; }
        public DateTime LastOnLine { get; set; }
        public uint OnLineSecs { get; set; }
    }
}
