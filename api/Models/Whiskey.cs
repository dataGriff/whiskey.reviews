using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace api.Models
{
    public class Whiskey
    {

        [JsonProperty("id")]
        private string Id { get {return WhiskeyName;}}

        [JsonProperty("date")]
        private DateTime Date { get; }

        [Required]
        [JsonProperty("whiskeyName")]
        public string WhiskeyName { get; set; }

        
    }
}
