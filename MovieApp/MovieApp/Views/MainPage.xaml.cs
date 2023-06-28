using MovieApp.Models;
using MovieApp.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Template10.Common;
using Template10.Services.NavigationService;
using Template10.Utils;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace MovieApp.Views
{
    public sealed partial class MainPage : Windows.UI.Xaml.Controls.Page
    {
        private PageHeader PageHeader;
        public MainPageViewModel MainViewModel;

        public MainPage()
        {
            PageHeader = new PageHeader()
            {
                MediaType = "movie",
                PageNumber = 1,
                DayOrWeek = "day"
            };
            MainViewModel = new MainPageViewModel();
            DataContext = MainViewModel;
            InitializeComponent();
        }

        /// <summary>
        /// Navigációban a kiválszotott elem változásának kezelése.
        /// </summary>
        /// <param name="sender">A küldő objektum.</param>
        /// <param name="e">A navigáció változásának argumentumai.</param>
        private void NavigationView_SelectionChanged(NavigationView sender, NavigationViewSelectionChangedEventArgs args)
        {
            var selectedItem = (NavigationViewItem)args.SelectedItem;
            if (selectedItem.Name == NavItem_Movies.Name)
            {
                ContentFrame.Navigate(typeof(MoviesPage));
                if (ContentFrame.Content is MoviesPage moviesPage)
                {
                    moviesPage.NavigationCacheMode = NavigationCacheMode.Enabled;
                    moviesPage.MoviesViewModel.NavigationService = MainViewModel.NavigationService;
                }
            }
            if (selectedItem.Name == NavItem_Series.Name)
            {
                ContentFrame.Navigate(typeof(SeriesPage));
                if (ContentFrame.Content is SeriesPage seriesPage)
                {
                    seriesPage.NavigationCacheMode = NavigationCacheMode.Enabled;
                    seriesPage.SeriesViewModel.NavigationService = MainViewModel.NavigationService;
                }
            }
            if (selectedItem.Name == NavItem_Trending.Name)
            {
                ContentFrame.Navigate(typeof(TrendingPage), PageHeader);
                if (ContentFrame.Content is TrendingPage trendingPage)
                {
                    trendingPage.NavigationCacheMode = NavigationCacheMode.Enabled;
                    trendingPage.TrendingViewModel.NavigationService = MainViewModel.NavigationService;
                }
            }
        }
    }
}
