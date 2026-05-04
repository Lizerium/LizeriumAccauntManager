/*
 * Author: Nikolay Dvurechensky
 * Site: https://dvurechensky.pro/
 * Gmail: dvurechenskysoft@gmail.com
 * Last Updated: 04 мая 2026 06:53:07
 * Version: 1.0.30
 */

using Root.Components;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using Microsoft.Data.Sqlite;

namespace Root.Services
{
    public interface IDataBaseService : IDisposable
    {
        /// <summary>
        /// Модель хранения и обработки данных
        /// </summary>
        DataModel Model { get; set; }

        #region Base

        /// <summary>
        /// Support for the IDisposable interface.
        /// </summary>
        void Dispose();

        /// <summary>
        /// При необходимости откройте соединение с базой данных.
        /// </summary>
        /// <returns>SqliteConnection</returns>
        Task<SqliteConnection> GetConnectionAsync();

        #endregion

        /// <summary>
        /// Обновление генеральной таблицу статистики.
        /// </summary>
        Task UpdateGeneralTableStatistics();

        /// <summary>
        /// Обновление статистики персонажей
        /// </summary>
        /// <param name="gameData">FLGameData</param>
        Task UpdateCharactersStatistic(IFLGameDataService gameData);

        /// <summary>
        /// Создает таблицу истории за вчерашний день, копируя в нее информацию
        /// обо всех активных в данный момент персонажах.
        /// </summary>
        Task UpdateActiveCharacterHistory();
        #region Exists

        /// <summary>
        /// Возвращает значение true, если таблица CharacterHistory существует в указанный день.
        /// </summary>
        /// <param name="date">DateTime</param>
        /// <returns>bool</returns>
        Task<bool> CharacterHistoryExists(DateTime date);

        /// <summary>
        /// Проверяет, существует ли таблица истории за вчерашний день. 
        /// Если нет, то возвращает true, в противном случае false.
        /// </summary>
        /// <returns>bool</returns>
        Task<bool> ActiveCharacterHistoryExist();

        #endregion

        #region Insert or Update

        Task InsertOrUpdateGameInfoChanges(ILogRecorder log, DataModel model);

        /// <summary>
        /// Commit pending changes on the CharacterList, BanList, IPList and LoginID
        /// </summary>
        Task InsertOrUpdatePlayerInfoChanges(DataModel tempDataStore, ILogRecorder log);

        /// <summary>
        /// Добавить запись об авторизации пользователя
        /// </summary>
        /// <param name="item">LoginIDItem</param>
        Task InsertOrUpdateLoginIdRecord(LoginIDItem item);

        /// <summary>
        /// Добавить запись об IP пользователя
        /// </summary>
        /// <param name="item">IPItem</param>
        Task InsertOrUpdateIPRecord(IPItem item);

        /// <summary>
        /// Добавить запись о персонаже пользователя
        /// </summary>
        /// <param name="item">CharacterItem</param>
        Task InsertOrUpdateCharacter(CharacterItem item);

        /// <summary>
        /// Добавление забаненного пользователя
        /// </summary>
        /// <param name="item">BanItem</param>
        Task InsertOrUpdateBanRecordToDatabase(BanItem item);

        /// <summary>
        /// Insert into the general statistics table.
        /// </summary>
        /// <param name="description"></param>
        /// <param name="SQL"></param>
        Task UpdateGeneralStatistics(string description, string SQL);

        #endregion

        #region Get

        /// <summary>
        /// Получение актуальных игровых данных
        /// </summary>
        /// <param name="log">LogRecorderInterface</param>
        /// <param name="model">DataModel</param>
        /// <returns>DataModel</returns>
        Task GetGameData(ILogRecorder log, DataModel model);

        /// <summary>
        /// Get all character records
        /// </summary>
        Task GetCharacterList(DataModel model);

        /// <summary>
        /// Get the characters for the specified account
        /// </summary>
        /// <param name="accDir">The account dir</param>
        Task GetCharacterListByAccDir(string accDir);

        /// <summary>
        /// Get all ban list records.
        /// </summary>
        /// <param name="model">The ban list.</param>
        Task GetBanList(DataModel model);

        /// <summary>
        /// Get the ban list for the specified account
        /// </summary>
        Task GetBanListByAccDir(string accDir);

        /// <summary>
        /// Get the IPList for the specified account
        /// </summary>
        /// <param name="accDir">The account dir</param>
        Task<List<IPItem>> GetIPListByAccDir(string accDir);

        /// <summary>
        /// Get the IPList for the specified array of IP addresses
        /// </summary>
        /// <param name="IPs">The IPs to search for.</param>
        Task<List<IPItem>> GetIPListByIP(string[] IPs);

        /// <summary>
        /// Get the IPList for the specified IP and fragment of IP.
        /// </summary>
        /// <param name="ipFrag">The IPs to search for.</param>
        Task<List<IPItem>> GetIPListByIP(string ipFrag);

        /// <summary>
        /// Get the LoginIDList for the specified account
        /// </summary>
        /// <param name="accDir">The account dir</param>
        Task<List<LoginIDItem>> GetLoginIDListByAccDir(string accDir);

        /// <summary>
        /// Get the LoginIDList for the specified loginID
        /// </summary>
        /// <param name="loginIDs">The login ID to search for.</param>
        Task<List<LoginIDItem>> GetLoginIDListByLoginID(string[] loginIDs);

        /// <summary>
        /// Get all statistics records
        /// </summary>
        Task<List<GeneralStatistics>> GetGeneralStatisticsTable();

        /// <summary>
        /// Get all character history records for the specified date. Return true if the history
        /// table exists, otherwise return false.
        /// </summary>
        Task<bool> GetCharacterHistory(DateTime date);

        #endregion

        #region Delete

        /// <summary>
        /// Удаление забаненного пользователя
        /// </summary>
        /// <param name="accDir">accDir</param>
        Task DeleteBanRecordFromDatabase(string accDir);

        /// <summary>
        /// Удаляет старые таблицы истории на основе ретроспективного горизонта в днях
        /// </summary>
        /// <param name="horizon">int</param>
        Task CleanUpCharacterHistory(int horizon);

        /// <summary>
        /// Очистка игровых данных
        /// </summary>
        /// <param name="log">LogRecorderInterface</param>
        Task ClearGameData(ILogRecorder log);
        #endregion
    }
}
