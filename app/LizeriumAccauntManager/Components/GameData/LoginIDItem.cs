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
    public class LoginIDItem
    {
        public string AccDir {  get; set; }
        public string LoginID {  get; set; }
        public DateTime AccessTime { get; set; }

        public bool IsValid => !string.IsNullOrEmpty(AccDir);
    }
}
