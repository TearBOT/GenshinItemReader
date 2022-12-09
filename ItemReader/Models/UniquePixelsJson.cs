using Newtonsoft.Json;

namespace ItemReader.Models
{

    public class UniquePixelsJson {
        [JsonProperty("name")]
        public string ItemName { get; set; }

        [JsonProperty("uniques")]
        public List<Pixel> Pixels { get; set; }

    }

    public class Pixel {
        [JsonProperty("coord")]
        public Coord Pos { get; set; }

        [JsonProperty("value R")]
        public int ValueR { get; set; }

        [JsonProperty("value G")]
        public int ValueG { get; set; }

        [JsonProperty("value B")]
        public int ValueB { get; set; }

        [JsonProperty("value A")]
        public int ValueA { get; set; }

    }

    public class Coord {
        public int X { get; set; }
        public int Y { get; set; }

    }
}
