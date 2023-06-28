using MovieApp.Models;
using MovieApp.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Template10.Mvvm;
using Windows.UI.Xaml.Navigation;

namespace MovieApp.ViewModels
{
    public class DetailedSeriesPageViewModel : ViewModelBase
    {
        private readonly string imagePath = "https://image.tmdb.org/t/p/w500/";

        private string errorMessage;
        public string ErrorMessage
        {
            get { return errorMessage; }
            set { Set(ref errorMessage, value); }
        }

        private Series _series;
        public Series Series
        {
            get { return _series; }
            set { Set(ref _series, value); }
        }

        private Episode _episode;
        public Episode Episode
        {
            get { return _episode; }
            set { Set(ref _episode, value); }
        }
        

        public ObservableCollection<string> Seasons { get; set; } = new ObservableCollection<string>();
        public ObservableCollection<Episode> Episodes { get; set; } = new ObservableCollection<Episode>();

        public override async Task OnNavigatedToAsync(
            object parameter, NavigationMode mode, IDictionary<string, object> state)
        {
            var pageHeader = parameter as PageHeader;
            var service = new MovieService();
            var response = await service.GetSeriesDetailsAsync(pageHeader.ShowId);
            if (response.StatusCode != System.Net.HttpStatusCode.OK)
            {
                Series = null;
                ErrorMessage = "Error message: " + response.StatusCode;
            }
            else
            {
                Series = response.Data;
                ErrorMessage = "";
                SeriesFormatChange();
                GetSeasons();
            }
            await base.OnNavigatedToAsync(parameter, mode, state);
        }

        /// <summary>
        /// A sorozat évadinak listáját tölti fel.
        /// </summary>
        private void GetSeasons()
        {
            foreach(var item in Series.seasons)
            {
                Seasons.Add("Season " + item.season_number);
            }
        }

        /// <summary>
        /// A sorozat adott évadjának részeit kéri el.
        /// </summary>
        /// <param name="seasonNumber">Az évad sorszáma.</param>
        public async void GetEpisodes(int seasonNumber)
        {
            var service = new MovieService();
            Episodes.Clear();
            var response= await service.GetEpisodesAsync(Series.id,seasonNumber);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                ErrorMessage = "";
                var eps = response.Data.episodes;
                foreach (var item in eps)
                {
                    Episodes.Add(item);
                    if (!string.IsNullOrEmpty(item.still_path))
                        item.still_path = imagePath + item.still_path;
                    else
                        item.still_path = "https://d32qys9a6wm9no.cloudfront.net/images/movies/poster/original.png";
                }
            }
            else
                ErrorMessage = "Error message: " + response.StatusCode;
        }

        /// <summary>
        /// A sorozat adatainak formázása, képek helyettesítése placeholderekkel.
        /// </summary>
        private void SeriesFormatChange()
        {
            if (!string.IsNullOrEmpty(Series.poster_path))
                Series.poster_path = imagePath + Series.poster_path;
            else
                Series.poster_path = "https://d32qys9a6wm9no.cloudfront.net/images/movies/poster/original.png";

            if (!string.IsNullOrEmpty(Series.backdrop_path))
                Series.backdrop_path = imagePath + Series.backdrop_path;
            else
                Series.backdrop_path = null;
        }
    }
}
