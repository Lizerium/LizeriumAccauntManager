/*
 * Author: Nikolay Dvurechensky
 * Site: https://dvurechensky.pro/
 * Gmail: dvurechenskysoft@gmail.com
 * Last Updated: 13 июня 2026 14:01:35
 * Version: 1.0.70
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
