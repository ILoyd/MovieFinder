using MovieApp.Models;
using MovieApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Template10.Mvvm;

namespace MovieApp.ViewModels
{
    public class SeriesPageViewModel : MoviePageViewModel
    {

        /// <summary>
        /// A sorozatokat tartalmazó oldal betöltése.
        /// <param name="ph">Az oldal paraméterei.</param>
        /// </summary>
        protected override async void Load(PageHeader ph)
        {
            var service = new MovieService(); 
            var response = await service.GetPopularSeriesAsync(ph.PageNumber);
            if (response.StatusCode != System.Net.HttpStatusCode.OK)
            {
                Page = null;
                SortType = "Error message: " + response.StatusCode;
            }
            else
            {
                Page = response.Data;
                SortType = "Sorted by popularity";
                FormatChanged();
            }
        }

        /// <summary>
        /// Keresés a sorozatok között.
        /// <param name="ph">Az oldal paraméterei.</param>
        /// </summary>
        public override async void Search(PageHeader ph)
        {
            if (!string.IsNullOrEmpty(ph.SearchKey))
            {
                var service = new MovieService();

                var response = await service.GetSearchedSeriesAsync(ph);
                if (response.StatusCode != System.Net.HttpStatusCode.OK)
                {
                    Page = null;
                    SortType = "Error message: " + response.StatusCode.ToString();
                }
                else
                {
                    Page = response.Data;
                    SortType = "Sorted by popularity";
                    FormatChanged();
                }
            }
            else if (Page == null)
                Load(new PageHeader() { PageNumber = 1 });
        }
    }
}
