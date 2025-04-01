namespace reviuAPI.Models
{
    public class resultatsLlancaments
    {
        public dates dates { get; set; }
        public List<UltimsLlencaments> results { get; set; }

    }

    public class dates
    {
        public string maximum { get; set; }
        public string minimum { get; set; }
    }
}
