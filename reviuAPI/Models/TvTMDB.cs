namespace reviuAPI.Models
{
    public class TvTMDB
    {
        public bool adult { get; set; }
        public string backdrop_path { get; set; }
        public List<int> episode_run_time { get; set; }
        public string first_air_date {  get; set; }
        public List<genres> genres { get; set; }
        public string homepage {  get; set; }
        public int id {  get; set; }
        public bool in_production { get; set; }
        public List<string> languages { get; set; }
        public string last_air_date { get; set; }
        public string name { get; set; }
        public next_episode_to_air? next_episode_to_air {  get; set; }
        public int number_of_episodes { get; set; }
        public int number_of_seasons { get; set; }
        public List<string> origin_country { get; set; }
        public string original_language {  get; set; }
        public string original_name { get; set; }
        public string overview {  get; set; }
        public float popularity {  get; set; }
        public string poster_path {  get; set; }
        public List<season> seasons { get; set; }
        public string status {  get; set; }
        public string tagline { get; set; }
        public string type { get; set; }
        public float vote_average {  get; set; }
        public int vote_count {  get; set; }

    }
}
