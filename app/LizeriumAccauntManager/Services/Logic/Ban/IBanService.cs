/*
 * Author: Nikolay Dvurechensky
 * Site: https://dvurechensky.pro/
 * Gmail: dvurechenskysoft@gmail.com
 * Last Updated: 03 июля 2026 08:30:08
 * Version: 1.0.90
 */

using System;

using Root.Components;

namespace Root.Services
{
    public interface IBanService
    {
        /// <summary>
        /// Сообщения от процессов
        /// </summary>
        Action<string> SendMessageAction { get; set; }

        /// <summary>
        /// Заблокировать указанный аккаунт.
        /// </summary>
        /// <param name="request">Информация о бане</param>
        void BanAccount(BanRequest request);

        /// <summary>
        /// Разблокировать указанный аккаунт.
        /// </summary>
        /// <param name="accDir">Путь к учетной записи</param>
        void UnbanAccount(string accDir);
    }
}
