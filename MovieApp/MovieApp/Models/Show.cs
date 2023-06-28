using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieApp.Models
{
    public class Show : INotifyPropertyChanged
    {
        public bool adult { get; set; }
        public string _backdrop_path;
        public string backdrop_path 
        {
            get { return _backdrop_path; } 
            set { _backdrop_path = value;
                OnPropertyChanged(nameof(backdrop_path));
            } 
        }
        public Genre[] genres { get; set; }
        public string homepage { get; set; }
        public int id { get; set; }
        public string original_language { get; set; }
        public string overview { get; set; }
        public float popularity { get; set; }
        public string _poster_path;
        public string poster_path
        {
            get { return _poster_path; }
            set
            {
                _poster_path = value;
                OnPropertyChanged(nameof(poster_path));
            }
        }
        public Production_Companies[] production_companies { get; set; }
        public Production_Countries[] production_countries { get; set; }
        public Spoken_Languages[] spoken_languages { get; set; }
        public string status { get; set; }
        public string tagline { get; set; }
        public float vote_average { get; set; }
        public int vote_count { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public class Genre
    {
        public int id { get; set; }
        public string name { get; set; }
    }

    public class Production_Companies
    {
        public int id { get; set; }
        public string logo_path { get; set; }
        public string name { get; set; }
        public string origin_country { get; set; }
    }

    public class Production_Countries
    {
        public string iso_3166_1 { get; set; }
        public string name { get; set; }
    }

    public class Spoken_Languages
    {
        public string english_name { get; set; }
        public string iso_639_1 { get; set; }
        public string name { get; set; }
    }

}
