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

        /* PRIVATE METHOD(S) */

    }
}
