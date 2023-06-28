using MovieApp.Models;
using MovieApp.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace MovieApp.Views
{

    public sealed partial class MoviesPage : Windows.UI.Xaml.Controls.Page
    {


        public MoviePageViewModel MoviesViewModel
        {
            get { return moviesViewModel; }
        }

        public MoviesPage()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Egy filmre való kattintás eseménykezelője.
        /// </summary>
        /// <param name="sender">A küldő objektum.</param>
        /// <param name="e">Az kattintás argumentumai.</param>
        private void MovieDetails_ItemClick(object sender, ItemClickEventArgs e)
        {
            var showHeader = (ShowHeader)e.ClickedItem;
            moviesViewModel.NavigateToMovieDetails(showHeader.id);
        }

        /// <summary>
        /// A keresés gomb megnyomásának eseménykezelője.
        /// </summary>
        /// <param name="sender">A küldő objektum.</param>
        /// <param name="e">Az kattintás argumentumai.</param>
        private void SearchMovies_Click(object sender, RoutedEventArgs e)
        {
            moviesViewModel.Search(new PageHeader() { SearchKey=SearchKey.Text.ToString().ToLower(), PageNumber=1 });
        }
    }
}
