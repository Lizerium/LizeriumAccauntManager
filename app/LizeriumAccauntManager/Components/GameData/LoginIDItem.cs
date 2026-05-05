/*
 * Author: Nikolay Dvurechensky
 * Site: https://dvurechensky.pro/
 * Gmail: dvurechenskysoft@gmail.com
 * Last Updated: 05 мая 2026 07:02:03
 * Version: 1.0.31
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
