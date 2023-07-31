using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

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

        [Required]
        [JsonProperty("userId")]
        public string UserID { get; set; }

        [Required]
        [JsonProperty("whiskey")]
        public string Whiskey { get; set; }

        [Required]
        [Range(1,5)]
        [JsonProperty("rating")]
        public int Rating { get; set; }

        [JsonProperty("notes")]
        public Note[]? Notes { get; set; }

        [StringLength(100, MinimumLength = 1, ErrorMessage = "Must be at least 1 characters long and less than 100 characters.")] 
        [JsonProperty("review")]
        public string Review { get; set; }
    }
}
