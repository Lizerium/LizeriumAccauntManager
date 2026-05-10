/*
 * Author: Nikolay Dvurechensky
 * Site: https://dvurechensky.pro/
 * Gmail: dvurechenskysoft@gmail.com
 * Last Updated: 10 мая 2026 07:42:56
 * Version: 1.0.36
 */

using System;

namespace Root.Components
{
    public class CharacterItem
    {
        public string CharPath { get; set; }
        public string AccDir { get; set; }
        public string AccID { get; set; }
        public string CharName { get; set; }
        public bool IsDeleted { get; set; }
        public string Location { get; set; }
        public string Ship { get; set; }
        public uint Money { get; set; }
        public uint Rank { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
        public uint OnLineSecs { get; set; }
        public DateTime LastOnLine { get; set; }
    }
}
