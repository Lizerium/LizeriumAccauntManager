/*
 * Author: Nikolay Dvurechensky
 * Site: https://dvurechensky.pro/
 * Gmail: dvurechenskysoft@gmail.com
 * Last Updated: 16 апреля 2026 11:44:04
 * Version: 1.0.11
 */

using System.Collections.Concurrent;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Text.Json;

using Root.Components;
using Root.Services;

namespace DSAccountManager.Tests
{
    [TestClass]
    public sealed class FLGameDataServiceTests
    {
        [TestMethod]
        public async Task LoadAllAsync_Test()
        {
            /*
                History:
                08.07.2025 20:00 - 14,3 сек
                09.07.2025 00:30 - 14 сек
                09.07.2025 12:30 - 5 сек
             */
            Stopwatch timer = new Stopwatch();
            timer.Start();
            IFLGameDataService FLGameDataService = new FLGameDataService("F:\\LIZERIUM\\6_COMPARE\\COMPARE_GAMES\\LizeriumFreelancerModeChange");
            var mockRecorder = new TestLogRecorder();
            var progress = new Progress<ProgressInfo>(tuple =>
            {
            });

            CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
            await FLGameDataService.LoadAllAsync(cancellationTokenSource.Token, progress, mockRecorder);
            timer.Stop();
            var time = timer.Elapsed.TotalSeconds;
            Assert.IsTrue(time < 10, $"Elapsed: {time}s");
            Assert.IsTrue(FLGameDataService.Model.HashListItems != null);//77434->77427
            Assert.IsTrue(FLGameDataService.Model.HashListItems.Count == 77427, $"HashListItems: {FLGameDataService.Model.HashListItems.Count}");
            Assert.IsTrue(FLGameDataService.Model.EquipInfoList.Count == 3366, $"EquipInfoList: {FLGameDataService.Model.EquipInfoList.Count}");
        }

        [TestMethod]
        public async Task ParseBaseAndSystemsEntries_Test()
        {
            /*
               History:
               08.07.2025 20:23 - 674 миллисекунды
               08.07.2025 21:25 - 459 миллисекунд add Parallel
               08.07.2025 21:55 - 443 миллисекунд add Task.WhenAll
            */
            Stopwatch timer = new Stopwatch();
            IFLGameDataService FLGameDataService = new FLGameDataService("F:\\LIZERIUM\\6_COMPARE\\COMPARE_GAMES\\LizeriumFreelancerModeChange");
            var mockRecorder = new TestLogRecorder();
            var progress = new Progress<ProgressInfo>(tuple =>
            {
            });
            CancellationToken cancellationToken = new CancellationToken();
            // Load the string dlls.
            await FLGameDataService.LoadDLLS(cancellationToken);
            timer.Start();
            await FLGameDataService.ParseBaseAndSystemsEntries(progress, mockRecorder, cancellationToken);
            timer.Stop();
            var time = timer.Elapsed.TotalSeconds;
            Assert.IsTrue(time < 1, $"Elapsed: {time}s");
        }

        [TestMethod]
        public async Task LoadInfocardMap_Test()
        {
            Stopwatch timer = new Stopwatch();
            IFLGameDataService FLGameDataService = new FLGameDataService("F:\\LIZERIUM\\6_COMPARE\\COMPARE_GAMES\\LizeriumFreelancerModeChange");
            var mockRecorder = new TestLogRecorder();
            var progress = new Progress<ProgressInfo>(tuple =>
            {
            });
            CancellationToken cancellationToken = new CancellationToken();
            timer.Start();
            await FLGameDataService.LoadInfocardMap(mockRecorder, cancellationToken);
            timer.Stop();
            var time = timer.Elapsed.TotalSeconds;
            Assert.IsTrue(time < 10, $"Elapsed: {time}s");
            Assert.IsTrue(FLGameDataService.InfocardMap.Count > 0, $"InfocardMap::Count::{FLGameDataService.InfocardMap.Count}");
        }

        [TestMethod]
        public async Task ParseEquipmentEntries_Test()
        {
            /*
               History:
               08.07.2025 22:23 - 439 миллисекунды
               08.07.2025 22:45 - 358 миллисекунд add Parallel first foreach
               08.07.2025 23:45 - 315 миллисекунд add Parallel all foreach
            */
            Stopwatch timer = new Stopwatch();
            IFLGameDataService FLGameDataService = new FLGameDataService("F:\\LIZERIUM\\6_COMPARE\\COMPARE_GAMES\\LizeriumFreelancerModeChange");
            var mockRecorder = new TestLogRecorder();
            var progress = new Progress<ProgressInfo>(tuple =>
            {
            });
            CancellationToken cancellationToken = new CancellationToken();
            // Load the string dlls.
            await FLGameDataService.LoadDLLS(cancellationToken);
            timer.Start();
            await FLGameDataService.ParseEquipmentEntries(progress, mockRecorder, cancellationToken);
            timer.Stop();
            var time = timer.Elapsed.TotalSeconds;
            Assert.IsTrue(time < 1, $"Elapsed: {time}s");
            //Assert.IsTrue(FLGameDataService.Model.HashListItems.Count == 8363, $"EquipInfoList: {FLGameDataService.Model.EquipInfoList.Count}");
            Assert.IsTrue(FLGameDataService.Model.EquipInfoList.Count == 3366, $"EquipInfoList: {FLGameDataService.Model.EquipInfoList.Count}");
        }

        [TestMethod]
        public async Task ParseFactionEntries_Test()
        {
            /*
              History:
              09.07.2025 00:50 - 30 миллисекунд
           */
            Stopwatch timer = new Stopwatch();
            IFLGameDataService FLGameDataService = new FLGameDataService("F:\\LIZERIUM\\6_COMPARE\\COMPARE_GAMES\\LizeriumFreelancerModeChange");
            var mockRecorder = new TestLogRecorder();
            var progress = new Progress<ProgressInfo>(tuple =>
            {
            });
            CancellationToken cancellationToken = new CancellationToken();
            // Load the string dlls.
            await FLGameDataService.LoadDLLS(cancellationToken);
            timer.Start();
            await FLGameDataService.ParseFactionEntries(progress, mockRecorder, cancellationToken);
            timer.Stop();
            var time = timer.Elapsed.TotalSeconds;
            Assert.IsTrue(time < 1, $"Elapsed: {time}s");
            Assert.IsTrue(FLGameDataService.ThreadSafeHashListModel.Count == 222, $"HashList: {FLGameDataService.Model.EquipInfoList.Count}");
        }

        [TestMethod]
        public async Task ParseShipEntries_Test()
        {
            /*
             History:
             09.07.2025 00:56 - 3,7 сек
             09.07.2025 2:45 - 1 сек add Parallel all foreach

             2240164623 example id hardpoints, 28 elements - 
             1396 + 32492 = 33888
           */
            Stopwatch timer = new Stopwatch();
            IFLGameDataService FLGameDataService = new FLGameDataService("F:\\LIZERIUM\\6_COMPARE\\COMPARE_GAMES\\LizeriumFreelancerModeChange");
            var mockRecorder = new TestLogRecorder();
            var progress = new Progress<ProgressInfo>(tuple =>
            {
            });
            CancellationToken cancellationToken = new CancellationToken();
            // Load the string dlls.
            await FLGameDataService.LoadDLLS(cancellationToken);
            timer.Start();
            await FLGameDataService.ParseShipEntries(progress, mockRecorder, cancellationToken);
            timer.Stop();
            var time = timer.Elapsed.TotalSeconds;
            Assert.IsTrue(time < 2, $"Elapsed: {time}s");
            Assert.IsTrue(FLGameDataService.ThreadSafeHashListModel.Count == 1510, $"HashList: {FLGameDataService.Model.EquipInfoList.Count}");
            Assert.IsTrue(FLGameDataService.Model.HardPointList.Count == 33888, $"HardPointList: {FLGameDataService.Model.HardPointList.Count}");
        }

        [TestMethod]
        public async Task ParseGoodEntries_Test()
        {
            /*
            History:
            09.07.2025 00:56 - 3,7 сек
            09.07.2025 2:45 - 1 сек add Parallel all foreach
          */
            Stopwatch timer = new Stopwatch();
            IFLGameDataService FLGameDataService = new FLGameDataService("F:\\LIZERIUM\\6_COMPARE\\COMPARE_GAMES\\LizeriumFreelancerModeChange");
            var mockRecorder = new TestLogRecorder();
            var progress = new Progress<ProgressInfo>(tuple =>
            {
            });
            CancellationToken cancellationToken = new CancellationToken();
            // Load the string dlls.
            await FLGameDataService.LoadDLLS(cancellationToken);
            await FLGameDataService.ParseFactionEntries(progress, mockRecorder, cancellationToken);
            await FLGameDataService.ParseShipEntries(progress, mockRecorder, cancellationToken);
            await FLGameDataService.ParseEquipmentEntries(progress, mockRecorder, cancellationToken);
            timer.Start();
            await FLGameDataService.ParseGoodEntries(progress, mockRecorder, cancellationToken);
            timer.Stop();
            var time = timer.Elapsed.TotalSeconds;
            Assert.IsTrue(time < 2, $"Elapsed: {time}s");//1270 shipinfo  33888->45856 Hardpoint(Минус 29 как выяснилось дубликаты (например Mantis два огня разных на одном разъёме HpRunningLight01))
            //3757 errors addon 
            Assert.IsTrue(FLGameDataService.Model.ShipInfoList.Count == 1270, $"ShipInfoList: {FLGameDataService.Model.ShipInfoList.Count}");
            Assert.IsTrue(FLGameDataService.Model.HardPointList.Count == 45827, $"HardPointList: {FLGameDataService.Model.HardPointList.Count}");
        }
    }
}
