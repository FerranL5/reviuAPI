using Newtonsoft.Json;

namespace reviuAPI.Models
{
    public class consumidorTMDB
    {

        static HttpClient httpClient;

        static readonly string ErrorMessage = "Error en l'API.";
        static readonly string contentType = "application/json";
        static readonly string key = "bef269f41a86da36e5050bf1db568aab";

        public static void CreateHttpClient()  // Cal executar aquest mètode en el constructor del Controller
        {
            httpClient = new HttpClient
            {
                BaseAddress = new Uri("https://api.themoviedb.org/3/")
            };
            httpClient.DefaultRequestHeaders.Add("Accept", contentType);
            httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer eyJhbGciOiJIUzI1NiJ9.eyJhdWQiOiJiZWYyNjlmNDFhODZkYTM2ZTUwNTBiZjFkYjU2OGFhYiIsIm5iZiI6MTczOTg5MDc3NS43NjEsInN1YiI6IjY3YjRhMDU3ZTBkOWY4MzNiYzZkYzcyMiIsInNjb3BlcyI6WyJhcGlfcmVhZCJdLCJ2ZXJzaW9uIjoxfQ.qv25wnNf8ZqrioAVqOW5Mjk1sZYC1Im7cYKjmT4XPZA");
        }

        public static async Task<object> MakeRequest(string url, Type responseType)
        {
            HttpResponseMessage response;

            response = httpClient.GetAsync(url).Result;

            if (response.IsSuccessStatusCode)
            {
                string json = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject(json, responseType);
                return result;
            }
            else
            {
                throw new Exception(ErrorMessage);
            }
        }

        public buscarContingutPerNom buscarContingutPerNom(string title)
        {

            buscarContingutPerNom bcpn = new buscarContingutPerNom();
            try
            {
                string url = "search/multi?query=" + title + "&include_adult=false&language=en-US&page=1";
                bcpn = (buscarContingutPerNom)MakeRequest(url, typeof(buscarContingutPerNom)).Result;
                              

                return bcpn;
            }
            catch
            {
                return bcpn;
            }

        }

        public ContingutDTO GetContingutDTO(int id, string type)
        {

            ContingutDTO cDTO = new ContingutDTO();
            try
            {
                
                string url = type + "/" + id + "?language=en-US";
                if(type == "movie")
                {
                    MovieTMDB m = new MovieTMDB();
                    m = (MovieTMDB)MakeRequest(url, typeof(MovieTMDB)).Result;
                    cDTO = new ContingutDTO(m);
                } else if (type == "tv")
                {
                    TvTMDB tv = new TvTMDB();
                    tv = (TvTMDB)MakeRequest(url, typeof(TvTMDB)).Result;
                    cDTO = new ContingutDTO(tv);
                }
                    
               
                return cDTO;
            }
            catch
            {
                return cDTO;
            }

        }

        public season GetSeasonDeatails(int id, int season)
        {

            season s = new season();
            try
            {

                string url = "tv/" + id + "/season/" + season + "?language=en-US";
                s = (season)MakeRequest(url, typeof(season)).Result;

                return s;
            }
            catch
            {
                return s;
            }

        }

        public resultatsRecomanacions GetRecomanacions(int id, string type)
        {

            resultatsRecomanacions result = new resultatsRecomanacions();

            try
            {

                string url = type+"/"+ id +"/recommendations?language=en-US&page=1";
               
                result = (resultatsRecomanacions)MakeRequest(url, typeof(resultatsRecomanacions)).Result;
               
                return result;
            }
            catch
            {
                return result;
            }

        }

        public resultatsLlancaments GetUltimsLlancaments()
        {
            resultatsLlancaments llancaments = new resultatsLlancaments();

            DateTime dateTimeMin = DateTime.Now.AddMonths(-1);
            DateTime dateTimeMax = DateTime.Now;

            try
            {
                string url = "discover/movie?include_adult=false&include_video=false&language=en-US&page=1&sort_by=popularity.desc&with_release_type=2|3&release_date.gte="+ dateTimeMin.ToString().Substring(0, 10)+"&release_date.lte=" + dateTimeMax.ToString().Substring(0, 10);
                llancaments = (resultatsLlancaments)MakeRequest(url, typeof (resultatsLlancaments)).Result;

                llancaments.results = llancaments.results.OrderByDescending(x => x.release_date).ToList();

                return llancaments;
            }
            catch
            {
                return llancaments;
            }
        }
        public resultatsLlancaments GetProximsLlancaments()
        {
            resultatsLlancaments llancaments = new resultatsLlancaments();

            DateTime dateTimeMin = DateTime.Now.AddDays(1);
            DateTime dateTimeMax = DateTime.Now.AddMonths(1);

            try
            {
                string url = "discover/movie?include_adult=false&include_video=false&language=en-US&page=1&primary_release_date.gte=" + dateTimeMin.ToString().Substring(0, 10) + "&primary_release_date.lte=" + dateTimeMax.ToString().Substring(0, 10) + "&sort_by=popularity.desc";
                llancaments = (resultatsLlancaments)MakeRequest(url, typeof(resultatsLlancaments)).Result;

                return llancaments;
            }
            catch
            {
                return llancaments;
            }
        }

        public resultatsRecomanacions GetPopular()
        {
            resultatsRecomanacions popular = new resultatsRecomanacions();

            try
            {
                string url = "trending/all/day?language=en";
                popular = (resultatsRecomanacions)MakeRequest(url, typeof(resultatsRecomanacions)).Result;

                return popular;
            }
            catch
            {
                return popular;
            }
        }

        public proveidors GetProveidors(int id, string type)
        {
            proveidors proveidors = new proveidors();

            try
            {

                string url = type + "/" + id + "/watch/providers";

                proveidors = (proveidors)MakeRequest(url, typeof(proveidors)).Result;

                return proveidors;
            }
            catch
            {
                return proveidors;
            }

        }

    }
}
