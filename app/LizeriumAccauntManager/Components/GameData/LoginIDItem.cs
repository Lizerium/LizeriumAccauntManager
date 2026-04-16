/*
 * Author: Nikolay Dvurechensky
 * Site: https://dvurechensky.pro/
 * Gmail: dvurechenskysoft@gmail.com
 * Last Updated: 16 апреля 2026 11:44:04
 * Version: 1.0.11
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
