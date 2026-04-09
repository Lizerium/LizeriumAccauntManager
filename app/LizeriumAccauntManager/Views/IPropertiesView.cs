/*
 * Author: Nikolay Dvurechensky
 * Site: https://dvurechensky.pro/
 * Gmail: dvurechenskysoft@gmail.com
 * Last Updated: 09 апреля 2026 10:58:05
 * Version: 1.0.3
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
