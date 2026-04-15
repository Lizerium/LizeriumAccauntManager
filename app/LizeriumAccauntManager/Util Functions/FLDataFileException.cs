/*
 * Author: Nikolay Dvurechensky
 * Site: https://dvurechensky.pro/
 * Gmail: dvurechenskysoft@gmail.com
 * Last Updated: 15 апреля 2026 06:52:25
 * Version: 1.0.10
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
