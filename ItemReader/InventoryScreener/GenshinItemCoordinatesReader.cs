using ItemReader.Models;
using ItemReader.Utils;
using Newtonsoft.Json;

namespace ItemReader.InventoryScreener
{
    internal static class GenshinItemCoordinatesReader {

        /* PUBLIC METHOD(S) */

        public static GenshinItemCoordinates EvaluateAllItemsPositions()
        {
            string jsonString = File.ReadAllText(@"..\..\..\Resources\Coordinates.json");
            GenshinCoordinatesJson JsonData = JsonConvert.DeserializeObject<GenshinCoordinatesJson>(jsonString);

            if (JsonData is null ) {
                return null;
            }

            GenshinItemCoordinates ItemsPosData = new GenshinItemCoordinates(
                new Rect(
                    JsonData.BagIconPos.TopLeftPos,
                    JsonData.BagIconPos.BotRightPos,
                    JsonData.BagIconPos.Dimensions
                    ),
                ProcessItemsPosFromJson(JsonData.FirstLineItemsPos),
                ProcessItemsPosFromJson(JsonData.LastLineItemsPos),
                new Rect(
                    JsonData.MoraPos.TopLeftPos,
                    JsonData.MoraPos.BotRightPos,
                    JsonData.MoraPos.Dimensions)
                );

            return ItemsPosData;
        }

        /* PRIVATE METHOD(S) */

        private static List<Rect> ProcessItemsPosFromJson(IEnumerable<ItemsPos> ItemLine)
        {
            var ItemsPosList = new List<Rect>();

            foreach (var Item in ItemLine) {
                ItemsPosList.Add(new Rect(Item.TopLeftPos, Item.BotRightPos, Item.Dimensions));
            }

            return ItemsPosList;
        }

    }
}
