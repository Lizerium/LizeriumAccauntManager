/*
 * Author: Nikolay Dvurechensky
 * Site: https://dvurechensky.pro/
 * Gmail: dvurechenskysoft@gmail.com
 * Last Updated: 13 апреля 2026 12:59:47
 * Version: 1.0.7
 */

using System;
using System.Collections.Generic;

using Root.Components;

namespace Root
{
    public interface IBannedPlayersView
    {
        void Show();
        event Action<string> OnAccountSelected;
        event Action<string> OnAccountSelectedForDetails;
        event Action<string> UnbanAction;
        event Action<string> BanAction;
        void UpdateOnAccauntInfoFinaly(string charStr);
        void UpdateAccInfo();
        void SetData(IEnumerable<BanItem> banList);
        void BringToFront();
        void HighlightRecord(string accID);
    }
}
