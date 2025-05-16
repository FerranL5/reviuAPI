﻿namespace reviuAPI.Models
{
    public class next_episode_to_air
    {
        public int id { get; set; }
        public string name { get; set; }
        public string overview { get; set; }
        public float vote_average { get; set; }
        public int vote_count { get; set; }
        public string air_date { get; set; }
        public int episode_number { get; set; }
        public string episode_type { get; set; }
        public string production_code { get; set; }
        public int runtime { get; set; }
        public int season_number { get; set; }
        public int show_id { get; set; }
        public string still_path { get; set; }
    }
}