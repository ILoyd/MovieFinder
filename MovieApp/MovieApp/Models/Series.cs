using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieApp.Models
{

    public class Series : Show
    {
        public Created_By[] created_by { get; set; }
        public int[] episode_run_time { get; set; }
        public string first_air_date { get; set; }
        public bool in_production { get; set; }
        public string[] languages { get; set; }
        public string last_air_date { get; set; }
        public Last_Episode_To_Air last_episode_to_air { get; set; }
        public string name { get; set; }

        public object next_episode_to_air { get; set; }
        public Network[] networks { get; set; }
        public int? number_of_episodes { get; set; }
        public int? number_of_seasons { get; set; }
        public string[] origin_country { get; set; }
        public string original_name { get; set; }
        public Season[] seasons { get; set; }
        public string type { get; set; }
    }

    public class Last_Episode_To_Air
    {
        public int id { get; set; }
        public string name { get; set; }
        public string overview { get; set; }
        public float vote_average { get; set; }
        public int vote_count { get; set; }
        public string air_date { get; set; }
        public int episode_number { get; set; }
        public string production_code { get; set; }
        public int? runtime { get; set; }
        public int season_number { get; set; }
        public int show_id { get; set; }
        public string still_path { get; set; }
    }

    public class Created_By
    {
        public int id { get; set; }
        public string credit_id { get; set; }
        public string name { get; set; }
        public int gender { get; set; }
        public string profile_path { get; set; }
    }

    public class Network
    {
        public int id { get; set; }
        public string logo_path { get; set; }
        public string name { get; set; }
        public string origin_country { get; set; }
    }

    public class Season
    {
        public string air_date { get; set; }
        public int episode_count { get; set; }
        public int id { get; set; }
        public string name { get; set; }
        public string overview { get; set; }
        public string poster_path { get; set; }
        public int season_number { get; set; }
    }

}
