namespace ItemReader.Utility
{
    internal class ImageComparator
    {

        /* CLASS VARIABLE(S) */
        /* PUBLIC METHOD(S) */

        public ImageComparator() { }

        public static bool ComapreImages(Bitmap img1, Bitmap img2)
        {
            int variance = 8;
            int diff_counter = 0;
            int same_counter = 0;

            if (img1.Width == img2.Width && img1.Height == img2.Height)
            {
                for (int i = 0; i < img1.Width; i++)
                {
                    for (int j = 0; j < img1.Height; j++)
                    {
                        Color px_ref = img1.GetPixel(i, j);
                        Color px_test = img2.GetPixel(i, j);
                        if (px_ref.R - variance <= px_test.R && px_test.R <= px_ref.R + variance
                            && px_ref.G - variance <= px_test.G && px_test.G <= px_ref.G + variance
                            && px_ref.B - variance <= px_test.B && px_test.B <= px_ref.B + variance
                            )
                        {
                            same_counter++;
                        }
                        else
                            diff_counter++;
                    }
                }
                var imgSize = img1.Width * img1.Height;
                if (diff_counter <= imgSize * 0.05) {
                    return true;
                }
            }
            return false;
        }

        /* PRIVATE METHOD(S) */

    }
}
