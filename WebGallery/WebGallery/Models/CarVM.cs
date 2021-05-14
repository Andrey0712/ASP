using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebGallery.Models
{
    public class CarVM
    {
        [JsonProperty("Id")]
        public long Id { get; set; }
        [JsonProperty("Mark")]
        public string Mark { get; set; }
        [JsonProperty("Model")]
        public string Model { get; set; }
        [JsonProperty("Year")]
        public int Year { get; set; }
        [JsonProperty("Fuel")]
        public string Fuel { get; set; }
        [JsonProperty("Сapacity")]
        public float Сapacity { get; set; }
        [JsonProperty("Image")]
        public string Image { get; set; }
    }
}
