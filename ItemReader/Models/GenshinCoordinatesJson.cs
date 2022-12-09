namespace ItemReader.Models
{
    internal class GenshinCoordinatesJson
    {
        public BagIconPos BagIconPos { get; set; }
        public List<ItemsPos> FirstLineItemsPos { get; set; }
        public List<ItemsPos> LastLineItemsPos { get; set; }
        public MoraPos MoraPos { get; set; }
    }

    public class BagIconPos
    {
        public TopLeftPointPos TopLeftPos { get; set; }
        public BottomRightPointPos BotRightPos { get; set; }
        public RectDimensions Dimensions { get; set; }
    }
    public class ItemsPos
    {
        public TopLeftPointPos TopLeftPos { get; set; }
        public BottomRightPointPos BotRightPos { get; set; }
        public RectDimensions Dimensions { get; set; }
    }
    public class MoraPos
    {
        public TopLeftPointPos TopLeftPos { get; set; }
        public BottomRightPointPos BotRightPos { get; set; }
        public RectDimensions Dimensions { get; set; }
    }

    public class TopLeftPointPos
    {
        public int X { get; set; }
        public int Y { get; set; }
    }

    public class BottomRightPointPos
    {
        public int X { get; set; }
        public int Y { get; set; }
    }

    public class RectDimensions
    {
        public int width { get; set; }
        public int height { get; set; }
    }
}
