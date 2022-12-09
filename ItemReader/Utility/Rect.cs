using ItemReader.Models;

namespace ItemReader.Utility
{

    public struct Rect {

        /* CLASS VARIABLE(S) */
        
        public Point topLeft;
        public Point bottomRight;
        public Size rectSize;

        /* PUBLIC METHOD(S) */
        
        public Rect(int x, int y, int x2, int y2)
        {
            topLeft.X = x;
            topLeft.Y = y;
            bottomRight.X = x2;
            bottomRight.Y = y2;
            rectSize.Width = x2 - x;
            rectSize.Height = y2 - y;
        }
        public Rect(TopLeftPointPos TopLeftPos, BottomRightPointPos BotRightPos, RectDimensions Dimensions)
        {
            topLeft.X = TopLeftPos.X;
            topLeft.Y = TopLeftPos.Y;
            bottomRight.X = BotRightPos.X;
            bottomRight.Y = BotRightPos.Y;
            rectSize.Width = Dimensions.width;
            rectSize.Height = Dimensions.height;
        }

        /* PRIVATE METHOD(S) */

    }
}
