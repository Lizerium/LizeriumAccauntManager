/*
 * Author: Nikolay Dvurechensky
 * Site: https://dvurechensky.pro/
 * Gmail: dvurechenskysoft@gmail.com
 * Last Updated: 25 апреля 2026 08:11:28
 * Version: 1.0.21
 */

using System;
using System.Windows.Forms;

using Root.Services;
using Root.Views;
using System.Threading.Tasks;

namespace Root.Presenters
{
    public class StatisticsPresenter
    {
        private readonly IStatisticsView view;
        /// <summary>
        /// Служба доступа к базе данных.
        /// </summary>
        private IDataBaseService DataBaseService { get; set; }

        public StatisticsPresenter(IStatisticsView view,
            IDataBaseService dataBaseService)
        {
            this.view = view;
            this.DataBaseService = dataBaseService;

            view.OnRefreshRequested += async (s, e) => await LoadStatisticsAsync();
        }

        public async Task LoadStatisticsAsync()
        {
            try
            {
                view.SetLoadingState(true);
                var stats = await DataBaseService.GetGeneralStatisticsTable();
                view.SetStatistics(stats);
            }
            catch (Exception ex)
            {
                view.ShowMessage($"Error loading statistics:\n{ex.Message}", "Error");
            }
            finally
            {
                view.SetLoadingState(false);
            }
        }

        public void Show() => view.Show();
        public void BringToFront() => view.BringToFront();
        public bool IsDisposed => (view as Form)?.IsDisposed ?? false;
    }
}
