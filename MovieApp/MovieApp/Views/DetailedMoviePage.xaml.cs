using MovieApp.Models;
using MovieApp.ViewModels;
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

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace MovieApp.Views
{
    public sealed partial class DetailedMoviePage : Windows.UI.Xaml.Controls.Page
    {

        public DetailedMoviePage()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Egy személyre való kattintás eseménykezelője.
        /// </summary>
        /// <param name="sender">A küldő objektum.</param>
        /// <param name="e">Az kattintás argumentumai.</param>
        private void Person_ItemClick(object sender, ItemClickEventArgs e)
        {
            var cast = (Cast)e.ClickedItem;
            DetailedMovieViewModel.NavigateToPerson(cast);
        }
    }
}
