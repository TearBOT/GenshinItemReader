using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ItemReader.Utility
{
    internal class AllCoordinates
    {
        public Rect BagIcon { get; set; }
        public List<List<Rect>> Items { get; set; }
        public List<List<Rect>> LastLine { get; set; }
        public Rect mora { get; set; }
    }

    internal class ReadCoordinates
    {
        public BagIcon bag_icon { get; set; }
        public List<List<Item>> items { get; set; }
        public List<List<Item>> last_line { get; set; }
        public Mora mora { get; set; }
    }

    public class BagIcon
    {
        public Start start { get; set; }
        public End end { get; set; }
        public Dimensions dimensions { get; set; }
    }
    public class Item
    {
        public Start start { get; set; }
        public End end { get; set; }
        public Dimensions dimensions { get; set; }
    }
    public class Mora
    {
        public Start start { get; set; }
        public End end { get; set; }
        public Dimensions dimensions { get; set; }
    }

    public class Start
    {
        public int x { get; set; }
        public int y { get; set; }
    }

    public class End
    {
        public int x { get; set; }
        public int y { get; set; }
    }

    public class Dimensions
    {
        public int width { get; set; }
        public int height { get; set; }
    }

    internal class AllCoordinatesFactory {

        public static AllCoordinates getAllCoordinates()
        {
            // C:\Users\Mini-Soo\Projects\GenshinItemReader\ItemReader\bin\Debug\net7.0-windows\Resources\Coordinates.json
            string resourcePath = @"..\..\..\Resources\Coordinates.json";
            string jsonString = File.ReadAllText(resourcePath);
            ReadCoordinates everything = JsonSerializer.Deserialize<ReadCoordinates>(jsonString);

            AllCoordinates easierToRead = new AllCoordinates();
            easierToRead.BagIcon = new Rect(everything.bag_icon.start, everything.bag_icon.end, everything.bag_icon.dimensions);
            easierToRead.mora = new Rect(everything.mora.start, everything.mora.end, everything.mora.dimensions);
            easierToRead.Items = new List<List<Rect>>();
            easierToRead.LastLine = new List<List<Rect>>();

            int i = 0;
            foreach (var x in everything.items) {
                easierToRead.Items.Add(new List<Rect>());
                foreach (var y in x) {
                    easierToRead.Items[i].Add(new Rect(y.start, y.end, y.dimensions));
                }
                i++;
            }

            i = 0;
            foreach (var x in everything.last_line) {
                easierToRead.LastLine.Add(new List<Rect>());
                foreach (var y in x) {
                    easierToRead.LastLine[i].Add(new Rect(y.start, y.end, y.dimensions));
                }
                i++;
            }

            return easierToRead;
        }

    }
}
