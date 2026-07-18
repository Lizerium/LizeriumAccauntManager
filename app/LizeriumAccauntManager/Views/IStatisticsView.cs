/*
 * Author: Nikolay Dvurechensky
 * Site: https://dvurechensky.pro/
 * Gmail: dvurechenskysoft@gmail.com
 * Last Updated: 18 июля 2026 07:27:53
 * Version: 1.0.105
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
