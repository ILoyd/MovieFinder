using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieApp.Models
{

    public class Movie : Show
    {
        public Belongs_To_Collection belongs_to_collection { get; set; }
        public int budget { get; set; }
        public string imdb_id { get; set; }
        public string original_title { get; set; }
        public string release_date { get; set; }
        public int revenue { get; set; }
        public int runtime { get; set; }
        public string title { get; set; }
        public bool video { get; set; }
    }

    public class Belongs_To_Collection
    {
        public int id { get; set; }
        public string name { get; set; }
        public string poster_path { get; set; }
        public string backdrop_path { get; set; }
    }

}
