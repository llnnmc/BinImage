using System.Drawing;
using System.Drawing.Imaging;

namespace binImg
{
    public class BinImg
    {
        private Bitmap bmp;

        // 构造图像对象
        public BinImg(string imgFile)
        {
            bmp = new Bitmap(imgFile, true);

        }

        // 图像灰度化
        public Bitmap ToGray()
        {
            for (int i = 0; i < bmp.Width; i++)
            {
                for (int j = 0; j < bmp.Height; j++)
                {
                    // 获取像素点颜色
                    Color pixelColor = bmp.GetPixel(i, j);

                    // 计算灰度值
                    int gray = (int)(0.299 * pixelColor.R + 0.587 * pixelColor.G + 0.114 * pixelColor.B);
                    Color newColor = Color.FromArgb(gray, gray, gray);
                    bmp.SetPixel(i, j, newColor);
                }
            }
            return bmp;
        }

        // 灰度图像二值化
        public Bitmap Binaryzation()
        {
            int[] histogram = new int[256];
            int minGrayValue = 255, maxGrayValue = 0;

            // 求取直方图
            for (int i = 0; i < bmp.Width; i++)
            {
                for (int j = 0; j < bmp.Height; j++)
                {
                    Color pixelColor = bmp.GetPixel(i, j);
                    histogram[pixelColor.R]++;
                    if (pixelColor.R > maxGrayValue) maxGrayValue = pixelColor.R;
                    if (pixelColor.R < minGrayValue) minGrayValue = pixelColor.R;
                }
            }

            // 迭代计算阀值
            int threshold = -1;
            int newThreshold = (minGrayValue + maxGrayValue) / 2;
            for (int iterationTimes = 0; threshold != newThreshold && iterationTimes < 100; iterationTimes++)
            {
                threshold = newThreshold;
                int lP1 = 0;
                int lP2 = 0;
                int lS1 = 0;
                int lS2 = 0;

                // 求两个区域的灰度的平均值
                for (int i = minGrayValue; i < threshold; i++)
                {
                    lP1 += histogram[i] * i;
                    lS1 += histogram[i];
                }
                int mean1GrayValue = (lP1 / lS1);
                for (int i = threshold + 1; i < maxGrayValue; i++)
                {
                    lP2 += histogram[i] * i;
                    lS2 += histogram[i];
                }
                int mean2GrayValue = (lP2 / lS2);
                newThreshold = (mean1GrayValue + mean2GrayValue) / 2;
            }

            // 二值化
            for (int i = 0; i < bmp.Width; i++)
            {
                for (int j = 0; j < bmp.Height; j++)
                {
                    Color pixelColor = bmp.GetPixel(i, j);
                    if (pixelColor.R > threshold)
                    {
                        bmp.SetPixel(i, j, Color.FromArgb(255, 255, 255));
                    }
                    else
                    {
                        bmp.SetPixel(i, j, Color.FromArgb(0, 0, 0));
                    }
                }
            }
            return bmp;
        }

        // 二值化图像反转
        public Bitmap Reverse()
        {
            for (int i = 0; i < bmp.Width; i++)
            {
                for (int j = 0; j < bmp.Height; j++)
                {
                    Color pixelColor = bmp.GetPixel(i, j);
                    Color newColor = Color.FromArgb(255 - pixelColor.R, 255 - pixelColor.G, 255 - pixelColor.B);
                    bmp.SetPixel(i, j, newColor);
                }
            }
            return bmp;
        }

        // 图像裁切
        public Bitmap Tailor(Rectangle rect)
        {
            bmp = bmp.Clone(rect, PixelFormat.DontCare);
            return bmp;
        }

        // 保存图像文件
        public void SaveImg(string imgFile)
        {
            bmp.Save(imgFile);
        }
    }
}
