/*
 * Author: Nikolay Dvurechensky
 * Site: https://dvurechensky.pro/
 * Gmail: dvurechenskysoft@gmail.com
 * Last Updated: 26 октября 2026 12:00:00
 * Version: 1.0.204
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
