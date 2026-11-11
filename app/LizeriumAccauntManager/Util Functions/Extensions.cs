/*
 * Author: Nikolay Dvurechensky
 * Site: https://dvurechensky.pro/
 * Gmail: dvurechenskysoft@gmail.com
 * Last Updated: 11 ноября 2026 09:16:03
 * Version: 1.0.220
 */

using System;

public class Extensions
{
    /// <summary>
    /// Имя таблицы истории списка символов на указанную дату.
    /// </summary>
    /// <param name="date">DateTime</param>
    /// <returns>string</returns>
    public static string GetCharacterHistoryName(DateTime date)
    {
        return String.Format("CharacterList_{0:yyyyMMdd}", date);
    }

    /// <summary>
    /// Escape SQL text.
    /// </summary>
    /// <param name="inputSQL"></param>
    /// <returns></returns>
    public static string SafeSqlLiteral(string inputSQL)
    {
        return inputSQL.Replace("'", "''");
    }
}