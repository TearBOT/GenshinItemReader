using ItemReader.Utils;

namespace ItemReader.Models
{
    internal class GenshinItemCoordinates {

        /* CLASS VARIABLE(S) */

        public Rect BagIconPos { get; set; }
        public List<Rect> FirstLineItemsPos { get; set; }
        public List<Rect> LastLineItemsPos { get; set; }
        public Rect MoraPos { get; set; }

        /* PUBLIC METHOD(S) */

        public GenshinItemCoordinates(Rect bagIconPos, List<Rect> firstLineItemsPos, List<Rect> lastLineItemsPos, Rect moraPos)
        {
            BagIconPos = bagIconPos;
            FirstLineItemsPos = firstLineItemsPos;
            LastLineItemsPos = lastLineItemsPos;
            MoraPos = moraPos;
        }

    }
}
