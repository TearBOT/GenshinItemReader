using ItemReader.Models;

namespace ItemReader.Utils
{

    public struct Rect {

        /* CLASS VARIABLE(S) */
        
        public Point TopLeft;
        public Point BottomRight;
        public Size RectSize;

        /* PUBLIC METHOD(S) */
        
        public Rect(int X, int Y, int X2, int Y2)  {
            TopLeft.X = X;
            TopLeft.Y = Y;

            BottomRight.X = X2;
            BottomRight.Y = Y2;

            RectSize.Width = X2 - X;
            RectSize.Height = Y2 - Y;
        }

        public Rect(TopLeftPointPos TopLeftPos, BottomRightPointPos BotRightPos, RectDimensions Dimensions) {
            TopLeft.X = TopLeftPos.X;
            TopLeft.Y = TopLeftPos.Y;

            BottomRight.X = BotRightPos.X;
            BottomRight.Y = BotRightPos.Y;

            RectSize.Width = Dimensions.width;
            RectSize.Height = Dimensions.height;
        }

    }
}
