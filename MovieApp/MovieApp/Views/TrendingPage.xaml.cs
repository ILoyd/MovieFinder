using MovieApp.Models;
using MovieApp.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Template10.Services.NavigationService;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace MovieApp.Views
{
    public sealed partial class TrendingPage : Windows.UI.Xaml.Controls.Page
    {

        private TrendingPageViewModel _trendingViewModel;
        public TrendingPageViewModel TrendingViewModel 
        { get { return _trendingViewModel; } 
     
        }

        public TrendingPage()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            var param = e.Parameter as PageHeader;
            _trendingViewModel = new TrendingPageViewModel(param);
            DataContext = TrendingViewModel;
            base.OnNavigatedTo(e);
        }


        /// <summary>
        /// A film gomb kiválasztásának kezelése.
        /// </summary>
        /// <param name="sender">A küldő objektum.</param>
        /// <param name="e">Az kattintás argumentumai.</param>
        private void MovieButton_Checked(object sender, RoutedEventArgs e)
        {
            if (TrendingViewModel != null)
            {
                TrendingViewModel.ChangeMediaType(new PageHeader() { DayOrWeek = GetDayOrWeek(), MediaType = "movie" });
            }
        }

        /// <summary>
        /// A sorozat gomb kiválasztásának kezelése.
        /// </summary>
        /// <param name="sender">A küldő objektum.</param>
        /// <param name="e">Az kattintás argumentumai.</param>
        private void SeriesButton_Checked(object sender, RoutedEventArgs e)
        {
            TrendingViewModel.ChangeMediaType(new PageHeader() { DayOrWeek = GetDayOrWeek(), MediaType = "tv" });
        }

        /// <summary>
        /// A nap gomb kiválasztásának kezelése.
        /// </summary>
        /// <param name="sender">A küldő objektum.</param>
        /// <param name="e">Az kattintás argumentumai.</param>
        private void DayButton_Checked(object sender, RoutedEventArgs e)
        {
            if (TrendingViewModel != null)
            {
                TrendingViewModel.ChangeMediaType(new PageHeader() { DayOrWeek = "day", MediaType = GetMediaType() });
            }

        }

        /// <summary>
        /// A hét gomb kiválasztásának kezelése.
        /// </summary>
        /// <param name="sender">A küldő objektum.</param>
        /// <param name="e">Az kattintás argumentumai.</param>
        private void WeekButton_Checked(object sender, RoutedEventArgs e)
        {

            TrendingViewModel.ChangeMediaType(new PageHeader() { DayOrWeek = "week", MediaType = GetMediaType() });
        }

        /// <summary>
        /// A nap/hét elkérése.
        /// </summary>
        private string GetDayOrWeek()
        {
            return (bool)DayButton.IsChecked ? "day" : "week";
        }

        /// <summary>
        /// A média típus elkérése.
        /// </summary>
        private string GetMediaType()
        {
            return (bool)MovieButton.IsChecked ? "movie" : "tv";
        }

        /// <summary>
        /// Egy filmre/sorozatra való kattintás eseménykezelője.
        /// </summary>
        /// <param name="sender">A küldő objektum.</param>
        /// <param name="e">Az kattintás argumentumai.</param>
        private void TrendingDetails_ItemClick(object sender, ItemClickEventArgs e)
        {
           
            if((bool)SeriesButton.IsChecked)
            {
                var seriesHeader = (SeriesHeader)e.ClickedItem;
                TrendingViewModel.NavigateToSeriesDetails(seriesHeader.id);
            }
            else if((bool)MovieButton.IsChecked)
            {
                var movieHeader = (MovieHeader)e.ClickedItem;
                TrendingViewModel.NavigateToMovieDetails(movieHeader.id);
            }
        }
    }
}
