using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;

namespace api.Models
{
     /// <summary>
    /// Represents a distillery.
    /// </summary>
    public class Distillery
    {
        /// <summary>
        /// Gets or sets the distillery ID.
        /// </summary>
        [JsonPropertyName("id")]
        public string Id { get; set; }

        /// <summary>
        /// This is the name of the distillery
        /// </summary>
        [JsonPropertyName("name")]
        public string Name { get; set; } 

        /// <summary>
        /// This is a link to the wikipedia of the distillery
        /// </summary>
        [JsonPropertyName("wikiLink")]
        public string? WikiLink { get; set; }

        /// <summary>
        /// This is the country that the distillery is found in 
        /// </summary>
        [JsonPropertyName("country")]
        public string? Country { get; set; }

        /// <summary>
        /// This is the type of whiskey at the distillery
        /// </summary>
        [JsonPropertyName("type")]
        public string? Type { get; set; }
    }
}