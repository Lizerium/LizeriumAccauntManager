/*
 * Author: Nikolay Dvurechensky
 * Site: https://dvurechensky.pro/
 * Gmail: dvurechenskysoft@gmail.com
 * Last Updated: 16 апреля 2026 11:44:04
 * Version: 1.0.11
 */

using System.Linq;
using System.Windows.Forms;

using Root.Components;
using Root.Services;
using Root.Views;

namespace Root.Presenters
{
    public class HashPresenter
    {
        private readonly IHashView view;
        /// <summary>
        /// Служба доступа к базе данных.
        /// </summary>
        private IFLGameDataService FLGameDataService { get; set; }

        public HashPresenter(IHashView view,
            IFLGameDataService fLGameDataService)
        {
            this.view = view;
            this.FLGameDataService = fLGameDataService;

            view.SetHashList(FLGameDataService.Model);

            view.OnRowSelected += View_OnRowSelected;
            view.OnFilterChanged += View_OnFilterChanged;
        }

        private void View_OnFilterChanged(object sender, string text)
        {
            var filterText = FLUtility.EscapeLikeExpressionString(text);

            if (string.IsNullOrWhiteSpace(filterText))
            {
                view.ApplyFilter(null);
                return;
            }

            var searchHashes = FLGameDataService.Model.HashListItems.Where(it => it.IDSName.Contains(filterText)
                || it.ItemNickName.Contains(filterText)
                || it.IDSInfo.Contains(filterText)).ToList();

            view.ApplyFilter(searchHashes);
        }

        private void View_OnRowSelected(SelectedStatData data)
        {
            string rtf = "";

            if (data.ItemType == FLGameDataService.GAMEDATA_SHIPS)
            {
                rtf += FLUtility.FLXmlToRtf(data.IDSInfo1);
                rtf += "\\pard \\par ";
                rtf += FLUtility.FLXmlToRtf(data.IDSInfo);
            }
            else if (data.ItemType == FLGameDataService.GAMEDATA_BASES)
            {
                rtf += FLUtility.FLXmlToRtf(data.IDSInfo);
                rtf += "\\pard \\par ";
                rtf += FLUtility.FLXmlToRtf(data.IDSInfo1);
                rtf += "\\pard \\par ";
                rtf += FLUtility.FLXmlToRtf(data.IDSInfo2);
                rtf += "\\pard \\par ";
                rtf += FLUtility.FLXmlToRtf(data.IDSInfo3);
            }
            else
            {
                string xml = data.IDSInfo ?? "No information available";
                rtf += FLUtility.FLXmlToRtf(xml);
            }

            view.SetInfoRtf(rtf);
        }

        public void Show() => view.Show();
        public void BringToFront() => view.BringToFront();
        public bool IsDisposed => (view as Form)?.IsDisposed ?? false;
    }
}
