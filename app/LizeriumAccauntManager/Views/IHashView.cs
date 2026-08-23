/*
 * Author: Nikolay Dvurechensky
 * Site: https://dvurechensky.pro/
 * Gmail: dvurechenskysoft@gmail.com
 * Last Updated: 23 августа 2026 06:53:14
 * Version: 1.0.141
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
