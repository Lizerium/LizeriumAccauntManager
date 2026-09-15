/*
 * Author: Nikolay Dvurechensky
 * Site: https://dvurechensky.pro/
 * Gmail: dvurechenskysoft@gmail.com
 * Last Updated: 15 сентября 2026 07:37:41
 * Version: 1.0.164
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
