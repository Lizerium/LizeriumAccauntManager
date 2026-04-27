/*
 * Author: Nikolay Dvurechensky
 * Site: https://dvurechensky.pro/
 * Gmail: dvurechenskysoft@gmail.com
 * Last Updated: 27 апреля 2026 09:41:57
 * Version: 1.0.23
 */

using System;
using System.Collections.Generic;

using Root.Components;

namespace Root.Views
{
    public interface IHashView
    {
        event Action<SelectedStatData> OnRowSelected;
        event EventHandler<string> OnFilterChanged;
        void SetHashList(DataModel model);
        void SetInfoRtf(string rtf);
        void ApplyFilter(List<HashListItem> expression);
        void Show();
        void BringToFront();
    }
}
