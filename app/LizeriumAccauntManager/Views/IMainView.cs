/*
 * Author: Nikolay Dvurechensky
 * Site: https://dvurechensky.pro/
 * Gmail: dvurechenskysoft@gmail.com
 * Last Updated: 24 апреля 2026 06:52:32
 * Version: 1.0.20
 */

using System;
using System.Collections.Generic;
using System.Windows.Forms;

using Root.Components;
using Root.Services;

namespace Root.App
{
    public interface IMainView : ILogRecorder
    {
        string GetCharRecordBySelectedRow();
        void ResetPlayerInfoUI();
        void EnableSimpleElements(bool state);
        void EnableCharIsNotDeleteElements();
        void SetupBanStates();
        void SetPlayerInfoBasic(CharUIInfo info);
        void SetIPInfo(string info);
        void SetupStateCheckFile(bool state);
        void SetupKills(string kills);
        void SetupShipName(string shipName);
        void SetupOnline(string online);
        void SetupKickPlayerState(bool state);
        void SetupAffiliation(string affiliation);
        void SortFactions();
        void SortCargo();
        void SortEquipment();
        void ShowMessage(string message);
        void SelectedTabControlChanged();

        void FilterUpdateState(bool state);
        bool GetStateCheckBoxFilterDeleted();
        string GetTextBoxFilterText();
        bool GetStateCheckBoxFilterSameAccount();
        bool GetStateCheckBoxFilterSameIP();
        bool GetStateCheckBoxFilterSameLoginID();
        void SetupCharacterListBindingSource(List<CharacterItem> characters);
        void SetupOldCharacterItem();

        void SetupMenuState(bool state);
        void SetupRescanAccountFilesToolStripState(bool state);
        void ReportProgress(int progressPercentage, string userState);
        void SetupTimerPeriodicTasksState(bool state);
        void SetupTimerDBSave(bool state);
        void CompleteLoadUIEvent();
        void SetupTimerShutdownState(bool state);
        void SetupUpdatePlayerInfoTimerState(bool state);
        void SetupWaitCursor();
        void RestoreCursor();
        void FilterOnAccDir(string accDir);
        void SetupHookStateText(string text);
        void SetupDisplayDBText(string text);
        void ResetCharListEvent();

        /// <summary>
        /// Открытие окна свойств
        /// </summary>
        void ShowProperties();
        /// <summary>
        /// Подтверждение выхода
        /// </summary>
        /// <returns>DialogResult</returns>
        DialogResult ConfirmClose(string text, string caption);
        void Close();

        /// <summary>
        /// Делегат загрузки формы
        /// </summary>
        event EventHandler ViewLoadEvent;
        event EventHandler RescanAccountFilesEvent;
        event EventHandler UpdatePlayerInfoEvent;
        event EventHandler TestFLHookConnectEvent;
        event EventHandler DBSaveEvent;
        event EventHandler ReloadGameDataEvent;
    }
}
