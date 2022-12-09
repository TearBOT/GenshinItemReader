namespace ItemReader.Models
{
    internal class GenshinCoordinatesJson {
        public BagIconPos BagIconPos { get; set; }
        public List<ItemsPos> FirstLineItemsPos { get; set; }
        public List<ItemsPos> LastLineItemsPos { get; set; }
        public MoraPos MoraPos { get; set; }

    }

    internal class BagIconPos {
        public TopLeftPointPos TopLeftPos { get; set; }
        public BottomRightPointPos BotRightPos { get; set; }
        public RectDimensions Dimensions { get; set; }

    }

    internal class ItemsPos {
        public TopLeftPointPos TopLeftPos { get; set; }
        public BottomRightPointPos BotRightPos { get; set; }
        public RectDimensions Dimensions { get; set; }

    }

    internal class MoraPos {
        public TopLeftPointPos TopLeftPos { get; set; }
        public BottomRightPointPos BotRightPos { get; set; }
        public RectDimensions Dimensions { get; set; }

    }

    internal class TopLeftPointPos {
        public int X { get; set; }
        public int Y { get; set; }

    }

    internal class BottomRightPointPos {
        public int X { get; set; }
        public int Y { get; set; }

    }

    internal class RectDimensions {
        public int Width { get; set; }
        public int Height { get; set; }

    }
}
