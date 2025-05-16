using System.Text.Json.Serialization;

namespace reviuAPI.Models
{
    public class crew
    {

        [JsonIgnore]
        public string department {  get; set; }
        public string job {  get; set; }
        public string credit_id { get; set; }
        public bool adult { get; set; }
        public int gender { get; set; }
        public int id { get; set; }
        public string known_for_department { get; set; }
        public string name { get; set; }
        public string original_name {  get; set; }
        public float popularity { get; set; }
        public string profile_path { get; set; }

    }
}
