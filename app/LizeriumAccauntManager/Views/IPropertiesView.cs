/*
 * Author: Nikolay Dvurechensky
 * Site: https://dvurechensky.pro/
 * Gmail: dvurechenskysoft@gmail.com
 * Last Updated: 04 мая 2026 06:53:07
 * Version: 1.0.30
 */

using System;

using Root.Components;

namespace Root.Views
{
    public interface IPropertiesView
    {
        void BringToFront();
        void Show();
        void InstallLoadData(PropsSaveRequest request);
        event EventHandler<PropsSaveRequest> SavePropsEvent;
        event Action LoadPropsEvent;
        event EventHandler ResetSettingsEvent;
    }
}
