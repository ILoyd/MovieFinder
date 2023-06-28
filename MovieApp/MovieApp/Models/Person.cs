using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieApp.Models
{

    public class Person : Cast
    {
        public string[] also_known_as { get; set; }

        private string _biography;
        public string biography {
            get { return _biography; } set
            {
                _biography= value;
                OnPropertyChanged(nameof(biography));
            }
        }
        public string birthday { get; set; }
        private object _deathhday;
        public object deathday {
            get { return _deathhday; }
            set
            {
                _deathhday = value;
                OnPropertyChanged(nameof(deathday));
            }
        }
        public object homepage { get; set; }
        public string imdb_id { get; set; }
        private string _place_of_birth;
        public string place_of_birth {
            get { return _place_of_birth; }
            set
            {
                _place_of_birth = value;
                OnPropertyChanged(nameof(place_of_birth));
            }
        }

    }

}
