using MovieApp.Models;
using MovieApp.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Template10.Mvvm;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace MovieApp.ViewModels
{
    public class TrendingPageViewModel : ShowsPageViewModel
    {
        private string errorMessage;
        public string ErrorMessage
        {
            get { return errorMessage; }
            set { Set(ref errorMessage, value); }
        }

        public TrendingPageViewModel(PageHeader ph)
        {
            Load(ph);
        }

        /// <summary>
        /// A média típusának megváltoztatása és az oldal újratöltése.
        /// <param name="ph">Az oldal paraméterei.</param>
        /// </summary>
        public void ChangeMediaType(PageHeader ph)
        {
            Load(ph);
        }

        /// <summary>
        /// A trendingben lévő műsorokat tartalmazó oldal betöltése.
        /// <param name="ph">Az oldal paraméterei.</param>
        /// </summary>
        protected override async void Load(PageHeader ph)
        {
            var service = new MovieService();

            var response = await service.GetTrendingShowsAsync(ph);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                Page = response.Data;
                ErrorMessage = "";
                FormatChanged();
            }
            else
            {
                Page = null;
                ErrorMessage = "Error message: " + response.StatusCode;
            }
        }
    }
}
