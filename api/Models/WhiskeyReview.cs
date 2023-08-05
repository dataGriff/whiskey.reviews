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
        public string Id { get { return UserId.ToLower() + "-" + WhiskeyID; } }

        [JsonProperty("userId")]
        public string UserId { get; set; }

        [JsonProperty("date")]
        private DateTime Date { get; set; }

        [JsonProperty("whiskeyId")]
        public string WhiskeyID
        {
            get
            {
                return new string(WhiskeyName.Where(c => Char.IsLetterOrDigit(c)).ToArray()).ToLower();
            }
        }

        [Required]
        [JsonProperty("whiskeyName")]
        public string WhiskeyName { get; set; }

        [Required]
        [JsonProperty("distilleryName")]
        public string? DistilleryName { get; set; }

        [Required]
        [Range(1, 5)]
        [JsonProperty("rating")]
        public int? Rating { get; set; }

        [JsonProperty("notes")]
        public Note[]? Notes { get; set; }

        [StringLength(100, MinimumLength = 1, ErrorMessage = "Must be at least 1 characters long and less than 100 characters.")]
        [JsonProperty("review")]
        public string? Review { get; set; }

        [JsonProperty("location")]
        public string? Location { get; set; }
    }
}
