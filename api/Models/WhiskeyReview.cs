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
        private string Id { get {return WhiskeyName;}}

        [JsonProperty("date")]
        private DateTime Date { get; }

        [Required]
        [JsonProperty("whiskeyName")]
        public string WhiskeyName { get; set; }

        [Required]
        [Range(1,5)]
        [JsonProperty("rating")]
        public int Rating { get; set; }

        [JsonProperty("notes")]
        public Note[]? Notes { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "Must be at least 1 characters long and less than 100 characters.")] 
        [JsonProperty("review")]
        public string Review { get; set; }
    }
}
