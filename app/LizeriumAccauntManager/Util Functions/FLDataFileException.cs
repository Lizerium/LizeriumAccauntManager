/*
 * Author: Nikolay Dvurechensky
 * Site: https://dvurechensky.pro/
 * Gmail: dvurechenskysoft@gmail.com
 * Last Updated: 23 апреля 2026 06:53:22
 * Version: 1.0.19
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
