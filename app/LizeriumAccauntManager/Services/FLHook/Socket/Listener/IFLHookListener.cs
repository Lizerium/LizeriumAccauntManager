/*
 * Author: Nikolay Dvurechensky
 * Site: https://dvurechensky.pro/
 * Gmail: dvurechenskysoft@gmail.com
 * Last Updated: 11 апреля 2026 13:39:27
 * Version: 1.0.5
 */

using System;
using System.Collections.Generic;

namespace Root.Services;

public interface IFLHookListener
{
    /// <summary>
    /// Получите событие из сокета команды flhook.
    /// </summary>
    /// <param name="type">Тип команды из поля keys[0]</param>
    /// <param name="keys">Массив ключей параметров.</param>
    /// <param name="values">Массив значений параметров.</param>
    /// <param name="eventLine">Неразобранная строка события</param>
    void ReceiveFLHookEvent(string type, string[] keys, string[] values, string eventLine);

    /// <summary>
    /// Событие обновления UI
    /// </summary>
    event Action<string, string[], string[], string>? HookEventProcessed;

    /// <summary>
    /// Если текущее время больше, чем указанное здесь, перепроверьте счет.
    /// </summary>
    Dictionary<string, DateTime> PendingAccDirsToCheck { get; set; }
}
