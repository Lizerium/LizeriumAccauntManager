/*
 * Author: Nikolay Dvurechensky
 * Site: https://dvurechensky.pro/
 * Gmail: dvurechenskysoft@gmail.com
 * Last Updated: 17 апреля 2026 06:52:03
 * Version: 1.0.13
 */

using System.Linq;
using System.Windows.Forms;

using Root.Components;
using Root.Services;

namespace Root.Presenters
{
    public class BannedPlayersPresenter
    {
        private readonly IBannedPlayersView view;
        private readonly IBanService banService;
        private readonly DataModel dataSet;
        private readonly MainPresenter mainPresenter;

        public BannedPlayersPresenter(IBannedPlayersView view, 
            IBanService banService, DataModel model, MainPresenter mainPresenter)
        {
            this.view = view;
            this.banService = banService;
            this.mainPresenter = mainPresenter;
            this.dataSet = model;

            view.OnAccountSelected += HandleAccountSelected;
            view.OnAccountSelectedForDetails += View_OnAccountSelectedForDetails;
            view.UnbanAction += View_UnbanAction;
            view.BanAction += View_BanAction;

            view.SetData(dataSet.DataSetPlayerInfo.BanList);
        }

        private void View_BanAction(string accDir)
        {
            BanItem banRecord = dataSet.DataSetPlayerInfo.BanList
                .FirstOrDefault(b => b.AccDir == accDir);
            if (banRecord != null)
            {
                new CreateBanWindow(banService, banRecord.AccDir, banRecord.AccID, dataSet, banRecord)
                    .ShowDialog();
                view.UpdateAccInfo();
            }
        }

        private void View_UnbanAction(string accDir)
        {
            banService.UnbanAccount(accDir);
        }

        private void View_OnAccountSelectedForDetails(string accDir)
        {
            // Фильтруем список CharacterList по AccDir и IsDeleted == false
            var charRecords = dataSet.DataSetPlayerInfo.CharacterList
                .Where(c => c.AccDir == accDir && c.IsDeleted == false)
                .ToList();
            string charStr = string.Join(", ",
                charRecords.Select(c => c.CharName));
            view.UpdateOnAccauntInfoFinaly(charStr);
        }

        private void HandleAccountSelected(string accDir)
        {
            this.mainPresenter.FilterOnAccDir(accDir);
        }

        public void Show() => view.Show();

        public void BringToFront() => view.BringToFront();

        public void HighlightRecord(string accID) => view.HighlightRecord(accID);

        public bool IsDisposed => (view as Form)?.IsDisposed ?? false;
    }
}
