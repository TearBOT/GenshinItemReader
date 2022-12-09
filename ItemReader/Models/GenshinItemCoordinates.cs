using ItemReader.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItemReader.Models
{
    internal class GenshinItemCoordinates
    {
        public Rect BagIconPos { get; set; }
        public List<Rect> FirstLineItemsPos { get; set; }
        public List<Rect> LastLineItemsPos { get; set; }
        public Rect MoraPos { get; set; }

        public GenshinItemCoordinates(Rect bagIconPos, List<Rect> firstLineItemsPos, List<Rect> lastLineItemsPos, Rect moraPos)
        {
            BagIconPos = bagIconPos;
            FirstLineItemsPos = firstLineItemsPos;
            LastLineItemsPos = lastLineItemsPos;
            MoraPos = moraPos;
        }
    }
}
