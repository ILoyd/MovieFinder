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
    public class DetailedMoviePageViewModel : ViewModelBase
    {
        private readonly string imagePath = "https://image.tmdb.org/t/p/w500";
        private Movie _movie;

        private string errorMessage;
        public string ErrorMessage
        {
            get { return errorMessage; }
            set { Set(ref errorMessage, value); }
        }

        public Movie Movie
        { get { return _movie; }
            set { Set(ref _movie, value); } 
        }

        private List<Cast> _cast;
        public List<Cast> Cast 
        {
            get { return _cast; }
            set { Set(ref _cast, value); }
        }

        public override async Task OnNavigatedToAsync(
            object parameter, NavigationMode mode, IDictionary<string, object> state)
        {
            var pageHeader = parameter as PageHeader;
            var service = new MovieService();

            var movieResponse = await service.GetMovieDetailsAsync(pageHeader.ShowId);
            Movie = movieResponse.Data;
            var castResponse = await service.GetCastAsync(pageHeader.ShowId);
            Cast = castResponse.Data.cast;

            if (movieResponse.StatusCode == System.Net.HttpStatusCode.OK && castResponse.StatusCode == System.Net.HttpStatusCode.OK)
            {
                ErrorMessage = "";
                MovieFormatChange();
            }
            else 
            {
                ErrorMessage = "Error message: " + movieResponse.StatusCode;
                Movie = null;
                Cast = null;
            }

            await base.OnNavigatedToAsync(parameter, mode, state);
        }

        /// <summary>
        /// Navigálás a szereplő részletes oldalára.
        /// </summary>
        /// <param name="cast">Az szereplő.</param>
        public void NavigateToPerson(Cast cast)
        {
            NavigationService.Navigate(typeof(Views.PersonPage), cast.id);
        }

        /// <summary>
        /// A film adatainak formázása, képek helyettesítése placeholderekkel.
        /// </summary>
        private void MovieFormatChange()
        {
            if (!string.IsNullOrEmpty(Movie.poster_path))
                Movie.poster_path = imagePath + Movie.poster_path;
            else
                Movie.poster_path = "https://d32qys9a6wm9no.cloudfront.net/images/movies/poster/original.png";

            if (!string.IsNullOrEmpty(Movie.backdrop_path))
                Movie.backdrop_path = imagePath + Movie.backdrop_path;
            else
                Movie.backdrop_path = null;

            foreach (var item in Cast)
            {
                if (!string.IsNullOrEmpty(item.profile_path))
                    item.profile_path = imagePath + item.profile_path;
                else
                    item.profile_path = "https://ongaropc.com/wp-content/uploads/2023/03/avatar-placeholder.jpg";
            }
        }
    }
}
