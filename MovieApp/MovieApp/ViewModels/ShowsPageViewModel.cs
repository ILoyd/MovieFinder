using MovieApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Template10.Mvvm;

namespace MovieApp.ViewModels
{
    public class ShowsPageViewModel : ViewModelBase
    {
        protected readonly string imagePath = "https://image.tmdb.org/t/p/w500";

        protected Page _page;
        public virtual Page Page
        {
            get { return _page; }
            set
            {
                Set(ref _page, value);
            }
        }

        /// <summary>
        /// Az oldal adatainak formázása, képek helyettesítése placeholderekkel.
        /// </summary>
        protected void FormatChanged()
        {
            foreach (var item in Page.results)
            {
                if (!string.IsNullOrEmpty(item.poster_path))
                    item.poster_path = imagePath + item.poster_path;
                else
                    item.poster_path = "https://d32qys9a6wm9no.cloudfront.net/images/movies/poster/original.png";
            }
        }
        protected virtual void Load(PageHeader ph = null) { }

        /// <summary>
        /// Navigálás a film részletes oldalára.
        /// </summary>
        /// <param name="showId">A film egyedi azonosítója.</param>
        public void NavigateToMovieDetails(int showId)
        {
            NavigationService.Navigate(typeof(Views.DetailedMoviePage), new PageHeader() { ShowId = showId });
        }

        /// <summary>
        /// Navigálás a sorozat részletes oldalára.
        /// </summary>
        /// <param name="showId">A sorozat egyedi azonosítója.</param>
        public void NavigateToSeriesDetails(int showId)
        {
            NavigationService.Navigate(typeof(Views.DetailedSeriesPage), new PageHeader() { ShowId = showId });
        }

    }
}
