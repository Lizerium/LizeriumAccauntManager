/*
 * Author: Nikolay Dvurechensky
 * Site: https://dvurechensky.pro/
 * Gmail: dvurechenskysoft@gmail.com
 * Last Updated: 25 мая 2026 11:13:25
 * Version: 1.0.51
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
