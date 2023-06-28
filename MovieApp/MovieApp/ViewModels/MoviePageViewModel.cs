using MovieApp.Models;
using MovieApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Template10.Mvvm;
using Windows.UI.Xaml.Navigation;

namespace MovieApp.ViewModels
{
    public class MoviePageViewModel : ShowsPageViewModel
    {

       public DelegateCommand<string> NextPageCommand { get; }
       public DelegateCommand<string> PrevPageCommand { get; }

        private string _sortType;
        public string SortType {
            get { return _sortType; }
            set { Set(ref _sortType, value); }
        }

       public override Page Page
        {
            get { return base.Page; }
            set
            {
                Set(ref _page, value);
                NextPageCommand.RaiseCanExecuteChanged();
                PrevPageCommand.RaiseCanExecuteChanged();
            }
        }

        public MoviePageViewModel()
        {
            Load(new PageHeader() { PageNumber=1});
            NextPageCommand = new DelegateCommand<string>(GetNextPage,CanGetNextPage);
            PrevPageCommand = new DelegateCommand<string>(GetPrevPage,CanGetPrevPage);
        }

        /// <summary>
        /// A filmeket tartalmazó oldal betöltése.
        /// <param name="ph">Az oldal paraméterei.</param>
        /// </summary>
        protected override async void Load(PageHeader ph)
        {
            var service = new MovieService();

            var response = await service.GetPopularMoviesAsync(ph.PageNumber);
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
        /// Az előző oldal betöltése.
        /// <param name="searchKey">Az keresési kulcs.</param>
        /// </summary>
        private void GetPrevPage(string searchKey)
        {
            if (!string.IsNullOrEmpty(searchKey))
                Search(new PageHeader() { SearchKey = searchKey, PageNumber = Page.page - 1 });
            else
                Load(new PageHeader() { PageNumber=Page.page - 1 });
        }

        /// <summary>
        /// Az következő oldal betöltése.
        /// </summary>
        /// <param name="searchKey">Az keresési kulcs.</param>
        private void GetNextPage(string searchKey)
        {
            if (!string.IsNullOrEmpty(searchKey))
                Search(new PageHeader() { SearchKey = searchKey, PageNumber = Page.page + 1 });
            else
                Load(new PageHeader() { PageNumber = Page.page + 1 });
        }

        /// <summary>
        /// Megadja, hogy betölthető-e a következő oldal.
        /// </summary>
        /// <param name="parameter">Az keresési kulcs.</param>
        /// <returns>Igaz, ha betölthető. Hamis ,ha nem.</returns>
        private bool CanGetNextPage(string parameter)
        {
            return Page != null && Page.page != 500 && Page.page!=Page.total_pages ? true : false;
        }

        /// <summary>
        /// Megadja, hogy betölthető-e az előző oldal.
        /// </summary>
        /// <param name="parameter">Az keresési kulcs.</param>
        /// <returns>Igaz, ha betölthető. Hamis ,ha nem.</returns>
        private bool CanGetPrevPage(string parameter)
        {
            return Page != null && Page.page != 1 ? true : false;
        }

        /// <summary>
        /// Keresés a filmek között.
        /// <param name="ph">Az oldal paraméterei.</param>
        /// </summary>
        public virtual async void Search(PageHeader ph)
        {
            if (!string.IsNullOrEmpty(ph.SearchKey))
            {
                var service = new MovieService();

                var response = await service.GetSearchedMoviesAsync(ph);
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
            else if(Page == null)
                Load(new PageHeader() { PageNumber = 1 });
        }
    }
}
