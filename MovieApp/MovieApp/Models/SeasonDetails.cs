using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieApp.Models
{
    public class SeasonDetails
    {
        public string _id { get; set; }
        public string air_date { get; set; }
        public ObservableCollection<Episode> episodes { get; set; }
        public string name { get; set; }
        public string overview { get; set; }
        public int id { get; set; }
        public string poster_path { get; set; }
        public int season_number { get; set; }
    }

    public class Episode : INotifyPropertyChanged
    {
        public string air_date { get; set; }
        public int episode_number { get; set; }
        public int id { get; set; }
        public string name { get; set; }
        public string overview { get; set; }
        public string production_code { get; set; }
        public int? runtime { get; set; }
        public int? season_number { get; set; }
        public int show_id { get; set; }

        private string _still_path;
        public string still_path {
            get { return _still_path; }
            set
            {
                _still_path = value;
                OnPropertyChanged(nameof(_still_path));
            }
        }
        public float vote_average { get; set; }
        public int vote_count { get; set; }
        public SeriesCrew[] crew { get; set; }
        public Guest_Stars[] guest_stars { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public class SeriesCrew
    {
        public string job { get; set; }
        public string department { get; set; }
        public string credit_id { get; set; }
        public bool adult { get; set; }
        public int gender { get; set; }
        public int id { get; set; }
        public string known_for_department { get; set; }
        public string name { get; set; }
        public string original_name { get; set; }
        public float popularity { get; set; }
        public string profile_path { get; set; }
    }

    public class Guest_Stars
    {
        public string character { get; set; }
        public string credit_id { get; set; }
        public int order { get; set; }
        public bool adult { get; set; }
        public int gender { get; set; }
        public int id { get; set; }
        public string known_for_department { get; set; }
        public string name { get; set; }
        public string original_name { get; set; }
        public float popularity { get; set; }
        public string profile_path { get; set; }
    }


}
