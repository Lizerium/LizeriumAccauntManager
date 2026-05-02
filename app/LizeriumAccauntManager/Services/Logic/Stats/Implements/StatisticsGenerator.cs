/*
 * Author: Nikolay Dvurechensky
 * Site: https://dvurechensky.pro/
 * Gmail: dvurechenskysoft@gmail.com
 * Last Updated: 02 мая 2026 19:17:21
 * Version: 1.0.28
 */

using System;
using System.Collections.Generic;
using System.IO;
using System.Data;
using System.ComponentModel;
using System.Text.RegularExpressions;
using Root.Components;
using System.Linq;
using System.Threading.Tasks;
using Root.Services;
using System.Threading;

namespace Root.Services
{
    class StatisticsGenerator : IDisposable
    {
        IDataBaseService dataBase;
        IFLHookSocketService m_flHookCmdr;
        IFLGameDataService m_gameData;

        public StatisticsGenerator(IFLGameDataService gameData, IFLHookSocketService flHookCmdr)
        {
            dataBase = new DataBaseService();
            m_gameData = gameData;
            m_flHookCmdr = flHookCmdr;
        }

        public void Dispose()
        {
            dataBase.Dispose();
        }

        public void GenerateGeneralStatistics(IProgress<ProgressInfo> progress, ILogRecorder log)
        {
            GenerateOnlinePlayerStats(log);

            DateTime date = DateTime.Now;
            try
            {
                string filePath = String.Format("{0}\\general_{1:yyyyMMdd}.html", AppSettings.Default.setStatisticsDir, date);
                if (File.Exists(filePath))
                    return;

                progress.Report(new(60, "Creating general statistics..."));
                var stats = dataBase.Model.GeneralStatisticsList;

                progress.Report(new(70, "Generating general reports..."));
                string[] colTitles = new string[] { "Description", "Result" };
                string[] colNames = new string[] { "Description", "Result" };
                string title = String.Format("General Statistics - {0}", date);
                SaveAsHTML(filePath, title, stats, colTitles, colNames);
                File.Copy(filePath, AppSettings.Default.setStatisticsDir + "\\general.html", true);
            }
            catch (Exception ex)
            {
                log.AddLog(String.Format("Error '{0}' when calculating general statistics", ex.Message));
            }
        }

        /// <summary>
        /// generate online statistics for online players ...
        /// </summary>
        private void GenerateOnlinePlayerStats(ILogRecorder log)
        {
            //***** assemble the sorted lists for both tables ...
            SortedDictionary<string, string> char_list = new SortedDictionary<string, string>();
            SortedDictionary<string, List<string>> system_list = new SortedDictionary<string, List<string>>();
            int slots_in_use = -1;
            lock (m_flHookCmdr.PlayerInfoList)
            {
                slots_in_use = m_flHookCmdr.PlayerInfoList.Count;
                //***** store the hook characters information into both lists ...
                foreach (KeyValuePair<int, FLHookSocketService.PlayerInfo> kvp in m_flHookCmdr.PlayerInfoList)
                {
                    string character = StringToSafeHTML(kvp.Value.charname);
                    if (character != "-")
                    {
                       string system = StringToSafeHTML(m_gameData.GetItemDescByNickNameX(kvp.Value.system));
                       char_list.Add(character, system);
                       if (!system_list.ContainsKey(system))
                       {
                          system_list.Add(system, new List<string>());
                       }
                       system_list[system].Add(character);
                    }
                }
            }
            //*****   generate the html contents ...
            string contents = "<html><head><title>Players Online</title><style type=text/css>"; ;
            contents += ".Column0 {FONT-FAMILY: Tahoma; FONT-SIZE: 10pt;  TEXT-ALIGN: left; COLOR: #000000; BACKGROUND: #FFFFFF;}";
            contents += ".Column1 {FONT-FAMILY: Tahoma; FONT-SIZE: 10pt;  TEXT-ALIGN: left; COLOR: #000000; BACKGROUND: #FFFFFF;}";
            contents += "</style>";
            contents += "</head><body>";
            contents += "<i>last update: " + DateTime.UtcNow.ToString() + " [ZTC]</i><br><br>";
            contents += "<i>used slots: " + String.Format("{0}", slots_in_use) + " </i><br><br><br><br>";
            contents += "<size=5><b><u>Characters by Name</u></b></size><br><br>";
            contents += "<table width=\"90%\" border=\"1\" cellspacing=\"0\" cellpadding=\"0\">";
            contents += "<tr><th bgcolor=\"#ECE9D8\" align=\"left\"><font face=\"Tahoma\" color=\"#000000\" size=\"2\">Character</font></th><th bgcolor=\"#ECE9D8\" align=\"left\"><font face=\"Tahoma\" color=\"#000000\" size=\"2\">System</font></th></tr>";
            foreach (KeyValuePair<string, string> kvp in char_list)
            {
                contents += "<tr><td class=\"column0\">" + kvp.Key + "</td><td class=\"column1\">" + kvp.Value + "</td></tr>";
            }
            contents += "</table><br><br><br><br>";
            contents += "<size=5><b><u>Characters by System</u></b></size><br><br>";
            contents += "<table width=\"90%\" border=\"1\" cellspacing=\"0\" cellpadding=\"0\">";
            contents += "<tr><th bgcolor=\"#ECE9D8\" align=\"left\"><font face=\"Tahoma\" color=\"#000000\" size=\"2\">Character</font></th><th bgcolor=\"#ECE9D8\" align=\"left\"><font face=\"Tahoma\" color=\"#000000\" size=\"2\">System</font></th></tr>";
            foreach (KeyValuePair<string, List<string>> kvp in system_list)
            {
               kvp.Value.Sort();
                foreach (string character in kvp.Value)
                {
                    contents += "<tr><td class=\"column0\">" + character + "</td><td class=\"column1\">" + kvp.Key + "</td></tr>";
                }
            }
            contents += "</table></body></html>";
            //*****   open the stream and write the contents ...
            String online_players_file = String.Format("{0}\\players_online.html", AppSettings.Default.setStatisticsDir);
            StreamWriter writer = new StreamWriter(online_players_file);
            try
            {
                writer.Write(contents);

            }
            catch (Exception ex)
            {
                log.AddLog(String.Format("Error '{0}'in GenerateOnlinePlayerStats", ex.Message));
            }
            finally
            {
                writer.Close();
            }
        }

        /// <summary>
        /// Generate player stats in a format similar to FLStat.
        /// </summary>
        /// <param name="progress"></param>
        /// <param name="log"></param>
        public void GeneratePlayerStats(IProgress<ProgressInfo> progress, ILogRecorder log)
        {
            DateTime date = DateTime.Now;
            try
            {
                string filePath = String.Format("{0}\\players_{1:yyyyMMdd}.html", AppSettings.Default.setStatisticsDir, date);
                if (File.Exists(filePath))
                    return;

                var chars = dataBase.Model.DataSetPlayerInfo.CharacterList;

                progress.Report(new(75, "Generating player reports..."));
                //DataRow[] rows = ds.CharacterList.Select("IsDeleted = 'false'");
                List<StatPlayer> players = new List<StatPlayer>();

                foreach (var cr in chars)
                {
                    if (!cr.IsDeleted)
                    {
                        players.Add(new StatPlayer
                        {
                            Name = cr.CharName,
                            Rank = cr.Rank,
                            Money = cr.Money,
                            LastOnLine = cr.LastOnLine,
                            OnLineTime = TimeSpan.FromSeconds(cr.OnLineSecs).ToString(@"hh\:mm\:ss")
                        });
                    }
                }

                // Сортировка по убыванию Rank
                var topPlayers = players.OrderByDescending(p => p.Rank).Take(2500).ToList();
                string[] colTitles = new string[] { "Name", "Rank", "Money", "Last On Line", "On Line Time" };
                string[] colNames = new string[] { "Name", "Rank", "Money", "LastOnLine", "OnLineTime" };
                string title = String.Format("Players - {0}", date);
                SaveAsHTML(filePath, title, topPlayers, colTitles, colNames);
                File.Copy(filePath, AppSettings.Default.setStatisticsDir + "\\players.html", true);
            }
            catch (Exception ex)
            {
                log.AddLog(String.Format("Error '{0}' when calculating player statistics", ex.Message));
            }
        }

        /// <summary>
        /// Рассчитать и сформировать статистику для фракций.
        /// </summary>
        /// <param name="progress"></param>
        /// <param name="token"></param>
        /// <param name="log"></param>
        public async void GenerateFactionActivity(IProgress<ProgressInfo> progress, CancellationToken token, 
            ILogRecorder log)
        {
            DateTime endDate = DateTime.Today;
            DateTime startDate = endDate.AddDays(-30);

            if (string.IsNullOrWhiteSpace(AppSettings.Default.setStatisticsDir))
                return;

            string filePath = Path.Combine(AppSettings.Default.setStatisticsDir, $"activity_summary_{endDate:yyyyMMdd}.html");
            if (File.Exists(filePath))
                return;

            try
            {
                progress.Report(new(80, "Creating activity statistics..."));

                List<StatPlayer> results = await CalcActivity(startDate, endDate, progress, token, log);
                if (results == null)
                    return;

                List<StatPlayer> summaryFactions = new List<StatPlayer>();

                // Общая активность
                string title = $"Activity - {(endDate - startDate).Days} days to {endDate:yyyy-MM-dd}";
                GenerateActivityReport(results, "", title, summaryFactions, endDate, showChangedOnlineTimeOnly: true);

                // Статистика по фракциям
                progress.Report(new(90, "Generating activity reports..."));
                string[] factionsFilter = AppSettings.Default.setStatsFactions.Split(' ', (char)StringSplitOptions.RemoveEmptyEntries);

                foreach (string faction in factionsFilter)
                {
                    string factionTitle = $"Activity {faction} - {endDate:yyyy-MM-dd}";
                    GenerateActivityReport(results, faction, factionTitle, summaryFactions, DateTime.MinValue, showChangedOnlineTimeOnly: false);
                }

                // Сохраняем summary как HTML
                string summaryTitle = $"Activity Summary - {(endDate - startDate).Days} days to {endDate:yyyy-MM-dd}";
                string summaryPath = Path.Combine(AppSettings.Default.setStatisticsDir, $"activity_summary_{endDate:yyyyMMdd}.html");

                SaveAsHTML(summaryPath, summaryTitle, summaryFactions, new[] { "Name", "On Line Time" }, new[] { "Name", "OnLineTime" });
                File.Copy(summaryPath, Path.Combine(AppSettings.Default.setStatisticsDir, "activity_summary.html"), true);
            }
            catch (Exception ex)
            {
                log.AddLog($"Error '{ex.Message}' when generating HTML");
                try { File.Delete(filePath); } catch { }
            }
        }

        /// <summary>
        /// Просмотрите исторические списки персонажей и составьте список, содержащий время, 
        /// в течение которого каждый игрок находился в сети в течение указанного периода времени.
        /// </summary>
        /// <returns>Возвращает путь к HTML-файлу cot</returns>
        private async Task<List<StatPlayer>> CalcActivity(DateTime startDate, DateTime endDate, 
            IProgress<ProgressInfo> progress, CancellationToken token, ILogRecorder log)
        {
            List<StatPlayer> results = new List<StatPlayer>();
            try
            {
                // Ведите две таблицы: одну с записями на момент их первого обнаружения,
                // а другую с самыми последними данными.
                DataModel oldestData = new DataModel();
                DataModel newestData = new DataModel();
                Dictionary<string, uint> onLineTimes = new Dictionary<string, uint>();

                // Сканирование от самой старой даты до самой новой,
                // чтение списков персонажей из базы данных и построение списка времени в режиме онлайн.
                for (DateTime date = startDate; date <= endDate; date = date.AddDays(1))
                {
                    if (token.IsCancellationRequested)
                        return null;

                    if (await dataBase.GetCharacterHistory(date))
                    {
                        foreach (var tempCharRecord in dataBase.Model.DataSetPlayerInfo.CharacterList)
                        {
                            if (token.IsCancellationRequested)
                                return null;

                            // Запишите самые старые данные. Не обновляйте, если данные уже есть.
                            if (oldestData.DataSetPlayerInfo.CharacterList.Any(it => it.CharPath == tempCharRecord.CharPath) == false)
                            {
                                oldestData.DataSetPlayerInfo.CharacterList.Add(new CharacterItem()
                                {
                                    CharPath = tempCharRecord.CharPath,
                                    AccDir = tempCharRecord.AccDir,
                                    AccID = tempCharRecord.AccID,
                                    CharName = tempCharRecord.CharName,
                                    Updated = tempCharRecord.Updated,
                                    Created = tempCharRecord.Created,
                                    IsDeleted = tempCharRecord.IsDeleted,
                                    LastOnLine = tempCharRecord.LastOnLine,
                                    Location = tempCharRecord.Location,
                                    Money = tempCharRecord.Money,
                                    OnLineSecs = tempCharRecord.OnLineSecs,
                                    Rank = tempCharRecord.Rank,
                                    Ship = tempCharRecord.Ship
                                });
                            }

                            // Запишите самые новые данные. Продолжайте обновлять данные самыми последними записями.
                            // Рассчитайте количество секунд онлайна, чтобы справиться с ситуацией,
                            // когда персонаж был удален, а затем создан заново.
                            var charRecord = newestData.DataSetPlayerInfo.CharacterList.FirstOrDefault(it => it.CharPath == tempCharRecord.CharPath);
                            if (charRecord == null)
                            {
                                charRecord = new CharacterItem()
                                {
                                    CharPath = tempCharRecord.CharPath,
                                    AccDir = tempCharRecord.AccDir,
                                    AccID = tempCharRecord.AccID,
                                    CharName = tempCharRecord.CharName,
                                    Updated = tempCharRecord.Updated,
                                    Created = tempCharRecord.Created,
                                    IsDeleted = tempCharRecord.IsDeleted,
                                    LastOnLine = tempCharRecord.LastOnLine,
                                    Location = tempCharRecord.Location,
                                    Money = tempCharRecord.Money,
                                    OnLineSecs = tempCharRecord.OnLineSecs,
                                    Rank = tempCharRecord.Rank,
                                    Ship = tempCharRecord.Ship
                                };
                                newestData.DataSetPlayerInfo.CharacterList.Add(charRecord);
                            }
                            else
                            {
                                uint onLineTime = tempCharRecord.OnLineSecs - charRecord.OnLineSecs;
                                if (onLineTime < 0)
                                    onLineTime = 0;
                                if (onLineTimes.ContainsKey(charRecord.CharPath))
                                    onLineTimes[charRecord.CharPath] += onLineTime;
                                else
                                    onLineTimes[charRecord.CharPath] = onLineTime;

                                charRecord.Created = tempCharRecord.Created;
                                charRecord.Updated = tempCharRecord.Updated;
                                charRecord.IsDeleted = tempCharRecord.IsDeleted;
                                charRecord.Location = tempCharRecord.Location;
                                charRecord.Rank = tempCharRecord.Rank;
                                charRecord.Money = tempCharRecord.Money;
                                charRecord.Ship = tempCharRecord.Ship;
                                charRecord.OnLineSecs = tempCharRecord.OnLineSecs;
                                charRecord.LastOnLine = tempCharRecord.LastOnLine;
                            }
                        }
                    }
                }

                // Создайте набор данных результатов. Этот набор данных будет использоваться
                // для создания индивидуальных отчетов о деятельности.
                foreach (var newestCharRecord in newestData.DataSetPlayerInfo.CharacterList)
                {
                    if (token.IsCancellationRequested)
                        return null;

                    var oldestCharRecord = oldestData.DataSetPlayerInfo.CharacterList.FirstOrDefault(it => it.CharPath == newestCharRecord.CharPath);
                    if (oldestCharRecord != null)
                    {
                        uint rankDelta = newestCharRecord.Rank - oldestCharRecord.Rank;
                        uint moneyDelta = newestCharRecord.Money - oldestCharRecord.Money;
                        uint onLineSecsDelta = 0;
                        if (onLineTimes.ContainsKey(oldestCharRecord.CharPath))
                            onLineSecsDelta = onLineTimes[oldestCharRecord.CharPath];

                        results.Add(new StatPlayer()
                        {
                            Rank = rankDelta,
                            Money = moneyDelta, 
                            LastOnLine = oldestCharRecord.LastOnLine,
                            Name = newestCharRecord.CharName,
                            OnLineSecs = onLineSecsDelta
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                log.AddLog(String.Format("Error '{0}' when calculating activity statistics", ex.Message));
                return null;
            }

            return results;
        }


        /// <summary>
        /// Generate from the results dataset for the specified charNameFilter
        /// </summary>
        void GenerateActivityReport(List<StatPlayer> results, string charNameFilter, string title, List<StatPlayer> summaryFactions, DateTime date, bool showChangedOnlineTimeOnly)
        {
            string[] colTitles = new[] { "Name", "Last On Line", "On Line Time" };
            string[] colNames = new[] { "Name", "LastOnLine", "OnLineTime" };

            // Готовим путь к файлу
            string escapedName = Regex.Replace(charNameFilter, @"[?:\/*""<>|]", "");
            string filePath = (date > DateTime.MinValue)
                ? $"{AppSettings.Default.setStatisticsDir}\\activity_{date:yyyyMMdd}.html"
                : $"{AppSettings.Default.setStatisticsDir}\\activity_{escapedName}.html";

            // Применяем фильтрацию
            var filtered = results
                .Where(r =>
                    (!string.IsNullOrEmpty(charNameFilter) &&
                     (r.Name.StartsWith(charNameFilter, StringComparison.OrdinalIgnoreCase) ||
                      r.Name.Contains(charNameFilter)))
                    && (!showChangedOnlineTimeOnly || r.OnLineSecs > 0))
                .OrderByDescending(r => r.OnLineTime)
                .ToList();

            // Сохраняем как HTML
            SaveAsHTML(filePath, title, filtered, colTitles, colNames);

            // Обновляем сводку
            string linkName = filePath.Substring(AppSettings.Default.setStatisticsDir.Length + 1);
            linkName = $"<a href=\"{StringToSafeHTML(linkName)}\">{StringToSafeHTML(linkName)}</a>";

            TimeSpan totalOnline = TimeSpan.FromSeconds(filtered.Sum(r => r.OnLineSecs));
            summaryFactions.Add(new StatPlayer
            {
                Name = linkName,
                OnLineTime = totalOnline.ToString(),
                OnLineSecs = (uint)totalOnline.TotalSeconds,
                LastOnLine = DateTime.Now
            });
        }

        string StringToSafeHTML(string input)
        {
            return input.Replace("/&(?!\\w+([;\\s]|$))/g", "&amp;").Replace("/</g", "&lt;").Replace("/>/g", "&gt;");
        }

        /// <summary>
        /// Save the dataset in HTML format.
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="title"></param>
        /// <param name="rows"></param>
        /// <param name="colTitles"></param>
        /// <param name="colNames"></param>
        private void SaveAsHTML<T>(string filePath, string title, IEnumerable<T> items, string[] colTitles, string[] colNames)
        {
            using (StreamWriter wr = new StreamWriter(filePath))
            {
                wr.WriteLine($"<html><head><title>{StringToSafeHTML(title)}</title>");
                wr.WriteLine("<style type='text/css'>");
                for (int i = 0; i < colTitles.Length; i++)
                {
                    wr.WriteLine($".Column{i} {{ FONT-FAMILY: Tahoma; FONT-SIZE: 10pt; TEXT-ALIGN: left; COLOR: #000000; BACKGROUND: #FFFFFF; }}");
                }
                wr.WriteLine("</style>");
                wr.WriteLine("</head><body>");
                wr.WriteLine("<table width='90%' border='1' cellspacing='0' cellpadding='0'>");

                // Header
                wr.Write("<tr>");
                foreach (var colTitle in colTitles)
                {
                    wr.Write($"<th bgcolor='#ECE9D8' align='left'><font face='Tahoma' color='#000000' size='2'>{StringToSafeHTML(colTitle)}</font></th>");
                }
                wr.WriteLine("</tr>");

                // Body
                foreach (var item in items)
                {
                    wr.WriteLine("<tr>");
                    for (int i = 0; i < colNames.Length; i++)
                    {
                        var prop = typeof(T).GetProperty(colNames[i]);
                        var value = prop?.GetValue(item)?.ToString() ?? "";
                        wr.WriteLine($"<td class='Column{i}'>{StringToSafeHTML(value)}</td>");
                    }
                    wr.WriteLine("</tr>");
                }

                wr.WriteLine("</table></body></html>");
            }
        }
    }
}
