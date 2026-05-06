/*
 * Author: Nikolay Dvurechensky
 * Site: https://dvurechensky.pro/
 * Gmail: dvurechenskysoft@gmail.com
 * Last Updated: 06 мая 2026 10:30:21
 * Version: 1.0.32
 */

using System;

/*  The following notes on bin file encoding were written by Christopher Wellons <ccw129@psu.edu>
    and were taken from http://www.nullprogram.com/projects/bini/

    The bini decode implementaton in the following code is my own (and so is probably buggy!)
  
    A BINI file has two segments: a data segment and a string segment, which contains the string table.
    +------------+
    |    data    |
    |------------|
    |   string   |
    |    table   |
    +------------+
    The string table is an argz vector: a contiguous block of memory with strings separated by
     null-characters (/0). The data section points to these strings. All values are stored as little endian.

    A BINI file begins with a 12-byte header made up of 3 4-byte segments,

    header {
      dword "BINI"
      dword version
      dword str_table
    }

    The first 4 bytes identify the file as a BINI file with those exact 4 ASCII letters. The second
    4 bytes are always equal to the number 1. This is believed to be a version number. The last 4 bytes
    is the string table offset from the beginning of the file.

    The first 4 bytes after the header is the first section. A section contains two 2-byte values,

    section {
      word string_offset
      word number_of_entries
    }

    The string offset is the offset from the beginning of the string table. This string is the name of
    that section. The second word is the number of entries in this section. Note that the number of 
    sections is not listed anywhere, so this information can only be found by iterating though the
    entire data segment.

    Following this section information is a 3-byte entry,

    entry {
      word string_offset
      byte number of values
    }

    This is data is setup just like sections with the string offset being the name of the entry,
    and following it is the same number of values as indicated,

    value {
      byte  type
      dword data
    }

    A value is 5 bytes. The first byte describes the type, of which there are three,

         1 - integer
         2 - float
         3 - a string table offset

    The data dword is of the indicated type. The next entry after all of the values for this entry,
    the next entry (if there is one). After the last entry, we may either find ourselves at the string
    table, meaning that the parsing is complete, or we are at another new section, and we start over again. 
*/

namespace Root.Services
{
    public class FLDataFileSetting
    {
        public FLDataFileSetting(string sectionName, string settingName, string desc, object[] values)
        {
            this._sectionName = sectionName;
            this._settingName = settingName;
            this._desc = desc;
            this._values = values;
        }

        private string _desc = "";
        private string _sectionName = "";
        private string _settingName = "";
        private object[] _values = new object[0];

        public string desc { get { return _desc; } }
        public string sectionName { get { return _sectionName; } }
        public string settingName { get { return _settingName; } }
        public object[] values { get { return _values; } set { _values = value; } }

        public uint UInt(int index) { return Convert.ToUInt32(values[index]); }
        public string Str(int index) { return Convert.ToString(values[index]); }
        public string UniStr(int index) { return FLUtility.DecodeUnicodeHex(Convert.ToString(values[index])); }
        public float Float(int index) { return Convert.ToSingle(values[index], System.Globalization.NumberFormatInfo.InvariantInfo); }
        public int NumValues() { return values.Length; }
    }
}
