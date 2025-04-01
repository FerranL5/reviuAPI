﻿namespace reviuAPI.Models
{
    public class episode
    {


        
        public string air_date { get; set; }
        public List<crew> crew { get; set; }
        public int episode_number { get; set; }
        public List<guest_stars> guest_stars { get; set; }
        public string name { get; set; }
        public string overview { get; set; }
        public string id { get; set; }
        public string production_code { get; set; }
        public int runtime {  get; set; }
        public int season_number { get; set; }
        public string still_path { get; set; }
        public float vote_average { get; set; }
        public int vote_count { get; set; }



    }
}
