using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace api.Models
{
    public class Whiskey
    {

        [JsonProperty("id")]
        public string Id { get {return new string(Name.Where(c => Char.IsLetterOrDigit(c)).ToArray()).ToLower();}}

        [Required]
        [JsonProperty("name")]
        public string Name { get; set; }

        [Required]
        [JsonProperty("distillery")]
        public string Distillery { get; set; }

        
    }
}
