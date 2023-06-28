using MovieApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace MovieApp
{
    public class MediaTemplateSelector : DataTemplateSelector
    {
        public DataTemplate MovieTemplate { get; set; }
        public DataTemplate SeriesTemplate { get; set; }

        /// <summary>
        /// A megfelelő template kiválasztása a header alapján.
        /// </summary>
        /// <param name="item">A header.</param>
        /// <param name="container">A konténer.</param>
        /// <returns>A kiválasztott template.</returns>
        protected override DataTemplate SelectTemplateCore(object item, DependencyObject container)
        {
            if (item is MovieHeader)
            {
                return MovieTemplate;
            }
            else if (item is SeriesHeader)
            {
                return SeriesTemplate;
            }

            return base.SelectTemplateCore(item);
        }
    }

}
