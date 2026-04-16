/*
 * Author: Nikolay Dvurechensky
 * Site: https://dvurechensky.pro/
 * Gmail: dvurechenskysoft@gmail.com
 * Last Updated: 16 апреля 2026 11:44:04
 * Version: 1.0.11
 */

using System;
using System.Collections.Generic;
using System.Text;

namespace Root
{
    class FLDataFileException : Exception
    {
        public FLDataFileException(string msg) : base(msg) { }
    }
}
