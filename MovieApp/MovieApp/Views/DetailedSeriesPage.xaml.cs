using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace MovieApp.Views
{
    public sealed partial class DetailedSeriesPage : Page
    {
        public DetailedSeriesPage()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Egy másik évad kiválasztása a selectorban.
        /// </summary>
        /// <param name="sender">A küldő objektum.</param>
        /// <param name="e">Az kiválasztás változásának argumentumai.</param>
        private void Season_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DetailedSeriesViewModel.GetEpisodes(SeasonSelector.SelectedIndex+1);
        }
    }
}
