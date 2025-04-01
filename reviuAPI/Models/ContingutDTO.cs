namespace reviuAPI.Models
{
    public class ContingutDTO
    {

        public bool adult { get; set; }
        public string backdrop_path { get; set; }
        public int? budget { get; set; }
        public List<genres> genres { get; set; }
        public string homepage { get; set; }
        public int id { get; set; }
        public string? imdb_id { get; set; }
        public List<string> origin_country { get; set; }
        public string original_language { get; set; }
        public string? original_title { get; set; }
        public string overview { get; set; }
        public float popularity { get; set; }
        public string poster_path { get; set; }
        public string status { get; set; }
        public string tagline { get; set; }
        public string? title { get; set; }
        public bool? video { get; set; }
        public float vote_average { get; set; }
        public int vote_count { get; set; }
        public List<int>? episode_run_time { get; set; }
        public string? first_air_date { get; set; }
        public bool? in_production { get; set; }
        public List<string>? languages { get; set; }
        public string? last_air_date { get; set; }
        public string? name { get; set; }
        public string? next_episode_to_air { get; set; }
        public int? number_of_episodes { get; set; }
        public int? number_of_seasons { get; set; }
        public string? original_name { get; set; }
        public List<season>? seasons { get; set; }
        public string? type { get; set; }

        public ContingutDTO( TvTMDB tv)
        {
            this.adult = tv.adult;
            this.backdrop_path = tv.backdrop_path;
            this.budget = null;
            this.genres = tv.genres;
            this.homepage = tv.homepage;
            this.id = tv.id;
            this.imdb_id = null;
            this.origin_country = tv.origin_country;
            this.original_language = tv.original_language;
            this.original_title = null;
            this.overview = tv.overview;
            this.popularity = tv.popularity;
            this.poster_path = tv.poster_path;
            this.status = tv.status;
            this.tagline = tv.tagline;
            this.title = null;
            this.video = null;
            this.vote_average = tv.vote_average;
            this.vote_count = tv.vote_count;
            this.episode_run_time = tv.episode_run_time;
            this.first_air_date = tv.first_air_date;
            this.in_production = tv.in_production;
            this.languages = tv.languages;
            this.last_air_date = tv.last_air_date;
            this.name = tv.name;
            this.next_episode_to_air = tv.next_episode_to_air;
            this.number_of_episodes = tv.number_of_episodes;
            this.number_of_seasons = tv.number_of_seasons;
            this.original_name = tv.original_name;
            this.seasons = tv.seasons;
            this.type = tv.type;
        }

        public ContingutDTO( MovieTMDB movie)
        {
            this.adult = movie.adult;
            this.backdrop_path = movie.backdrop_path;
            this.budget = movie.budget;
            this.genres = movie.genres;
            this.homepage = movie.homepage;
            this.id = movie.id;
            this.imdb_id = movie.imdb_id;
            this.origin_country = movie.origin_country;
            this.original_language = movie.original_language;
            this.original_title = movie.original_title;
            this.overview = movie.overview;
            this.popularity = movie.popularity;
            this.poster_path = movie.poster_path;
            this.status = movie.status;
            this.tagline = movie.tagline;
            this.title = movie.title;
            this.video = movie.video;
            this.vote_average = movie.vote_average;
            this.vote_count = movie.vote_count;
            this.episode_run_time = null;
            this.first_air_date = null;
            this.in_production = null;
            this.languages = null;
            this.last_air_date = null;
            this.name = null;
            this.next_episode_to_air = null;
            this.number_of_episodes = null;
            this.number_of_seasons = null;
            this.original_name = null;
            this.seasons = null;
            this.type = null;
        }

        public ContingutDTO() { }
    }
}
