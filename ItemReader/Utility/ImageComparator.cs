namespace ItemReader.Utility
{
    internal class ImageComparator
    {

        /* CLASS VARIABLE(S) */
        /* PUBLIC METHOD(S) */

        public ImageComparator() { }

        public bool SameImage(Bitmap img1, Bitmap img2)
        {   
            if (img1 == null || img2 == null)
                return false;
            else if (img1 == img2)
                return true;
            return false;
        }

        /* PRIVATE METHOD(S) */

    }
}
