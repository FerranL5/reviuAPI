namespace reviuAPI.Models
{
    public class season
    {

        public string air_date { get; set; }
        public int episode_count { get; set; }
        public int id { get; set; }
        public string name { get; set; }
        public string overview { get; set; }
        public string poster_path { get; set; }
        public int season_number { get; set; }
        public float vote_average { get; set; }
        public List<episode>? episodes { get; set; }

    }
}
