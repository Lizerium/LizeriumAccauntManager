using System;
using System.Collections.Generic;
using System.Data;
using Root.Components;
using Dapper;
using System.Linq;
using Root.Components;
using System.Threading.Tasks;
using Microsoft.Data.Sqlite;
using System.Threading;

namespace Root.Services;

public class DataBaseService : IDataBaseService 
{
    /// <summary>
    /// Модель хранения и обработки данных
    /// </summary>
    public DataModel Model { get; set; } = new DataModel();

    /// <summary>
    /// Подключение к базе данных.
    /// </summary>
    private SqliteConnection conn;

    /// <summary>
    /// Строка подключения к базе данных
    /// </summary>
    private string ConnectString { get; set; } = "Data Source=" + AppSettings.Default.setAccountDir + "\\dsam.db;";

    public DataBaseService()
    {
    }

    ~DataBaseService()
    {
        Dispose();
    }

    #region Base

    /// <summary>
    /// Support for the IDisposable interface.
    /// </summary>
    public void Dispose()
    {
        try
        {
            if (conn != null)
            {
                conn.Dispose();  // Корректно освобождаем ресурс
                conn = null;
            }
        }
        catch { }
    }

    private async Task CreateIndexIfNotExists(SqliteConnection conn, string indexName, string tableName, string columnName)
    {
        await GetConnectionAsync();
        using var transaction = conn.BeginTransaction();

        try
        {
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText = $"CREATE INDEX IF NOT EXISTS {indexName} ON {tableName} ({columnName});";
                cmd.Transaction = transaction;
                await cmd.ExecuteNonQueryAsync();
                transaction.Commit();
            }
        }
        catch
        {
            transaction.Rollback();
            throw;
        }
    }

    /// <summary>
    /// Выполнение команды без параметров
    /// </summary>
    /// <param name="sql">string</param>
    public async Task ExecuteSimpleSQL(string sql)
    {
        using var conn = await GetConnectionAsync();
        using var transaction = conn.BeginTransaction();
        try
        {
            using var cmd = new SqliteCommand(sql, conn);
            cmd.Transaction = transaction;
            await cmd.ExecuteNonQueryAsync();
            transaction.Commit();
        }
        catch
        {
            transaction.Rollback();
            throw;
        }
    }

    /// <summary>
    /// Установить параметры подключения
    /// </summary>
    /// <param name="connectString"></param>
    public void SetConnectionString(string connectString)
    {
        ConnectString = connectString;
    }

    /// <summary>
    /// При необходимости откройте соединение с базой данных.
    /// </summary>
    /// <returns>SqliteConnection</returns>
    public async Task<SqliteConnection> GetConnectionAsync()
    {
        if (conn == null)
        {
            try
            {
                var connString = ConnectString;
                conn = new SqliteConnection(connString);
                await conn.OpenAsync();

                await CreateTableIfNeeded(conn, "CharacterList", new string[] {
                    "CharPath text NOT NULL",
                    "AccDir text",
                    "AccID text",
                    "CharName text",
                    "IsDeleted bool",
                    "Location text",
                    "Ship text",
                    "Money integer",
                    "Rank integer",
                    "Created datetime",
                    "Updated datetime",
                    "OnLineSecs integer",
                    "LastOnLine datetime"
                }, "PRIMARY KEY(CharPath) ON CONFLICT REPLACE");
                await CreateIndexIfNotExists(conn, "idx_CharacterList_AccID", "CharacterList", "AccID");

                await CreateTableIfNeeded(conn, "BanList", new string[] {
                    "AccDir text NOT NULL",
                    "AccID text",
                    "BanReason text",
                    "BanStart datetime",
                    "BanEnd datetime"
                }, "PRIMARY KEY(AccDir) ON CONFLICT REPLACE");
                await CreateIndexIfNotExists(conn, "idx_BanList_AccID", "BanList", "AccID");

                await CreateTableIfNeeded(conn, "IPList", new string[] {
                    "AccDir text NOT NULL",
                    "IP text NOT NULL",
                    "AccessTime datetime"
                }, "PRIMARY KEY(AccDir, IP) ON CONFLICT REPLACE");
                await CreateIndexIfNotExists(conn, "idx_IPList_AccDir", "IPList", "AccDir");

                await CreateTableIfNeeded(conn, "LoginIDList", new string[] {
                    "AccDir text NOT NULL",
                    "LoginID text NOT NULL",
                    "AccessTime datetime"
                }, "PRIMARY KEY(AccDir, LoginID) ON CONFLICT REPLACE");
                await CreateIndexIfNotExists(conn, "idx_LoginIDList_AccDir", "LoginIDList", "AccDir");

                await CreateTableIfNeeded(conn, "GeneralStatistics", new string[] {
                    "Description text NOT NULL",
                    "Result text",
                    "SQL text",
                }, "PRIMARY KEY(Description) ON CONFLICT REPLACE");

                await CreateTableIfNeeded(conn, "HashList", new string[] {
                    "ItemHash integer PRIMARY KEY",
                    "ItemNickName text",
                    "ItemType text",
                    "IDSName text",
                    "IDSInfo text",
                    "IDSInfo1 text",
                    "IDSInfo2 text",
                    "IDSInfo3 text",
                    "ItemKeys text",
                }, null);
                await CreateIndexIfNotExists(conn, "idx_HashList_ItemNickName", "HashList", "ItemHash");

                await CreateTableIfNeeded(conn, "HardPointList", new string[] {
                    "ShipHash integer PRIMARY KEY",
                    "HPName text",
                    "HPType text",
                    "MountableTypes text",
                    "DefaultItemHash integer",
                }, null);
                await CreateIndexIfNotExists(conn, "idx_HardPointList_HPType", "HardPointList", "ShipHash");

                await CreateTableIfNeeded(conn, "EquipInfoList", new string[] {
                    "EquipHash integer PRIMARY KEY",
                    "ItemType text",
                    "MountableType text",
                }, null);
                await CreateIndexIfNotExists(conn, "idx_EquipInfoList_EquipHash", "EquipInfoList", "EquipHash");

                await CreateTableIfNeeded(conn, "ShipInfoList", new string[] {
                    "ShipHash integer PRIMARY KEY",
                    "DefaultSound integer",
                    "DefaultEngine integer",
                    "DefaultPowerPlant integer",
                }, null);
                await CreateIndexIfNotExists(conn, "idx_ShipInfoList_ShipHash", "ShipInfoList", "ShipHash");
            }
            catch (Exception e)
            {
                if (conn != null)
                    conn.Close();
                conn = null;
                throw e;
            }
        }

        if (conn.State != ConnectionState.Open)
            await conn.OpenAsync();

        return conn;
    }

    #endregion

    /// <summary>
    /// Создайте указанную таблицу. В конечном итоге это выдаст 
    /// команды alter, позволяющие добавлять столбцы.
    /// </summary>
    /// <param name="conn">SqliteConnection</param>
    /// <param name="name">string</param>
    /// <param name="cols">string[]</param>
    /// <param name="cols">string</param>
    private async Task<bool> CreateTableIfNeeded(SqliteConnection conn, string name, string[] cols, string constraint)
    {
        using var transaction = conn.BeginTransaction();
        try
        {
            using (var checkCmd = conn.CreateCommand())
            {
                checkCmd.CommandText = "SELECT name FROM sqlite_master WHERE name = @name;";
                checkCmd.Parameters.AddWithValue("@name", name);

                using (var reader = await checkCmd.ExecuteReaderAsync())
                {
                    if (await reader.ReadAsync())
                    {
                        transaction.Commit();
                        return false;
                    }
                }
            }

            using (var createCmd = conn.CreateCommand())
            {
                var columnDefs = string.Join(", ", cols);
                if (!string.IsNullOrWhiteSpace(constraint))
                    columnDefs += ", " + constraint;

                createCmd.CommandText = $"CREATE TABLE {name} ({columnDefs});";
                createCmd.Transaction = transaction;
                await createCmd.ExecuteNonQueryAsync();
                transaction.Commit();
            }
        }
        catch
        {
            transaction.Rollback();
            throw;
        }
        return true;
    }

    /// <summary>
    /// Обновление генеральной таблицу статистики.
    /// </summary>
    public async Task UpdateGeneralTableStatistics()
    {
        await ExecuteSimpleSQL("DELETE FROM GeneralStatistics;");

        await UpdateGeneralStatistics("Active characters", "SELECT COUNT(*) FROM CharacterList WHERE NOT IsDeleted ");
        await UpdateGeneralStatistics("Deleted characters", "SELECT COUNT(*) FROM CharacterList WHERE IsDeleted");
        await UpdateGeneralStatistics("Active Accounts", "SELECT COUNT(DISTINCT(AccDir)) FROM CharacterList WHERE NOT IsDeleted");
        await UpdateGeneralStatistics("Banned Accounts", "SELECT COUNT(*) FROM BanList");
        await UpdateGeneralStatistics("Unique Logins", "SELECT COUNT(DISTINCT(LoginID)) FROM LoginIDList");
        await UpdateGeneralStatistics("Characters over rank 80", "SELECT COUNT(*) FROM CharacterList WHERE Rank > '80'");
        await UpdateGeneralStatistics("Characters under rank 30", "SELECT COUNT(*) FROM CharacterList WHERE Rank < '30'");
    }

    /// <summary>
    /// Обновление статистики персонажей
    /// </summary>
    /// <param name="gameData">FLGameData</param>
    public async Task UpdateCharactersStatistic(IFLGameDataService gameData)
    {
        try
        {
            foreach (var row in gameData.Model.HashListItems)
            {
                if (row.ItemType == gameData.GAMEDATA_SYSTEMS)
                {
                    string systemName = gameData.GetItemDescByHash(row.ItemHash);
                    string systemSafe = Extensions.SafeSqlLiteral(systemName);

                    if(!string.IsNullOrEmpty(systemName) && !string.IsNullOrEmpty(systemSafe))
                    {
                        await UpdateGeneralStatistics("Персонаж в космосе в " + systemName,
                            "SELECT COUNT(*) FROM CharacterList WHERE Location LIKE '" + systemSafe + " in space%' AND IsDeleted = '0'");

                        await UpdateGeneralStatistics("Персонаж пристыкован к " + systemName,
                            "SELECT COUNT(*) FROM CharacterList WHERE Location LIKE '" + systemSafe + " docked%' AND IsDeleted = '0'");
                    }
                }
                else if (row.ItemType == gameData.GAMEDATA_SHIPS)
                {
                    string shipName = gameData.GetItemDescByHash(row.ItemHash);
                    string shipSafe = Extensions.SafeSqlLiteral(shipName);
                    if (!string.IsNullOrEmpty(shipName) && !string.IsNullOrEmpty(shipSafe))
                    {
                        await UpdateGeneralStatistics("Номер " + shipName,
                            "SELECT COUNT(*) FROM CharacterList WHERE Ship LIKE '" + shipSafe + "' AND IsDeleted = '0'");
                    }
                }
            }
        }
        catch
        {
            throw;
        }
    }

    /// <summary>
    /// Создает таблицу истории за вчерашний день, копируя в нее информацию
    /// обо всех активных в данный момент персонажах.
    /// </summary>
    public async Task UpdateActiveCharacterHistory()
    {
        if (!await ActiveCharacterHistoryExist())
            return;

        using var conn = await GetConnectionAsync();
        using var transaction = conn.BeginTransaction();
        DateTime yesterday = DateTime.Now.Subtract(new TimeSpan(1, 0, 0, 0));
        string historyTableName = Extensions.GetCharacterHistoryName(yesterday);
        try
        {
            // Проверяем, существует ли таблица CharacterList
            using (var checkCmd = conn.CreateCommand())
            {
                checkCmd.CommandText = "SELECT sql FROM sqlite_master WHERE type = 'table' AND name = 'CharacterList';";

                using var reader = await checkCmd.ExecuteReaderAsync();
                if (!await reader.ReadAsync())
                    return; // таблицы CharacterList нет

                var createSql = reader.GetString(0)
                    .Replace("CharacterList", historyTableName);

                // Создаём таблицу истории
                using (var createCmd = conn.CreateCommand())
                {
                    createCmd.CommandText = createSql;
                    createCmd.Transaction = transaction;
                    await createCmd.ExecuteNonQueryAsync();
                }
            }

            using var insertCmd = conn.CreateCommand();
            insertCmd.CommandText = $@"
                        INSERT OR REPLACE INTO {historyTableName}
                        SELECT * FROM CharacterList
                        WHERE NOT IsDeleted AND (LastOnLine > Date('now','-1 day'));";
            insertCmd.Transaction = transaction;

            await insertCmd.ExecuteNonQueryAsync();
            transaction.Commit();
        }
        catch
        {
            transaction.Rollback();
            throw;
        }
    }

    #region Exists

    /// <summary>
    /// Возвращает значение true, если таблица CharacterHistory существует в указанный день.
    /// </summary>
    /// <param name="date">DateTime</param>
    /// <returns>bool</returns>
    public async Task<bool> CharacterHistoryExists(DateTime date)
    {
        using var conn = await GetConnectionAsync();
        using var cmd = conn.CreateCommand();
        cmd.CommandText = "SELECT 1 FROM sqlite_master WHERE name = @name LIMIT 1;";
        cmd.Parameters.AddWithValue("@name", Extensions.GetCharacterHistoryName(date));

        using var reader = await cmd.ExecuteReaderAsync();
        return await reader.ReadAsync();
    }

    /// <summary>
    /// Проверяет, существует ли таблица истории за вчерашний день. 
    /// Если нет, то возвращает true, в противном случае false.
    /// </summary>
    /// <returns>bool</returns>
    public async Task<bool> ActiveCharacterHistoryExist()
    {
        DateTime yesterday = DateTime.Now.Subtract(new TimeSpan(1, 0, 0, 0));
        if (await CharacterHistoryExists(yesterday))
            return false;
        return true;
    }

    #endregion

    #region Insert or Update

    public async Task InsertOrUpdateGameInfoChanges(ILogRecorder log, DataModel model)
    {
        using var conn = await GetConnectionAsync();
        using var transaction = conn.BeginTransaction();
        try
        {
            await conn.ExecuteAsync(@"
                    INSERT INTO HashList (
                        ItemHash, ItemNickName, ItemType,
                        IDSName, IDSInfo, IDSInfo1, IDSInfo2, IDSInfo3, ItemKeys
                    ) VALUES (
                        @ItemHash, @ItemNickName, @ItemType,
                        @IDSName, @IDSInfo, @IDSInfo1, @IDSInfo2, @IDSInfo3, @ItemKeys
                    )
                    ON CONFLICT(ItemHash) DO UPDATE SET
                        ItemNickName = excluded.ItemNickName,
                        ItemType = excluded.ItemType,
                        IDSName = excluded.IDSName,
                        IDSInfo = excluded.IDSInfo,
                        IDSInfo1 = excluded.IDSInfo1,
                        IDSInfo2 = excluded.IDSInfo2,
                        IDSInfo3 = excluded.IDSInfo3,
                        ItemKeys = excluded.ItemKeys;",
              model.HashListItems,
              transaction);
            log.AddLog($"HashList updated: {model.HashListItems.Count}");

            await conn.ExecuteAsync(@"
                    INSERT INTO HardPointList (
                        ShipHash, HPName, HPType, MountableTypes, DefaultItemHash
                    ) VALUES (
                        @ShipHash, @HPName, @HPType, @MountableTypes, @DefaultItemHash
                    )
                    ON CONFLICT(ShipHash) DO UPDATE SET
                        HPType = excluded.HPType,
                        MountableTypes = excluded.MountableTypes,
                        DefaultItemHash = excluded.DefaultItemHash;",
                model.HardPointList,
                transaction);
            log.AddLog($"HardPointList updated: {model.HardPointList.Count}");

            await conn.ExecuteAsync(@"
                    INSERT INTO EquipInfoList (
                        EquipHash, ItemType, MountableType
                    ) VALUES (
                        @EquipHash, @ItemType, @MountableType
                    )
                    ON CONFLICT(EquipHash) DO UPDATE SET
                        ItemType = excluded.ItemType,
                        MountableType = excluded.MountableType;",
                model.EquipInfoList,
                transaction);
            log.AddLog($"EquipInfoList updated: {model.EquipInfoList.Count}");

            await conn.ExecuteAsync(@"
                    INSERT INTO ShipInfoList (
                        ShipHash, DefaultSound, DefaultEngine, DefaultPowerPlant
                    ) VALUES (
                        @ShipHash, @DefaultSound, @DefaultEngine, @DefaultPowerPlant
                    )
                    ON CONFLICT(ShipHash) DO UPDATE SET
                        DefaultSound = excluded.DefaultSound,
                        DefaultEngine = excluded.DefaultEngine,
                        DefaultPowerPlant = excluded.DefaultPowerPlant;",
                model.ShipInfoList,
                transaction);
            log.AddLog($"ShipInfoList updated: {model.ShipInfoList.Count}");
            transaction.Commit();

            Model = model;
        }
        catch (Exception e)
        {
            transaction.Rollback();
            log.AddLog(String.Format("Error '{0}' when updating db", e.Message));
        }
    }

    /// <summary>
    /// Commit pending changes on the CharacterList, BanList, IPList and LoginID
    /// </summary>
    public async Task InsertOrUpdatePlayerInfoChanges(DataModel tempDataStore, ILogRecorder log)
    {
        foreach (var item in tempDataStore.DataSetPlayerInfo.CharacterList)
        {
            try
            {
                await InsertOrUpdateCharacter(item);
            }
            catch (Exception e)
            {
                log.AddLog($"Error '{e.Message}' when updating CharacterList with CharPath = {item.CharPath}");
            }
        }

        foreach (var item in tempDataStore.DataSetPlayerInfo.BanList)
        {
            try
            {
                await InsertOrUpdateBanRecordToDatabase(item);
            }
            catch (Exception e)
            {
                log.AddLog($"Update of BanList failed for AccDir = {item.AccDir}: {e.Message}");
            }
        }
        foreach (var item in tempDataStore.IPList)
        {
            try
            {
                await InsertOrUpdateIPRecord(item);
            }
            catch (Exception e)
            {
                log.AddLog($"Update of IPList failed for AccDir = {item.AccDir}, IP = {item.IP}: {e.Message}");
            }
        }
        foreach (var item in tempDataStore.LoginIDList)
        {
            try
            {
                await InsertOrUpdateLoginIdRecord(item);
            }
            catch (Exception e)
            {
                log.AddLog($"Update of LoginIDList failed for AccDir = {item.AccDir}, LoginID = {item.LoginID}: {e.Message}");
            }
        }
    }

    /// <summary>
    /// Добавить запись об авторизации пользователя
    /// </summary>
    /// <param name="item">LoginIDItem</param>
    public async Task InsertOrUpdateLoginIdRecord(LoginIDItem item)
    {
        using var conn = await GetConnectionAsync();
        using var transaction = conn.BeginTransaction();

        try
        {
            int affected = await conn.ExecuteAsync(@"
                        UPDATE LoginIDList SET
                            AccessTime = @AccessTime
                        WHERE AccDir = @AccDir AND LoginID = @LoginID", item, transaction);

            if (affected == 0)
            {
                await conn.ExecuteAsync(@"
                            INSERT INTO LoginIDList
                            (AccDir, LoginID, AccessTime)
                            VALUES
                            (@AccDir, @LoginID, @AccessTime)", item, transaction);
            }

            transaction.Commit();
        }
        catch
        {
            transaction.Rollback();
            throw;
        }
    }

    /// <summary>
    /// Добавить запись об IP пользователя
    /// </summary>
    /// <param name="item">IPItem</param>
    public async Task InsertOrUpdateIPRecord(IPItem item)
    {
        using var conn = await GetConnectionAsync();
        using var transaction = conn.BeginTransaction();

        try
        {
            int affected = await conn.ExecuteAsync(@"
                        UPDATE IPList SET
                            AccessTime = @AccessTime
                        WHERE AccDir = @AccDir AND IP = @IP", item, transaction);

            if (affected == 0)
            {
                await conn.ExecuteAsync(@"
                            INSERT INTO IPList
                            (AccDir, IP, AccessTime)
                            VALUES
                            (@AccDir, @IP, @AccessTime)", item, transaction);
            }

            transaction.Commit();
        }
        catch
        {
            transaction.Rollback();
            throw;
        }
    }

    /// <summary>
    /// Добавить запись о персонаже пользователя
    /// </summary>
    /// <param name="item">CharacterItem</param>
    public async Task InsertOrUpdateCharacter(CharacterItem item)
    {
        using var conn = await GetConnectionAsync();
        using var transaction = conn.BeginTransaction();

        try
        {
            int affected = await conn.ExecuteAsync(@"
                        UPDATE CharacterList SET
                            AccDir = @AccDir,
                            AccID = @AccID,
                            CharName = @CharName,
                            IsDeleted = @IsDeleted,
                            Location = @Location,
                            Ship = @Ship,
                            Money = @Money,
                            Rank = @Rank,
                            Created = @Created,
                            Updated = @Updated,
                            OnLineSecs = @OnLineSecs,
                            LastOnLine = @LastOnLine
                        WHERE CharPath = @CharPath", item, transaction);

            if (affected == 0)
            {
                await conn.ExecuteAsync(@"
                            INSERT INTO CharacterList
                            (CharPath, AccDir, AccID, CharName, IsDeleted, Location, Ship, Money, Rank, Created, Updated, OnLineSecs, LastOnLine)
                            VALUES
                            (@CharPath, @AccDir, @AccID, @CharName, @IsDeleted, @Location, @Ship, @Money, @Rank, @Created, @Updated, @OnLineSecs, @LastOnLine)", item, transaction);
            }

            transaction.Commit();
        }
        catch
        {
            transaction.Rollback();
            throw;
        }
    }

    /// <summary>
    /// Добавление забаненного пользователя
    /// </summary>
    /// <param name="item">BanItem</param>
    public async Task InsertOrUpdateBanRecordToDatabase(BanItem item)
    {
        using var conn = await GetConnectionAsync();
        using var transaction = conn.BeginTransaction();
        try
        {
            int affected = await conn.ExecuteAsync(@"
                        UPDATE BanList SET
                            AccID = @AccID,
                            BanReason = @BanReason,
                            BanStart = @BanStart,
                            BanEnd = @BanEnd
                        WHERE AccDir = @AccDir", item, transaction);

            if (affected == 0)
            {
                await conn.ExecuteAsync(@"
                            INSERT INTO BanList
                            (AccDir, AccID, BanReason, BanStart, BanEnd)
                            VALUES
                            (@AccDir, @AccID, @BanReason, @BanStart, @BanEnd)", item, transaction);
            }
            transaction.Commit();

        }
        catch
        {
            transaction.Rollback();
            throw;
        }
    }

    /// <summary>
    /// Вставить в общую статистическую таблицу.
    /// </summary>
    /// <param name="description"></param>
    /// <param name="SQL"></param>
    public async Task UpdateGeneralStatistics(string description, string SQL)
    {
        using var conn = await GetConnectionAsync();
        using var transaction = conn.BeginTransaction();
        try
        {
            using var cmd = conn.CreateCommand();
            cmd.Transaction = transaction;

            cmd.CommandText = @"INSERT OR REPLACE INTO GeneralStatistics (Description, SQL)
                                VALUES (@Description, @SQL);";

            cmd.Parameters.AddWithValue("@Description", description);
            cmd.Parameters.AddWithValue("@SQL", SQL);

            await cmd.ExecuteNonQueryAsync();
            transaction.Commit();
        }
        catch
        {
            transaction.Rollback();
            throw;
        }
    }

    #endregion

    #region Get

    /// <summary>
    /// Получение актуальных игровых данных
    /// </summary>
    /// <param name="log">LogRecorderInterface</param>
    /// <param name="model">DataModel</param>
    /// <returns>DataModel</returns>
    public async Task GetGameData(ILogRecorder log, DataModel model)
    {
        try
        {
            await GetConnectionAsync();

            model.HashListItems = (await conn.QueryAsync<HashListItem>("SELECT * FROM HashList")).AsList();
            model.HardPointList = (await conn.QueryAsync<HardPointItem>("SELECT * FROM HardPointList")).AsList();
            model.EquipInfoList = (await conn.QueryAsync<EquipInfoItem>("SELECT * FROM EquipInfoList")).AsList();
            model.ShipInfoList = (await conn.QueryAsync<ShipInfoItem>("SELECT * FROM ShipInfoList")).AsList();

            Model = model;
        }
        catch (Exception e)
        {
            log.AddLog(String.Format("Error '{0}' when updating db", e.Message));
        }
    }

    /// <summary>
    /// Get all character records
    /// </summary>
    public async Task GetCharacterList(DataModel model)
    {
        await GetConnectionAsync();
        model.DataSetPlayerInfo.CharacterList = (await conn.QueryAsync<CharacterItem>("SELECT * FROM CharacterList")).AsList();
    }

    /// <summary>
    /// Get the characters for the specified account
    /// </summary>
    /// <param name="accDir">The account dir</param>
    public async Task GetCharacterListByAccDir(string accDir)
    {
        await GetConnectionAsync();
        Model.DataSetPlayerInfo.CharacterList = (await conn.QueryAsync<CharacterItem>("SELECT * FROM CharacterList WHERE AccDir ='" + accDir + "'")).AsList();
    }

    /// <summary>
    /// Get all ban list records.
    /// </summary>
    /// <param name="model">The ban list.</param>
    public async Task GetBanList(DataModel model)
    {
        await GetConnectionAsync();
        model.DataSetPlayerInfo.BanList = (await conn.QueryAsync<BanItem>("SELECT * FROM BanList")).AsList();
    }

    /// <summary>
    /// Get the ban list for the specified account
    /// </summary>
    public async Task GetBanListByAccDir(string accDir)
    {
        await GetConnectionAsync();
        Model.DataSetPlayerInfo.BanList = (await conn.QueryAsync<BanItem>("SELECT * FROM BanList WHERE AccDir ='" + accDir + "'")).AsList();
    }

    /// <summary>
    /// Get the IPList for the specified account
    /// </summary>
    /// <param name="accDir">The account dir</param>
    public async Task<List<IPItem>> GetIPListByAccDir(string accDir)
    {
        await GetConnectionAsync();
        Model.IPList = (await conn.QueryAsync<IPItem>("SELECT * FROM IPList WHERE AccDir ='" + accDir + "'")).AsList();
        return Model.IPList;
    }

    /// <summary>
    /// Get the IPList for the specified array of IP addresses
    /// </summary>
    /// <param name="IPs">The IPs to search for.</param>
    public async Task<List<IPItem>> GetIPListByIP(string[] IPs)
    {
        await GetConnectionAsync();

        if (IPs.Length == 0)
            return Model.IPList;

        string query = "SELECT * FROM IPList WHERE";
        for (int i = 0; i < IPs.Length; i++)
        {
            if (i != 0)
                query += " OR";
            query += " IP = '" + IPs[i] + "'";
        }

        Model.IPList = (await conn.QueryAsync<IPItem>(query)).AsList();
        return Model.IPList;
    }

    /// <summary>
    /// Get the IPList for the specified IP and fragment of IP.
    /// </summary>
    /// <param name="ipFrag">The IPs to search for.</param>
    public async Task<List<IPItem>> GetIPListByIP(string ipFrag)
    {
        if (!string.IsNullOrEmpty(ipFrag))
            return Model.IPList;

        await GetConnectionAsync();

        string query = "SELECT * FROM IPList WHERE IP LIKE '" + Extensions.SafeSqlLiteral(ipFrag) + "%'";
        Model.IPList = (await conn.QueryAsync<IPItem>(query)).AsList();
        return Model.IPList;
    }

    /// <summary>
    /// Get the LoginIDList for the specified account
    /// </summary>
    /// <param name="accDir">The account dir</param>
    public async Task<List<LoginIDItem>> GetLoginIDListByAccDir(string accDir)
    {
        if (!string.IsNullOrEmpty(accDir))
            return Model.LoginIDList;

        await GetConnectionAsync();

        Model.LoginIDList = (await conn.QueryAsync<LoginIDItem>("SELECT * FROM LoginIDList WHERE AccDir ='" + accDir + "'")).AsList();
        return Model.LoginIDList;
    }

    /// <summary>
    /// Get the LoginIDList for the specified loginID
    /// </summary>
    /// <param name="loginIDs">The login ID to search for.</param>
    public async Task<List<LoginIDItem>> GetLoginIDListByLoginID(string[] loginIDs)
    {
        await GetConnectionAsync();

        if (loginIDs.Length == 0)
            return Model.LoginIDList;

        string query = "SELECT * FROM LoginIDList WHERE";
        for (int i = 0; i < loginIDs.Length; i++)
        {
            if (i != 0)
                query += " OR";
            query += " LoginID = '" + loginIDs[i] + "'";
        }

        Model.LoginIDList = (await conn.QueryAsync<LoginIDItem>(query)).AsList();
        return Model.LoginIDList;
    }

    /// <summary>
    /// Get all statistics records
    /// </summary>
    public async Task<List<GeneralStatistics>> GetGeneralStatisticsTable()
    {
        await GetConnectionAsync();

        var stats = (await conn.QueryAsync<GeneralStatistics>("SELECT Description, Result, SQL FROM GeneralStatistics")).AsList();

        var semaphore = new SemaphoreSlim(5); // например, 5 параллельных запросов

        var tasks = stats.Select(async stat =>
        {
            await semaphore.WaitAsync();
            try
            {
                var result = await conn.ExecuteScalarAsync<long>(stat.SQL);
                stat.Result = result.ToString();
            }
            finally
            {
                semaphore.Release();
            }
        });

        await Task.WhenAll(tasks);

        Model.GeneralStatisticsList = stats;
        return Model.GeneralStatisticsList;
    }

    /// <summary>
    /// Get all character history records for the specified date. Return true if the history
    /// table exists, otherwise return false.
    /// </summary>
    public async Task<bool> GetCharacterHistory(DateTime date)
    {
        if (date == DateTime.Today)
        {
            Model.DataSetPlayerInfo.CharacterList = conn.Query<CharacterItem>("SELECT * FROM CharacterList WHERE NOT IsDeleted").ToList();
            return true;
        }
        else if (await CharacterHistoryExists(date))
        {
            Model.DataSetPlayerInfo.CharacterList = conn.Query<CharacterItem>("SELECT * FROM " 
                + Extensions.GetCharacterHistoryName(date)).ToList();
            return true;
        }
        return false;
    }

    #endregion

    #region Delete

    /// <summary>
    /// Удаление забаненного пользователя
    /// </summary>
    /// <param name="accDir">accDir</param>
    public async Task DeleteBanRecordFromDatabase(string accDir)
    {
        using var conn = await GetConnectionAsync();
        using var transaction = conn.BeginTransaction();
        try
        {
            using (var delCmd = conn.CreateCommand())
            {
                delCmd.CommandText = $"DELETE FROM BanList WHERE AccDir = @AccDir";
                delCmd.Parameters.AddWithValue("@AccDir", accDir);
                delCmd.Transaction = transaction;
                await delCmd.ExecuteNonQueryAsync();
                transaction.Commit();
            }
        }
        catch
        {
            transaction.Rollback();
            throw;
        }
    }

    /// <summary>
    /// Удаляет старые таблицы истории на основе ретроспективного горизонта в днях
    /// </summary>
    /// <param name="horizon">int</param>
    public async Task CleanUpCharacterHistory(int horizon)
    {
        DateTime last_week = DateTime.Now.Subtract(new TimeSpan(horizon, 0, 0, 0));
        string comparator = Extensions.GetCharacterHistoryName(last_week);
        var tablesToDrop = new List<string>();
        using var conn = await GetConnectionAsync();
        using var transaction = conn.BeginTransaction();

        try
        {
            using var cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT name FROM sqlite_master WHERE name LIKE 'CharacterList_%' AND name < @comparator;";
            cmd.Transaction = transaction;
            cmd.Parameters.AddWithValue("@comparator", comparator);

            using var reader = await cmd.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                tablesToDrop.Add(reader.GetString(0));
            }

            if (tablesToDrop.Count > 0)
            {

                foreach (var tableName in tablesToDrop)
                {
                    using var dropCmd = conn.CreateCommand();
                    dropCmd.Transaction = transaction;
                    dropCmd.CommandText = $"DROP TABLE IF EXISTS \"{tableName}\";";
                    await dropCmd.ExecuteNonQueryAsync();
                }
            }

            transaction.Commit();
        }
        catch
        {
            transaction.Rollback();
            throw;
        }
    }

    /// <summary>
    /// Очистка игровых данных
    /// </summary>
    /// <param name="log">LogRecorderInterface</param>
    public async Task ClearGameData(ILogRecorder log)
    {
        try
        {
            await ExecuteSimpleSQL("DELETE FROM HashList");
            await ExecuteSimpleSQL("DELETE FROM HardPointList");
            await ExecuteSimpleSQL("DELETE FROM EquipInfoList");
            await ExecuteSimpleSQL("DELETE FROM ShipInfoList");
        }
        catch (Exception e)
        {
            log.AddLog(String.Format("Error '{0}' when updating db", e.Message));
        }
    }

    #endregion
}