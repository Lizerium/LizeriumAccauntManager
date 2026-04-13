/*
 * Author: Nikolay Dvurechensky
 * Site: https://dvurechensky.pro/
 * Gmail: dvurechenskysoft@gmail.com
 * Last Updated: 13 апреля 2026 12:59:47
 * Version: 1.0.7
 */

using Dapper;

using Root;
using Root.Components;
using Root.Services;

namespace DSAccountManager.Tests
{
    [TestClass]
    public sealed class DataBaseServiceTests
    {
        /// <summary>
        /// Вызывается перед каждым тестом
        /// </summary>
        [TestInitialize]
        public void Init()
        {
            Console.WriteLine("🌍 Подготовка перед тестом...");
        }

        [TestMethod]
        public async Task UpdateCharactersStatistic_Test()
        {
            // Arrange
            var dbService = new DataBaseService();
            dbService.SetConnectionString($"Data Source={GenTestDbPath()}");

            var testGameData = new FLGameDataService("");
            testGameData.Model = GetTestDataModel();
            await dbService.InsertOrUpdateGameInfoChanges(new TestLogRecorder(), testGameData.Model);

            // Act
            await dbService.UpdateCharactersStatistic(testGameData);

            // Assert
            var conn = await dbService.GetConnectionAsync();
            var result = await conn.QueryAsync<string>("SELECT Description FROM GeneralStatistics");

            // Проверяем что хотя бы одна статистика была записана
            Assert.IsTrue(result.Any(desc => desc.StartsWith("Персонаж в космосе в") 
            || desc.StartsWith("Номер") 
            || desc.StartsWith("Персонаж пристыкован к")));
        }

        [TestMethod]
        public async Task GetConnectionAsync_Test()
        {
            var dbService = new DataBaseService();
            dbService.SetConnectionString($"Data Source={GenTestDbPath()}");
            var conn = await dbService.GetConnectionAsync();
            Assert.IsNotNull(conn);
        }

        [TestMethod]
        public async Task GetGameData_Test()
        {
            // Arrange
            var dbService = new DataBaseService();
            dbService.SetConnectionString($"Data Source={GenTestDbPath()}");

            DataModel testModel = GetTestDataModel();

            // простой логгер, сохраняющий строки
            var logMock = new TestLogRecorder();
            await dbService.InsertOrUpdateGameInfoChanges(logMock, testModel);

            // Assert
            // Проверим что нет ошибок в логах
            Assert.IsTrue(string.Join("; ", logMock.Messages) == "HashList updated: 2; HardPointList updated: 1; EquipInfoList updated: 3; ShipInfoList updated: 4");

            //Act
            logMock = new TestLogRecorder();
            var modelGameData = new DataModel();
            await dbService.GetGameData(logMock, modelGameData);

            // Assert
            Assert.IsNotNull(modelGameData.HashListItems);
            Assert.IsNotNull(modelGameData.HardPointList);
            Assert.IsNotNull(modelGameData.EquipInfoList);
            Assert.IsNotNull(modelGameData.ShipInfoList);

            // Проверяем количество записей
            Assert.AreNotEqual(0, modelGameData.HashListItems.Count, "HashListItems пуст");
            Assert.AreNotEqual(0, modelGameData.HardPointList.Count, "HashListItems пуст");
            Assert.AreNotEqual(0, modelGameData.EquipInfoList.Count, "HashListItems пуст");
            Assert.AreNotEqual(0, modelGameData.ShipInfoList.Count, "HashListItems пуст");
            // Убеждаемся, что присвоилось
            Assert.AreEqual(testModel.HashListItems.Count, dbService.Model.HashListItems.Count);
            Assert.AreEqual(testModel.HardPointList.Count, dbService.Model.HardPointList.Count);
            Assert.AreEqual(testModel.EquipInfoList.Count, dbService.Model.EquipInfoList.Count);
            Assert.AreEqual(testModel.ShipInfoList.Count, dbService.Model.ShipInfoList.Count);

            // Проверим что нет ошибок в логах
            Assert.IsFalse(logMock.Messages.Any(),
                $"Ожидался пустой лог, но было: {string.Join("; ", logMock.Messages)}");
        }

        [TestMethod]
        public async Task UpdateGeneralTableStatistics_Test()
        {
            // Arrange
            var dbService = new DataBaseService();
            dbService.SetConnectionString($"Data Source={GenTestDbPath()}");

            // Act
            await dbService.UpdateGeneralTableStatistics();

            // Assert
            var conn = await dbService.GetConnectionAsync();
            using var cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT Description, SQL FROM GeneralStatistics";

            var result = new List<(string desc, string sql)>();
            using (var reader = await cmd.ExecuteReaderAsync())
            {
                while (await reader.ReadAsync())
                {
                    result.Add((reader.GetString(0), reader.GetString(1)));
                }
            }

            Assert.AreEqual(7, result.Count, "Expected 7 statistics entries");

            Assert.IsTrue(result.Any(x => x.desc == "Active characters"));
            Assert.IsTrue(result.Any(x => x.desc == "Deleted characters"));
            Assert.IsTrue(result.Any(x => x.desc == "Active Accounts"));
            Assert.IsTrue(result.Any(x => x.desc == "Banned Accounts"));
            Assert.IsTrue(result.Any(x => x.desc == "Unique Logins"));
            Assert.IsTrue(result.Any(x => x.desc == "Characters over rank 80"));
            Assert.IsTrue(result.Any(x => x.desc == "Characters under rank 30"));
        }

        private string GenTestDbPath()
        {
            string tempDbPath = Path.Combine(AppContext.BaseDirectory, "test_dsam.db");
            if (File.Exists(tempDbPath))
                File.Delete(tempDbPath);
            return tempDbPath;
        }

        private static DataModel GetTestDataModel()
        {
            return new DataModel()
            {
                HashListItems = new List<HashListItem>()
                {
                    new HashListItem() { ItemHash = 1, ItemType = "systems", IDSName = "name1", IDSInfo1 = "info1" },
                    new HashListItem() { ItemHash = 2, ItemType = "ships", IDSName = "name2", IDSInfo1 = "info2" }
                },
                HardPointList = new List<HardPointItem>
                {
                    new HardPointItem() { ShipHash = 2 }
                },
                EquipInfoList = new List<EquipInfoItem>
                {
                    new EquipInfoItem() { EquipHash = 4 },
                    new EquipInfoItem() { EquipHash = 5 },
                    new EquipInfoItem() { EquipHash = 6 }
                },
                ShipInfoList = new List<ShipInfoItem>
                {
                    new ShipInfoItem() { ShipHash = 7 },
                    new ShipInfoItem() { ShipHash = 8 },
                    new ShipInfoItem() { ShipHash = 9 },
                    new ShipInfoItem() { ShipHash = 10 }
                }
            };
        }
    }
}
