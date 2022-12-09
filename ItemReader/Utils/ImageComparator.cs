namespace ItemReader.Utils
{

    internal static class ImageComparator {

        /* CLASS VARIABLE(S) */

        private const int VARIANCE = 8;
        private const double ERROR_MARGIN = 0.05;

        /* PUBLIC METHOD(S) */

        public static bool ComapreImages(Bitmap Image1, Bitmap Image2)
        {
            if (Image1.Width != Image2.Width
                || Image1.Height != Image2.Height) {
                return false;
            }

            int DifferentPixelCount = 0;

            for (int i = 0; i < Image1.Width; i++) {
                for (int j = 0; j < Image1.Height; j++) {
                    Color px_ref = Image1.GetPixel(i, j);
                    Color px_test = Image2.GetPixel(i, j);

                    if (px_ref.R < px_test.R - VARIANCE || px_ref.R > px_test.R + VARIANCE
                        || px_ref.G < px_test.G - VARIANCE || px_ref.G > px_test.G + VARIANCE
                        || px_ref.B < px_test.B - VARIANCE || px_ref.B > px_test.B + VARIANCE
                        ) {
                        DifferentPixelCount++;
                    }
                }
            }

            // Allowing an ERROR_MARGIN difference on top of the VARIANCE
            var ErrorMargin = Image1.Width * Image1.Height * ERROR_MARGIN;
            if (DifferentPixelCount <= ErrorMargin) {
                return true;
            }

            return false;
        }
        // TODO: compare image with unique pixels

    }
}
