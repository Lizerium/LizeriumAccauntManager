/*
 * Author: Nikolay Dvurechensky
 * Site: https://dvurechensky.pro/
 * Gmail: dvurechenskysoft@gmail.com
 * Last Updated: 08 апреля 2026 14:27:40
 * Version: 1.0.2
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
