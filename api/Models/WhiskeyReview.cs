using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace api.Models
{

    public enum Note
    {
        Vanilla,
        Honey,
        Caramel,
        Nutmeg
    }

    public class WhiskeyReview
    {
        public WhiskeyReview()
        {
             Date = DateTime.Now;
        }

        [JsonProperty("id")]
        public string Id { get {return Whiskey;}}

        [JsonProperty("date")]
        private DateTime Date { get; }

        [JsonProperty("userId")]
        public string UserID { get; set; }

        [JsonProperty("whiskey")]
        public string Whiskey { get; set; }

        [JsonProperty("rating")]
        public int Rating { get; set; }

        [System.Text.Json.Serialization.JsonConverter(typeof(JsonStringEnumConverter))]
        [JsonProperty("notes")]
        public Note[] Notes { get; set; }

        [JsonProperty("review")]
        public string Review { get; set; }
    }
}
