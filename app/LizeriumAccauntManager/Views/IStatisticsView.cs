/*
 * Author: Nikolay Dvurechensky
 * Site: https://dvurechensky.pro/
 * Gmail: dvurechenskysoft@gmail.com
 * Last Updated: 03 ноября 2026 12:19:42
 * Version: 1.0.212
 */

using System;
using System.Collections.Generic;

using Root.Components;

namespace Root.Views
{
    public interface IStatisticsView
    {
        event EventHandler OnRefreshRequested;
        void BringToFront();
        void SetLoadingState(bool isLoading);
        void SetStatistics(List<GeneralStatistics> stats);
        void Show();
        void ShowMessage(string text, string title);
    }
}
