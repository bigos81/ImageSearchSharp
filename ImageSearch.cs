using OpenCvSharp;
using OpenCvSharp.Extensions;
using Point = OpenCvSharp.Point;

namespace ImageSearchSharp
{
    public static class ImageSearch
    {
        public static Point FindImageInImage(Bitmap imageToSearch, Bitmap imgageToFind, double treshold)
        {
            using (Mat source = BitmapConverter.ToMat(imageToSearch))
            using (Mat template = BitmapConverter.ToMat(imgageToFind))
            using (Mat result = new(source.Rows - template.Rows + 1, source.Cols - template.Cols + 1, MatType.CV_32FC1))
            {
                Mat gsource = source.CvtColor(ColorConversionCodes.BGR2GRAY);
                Mat gtemplate = template.CvtColor(ColorConversionCodes.BGR2GRAY);

                Cv2.MatchTemplate(gtemplate, gsource, result, TemplateMatchModes.CCoeffNormed);
                Cv2.MinMaxLoc(result, out double minVal, out double maxVal, out Point minLoc, out Point maxLoc);
                if (maxVal > treshold)
                {
                    return maxLoc;
                }
            }

            return new Point(-1, -1);
        }
        
        public static List<Point> FindMultipleImagesInImage(Bitmap imageToSearch, Bitmap imageToFind, double treshold)
        {
            List<Point> resultPointList = [];
            using Mat source = BitmapConverter.ToMat(imageToSearch);
            using Mat template = BitmapConverter.ToMat(imageToFind);
            using Mat result = new(source.Rows - template.Rows + 1, source.Cols - template.Cols + 1, MatType.CV_32FC1);
            Mat gsource = source.CvtColor(ColorConversionCodes.BGR2GRAY);
            Mat gtemplate = template.CvtColor(ColorConversionCodes.BGR2GRAY);

            Cv2.MatchTemplate(gtemplate, gsource, result, TemplateMatchModes.CCoeffNormed);
            
            while (true)
            {
                Cv2.MinMaxLoc(result, out double minVal, out double maxVal, out Point minLoc, out Point maxLoc);
                if (maxVal > treshold)
                {
                    result.FloodFill(maxLoc, new Scalar(-1.0));
                    if (!ImageSearchHelpers.ContainsCloseEnoughPoint(resultPointList, maxLoc, template.Width, template.Height))
                        resultPointList.Add(new Point(maxLoc.X, maxLoc.Y));
                }
                else
                {
                    break;
                }
            }

            return resultPointList;
        }
    }
}
