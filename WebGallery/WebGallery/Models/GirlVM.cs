using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebGallery.Models
{
    public class GirlVM
    {
        [JsonProperty("Name")]
        public string Name { get; set; }
        [JsonProperty("Age")]
        public int Age { get; set; }
        [JsonProperty("Weight")]
        public int Weight { get; set; }
        [JsonProperty("Height")]
        public int Height { get; set; }
        [JsonProperty("Image")]
        public string Image { get; set; }
    }
}
