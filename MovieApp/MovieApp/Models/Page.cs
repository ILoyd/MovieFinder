using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieApp.Models
{

    public class Page
    {
        public int page { get; set; }
        public List<ShowHeader> results { get; set; }
        public int total_pages { get; set; }
        public int total_results { get; set; }
    }

    public class ShowHeader : INotifyPropertyChanged
    {
        public bool adult { get; set; }
        public string _backdrop_path;
        public string backdrop_path
        {
            get { return _backdrop_path; }
            set
            {
                _backdrop_path = value;
                OnPropertyChanged(nameof(backdrop_path));
            }
        }
        public int[] genre_ids { get; set; }
        public int id { get; set; }
        public string original_language { get; set; }
        public string overview { get; set; }
        public float popularity { get; set; }
        private string _poster_path;
        public string poster_path 
        {   get { return _poster_path; }
            set { _poster_path = value;
                 OnPropertyChanged(nameof(poster_path));   
            } 
        }
        public bool video { get; set; }
        public float vote_average { get; set; }
        public int vote_count { get; set; }
        public string status { get; set; }
        public string homepage { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }


    public class MovieHeader : ShowHeader
    {
        public string title { get; set; }
        public string original_title { get; set; }
        public string media_type { get; set; }
        public string release_date { get; set; }
    }


    public class SeriesHeader : ShowHeader
    {
        public string name { get; set; } = null;
        public string original_name { get; set; }
        public string media_type { get; set; }
        public string first_air_date { get; set; }
        public string[] origin_country { get; set; }
    }

}
