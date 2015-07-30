using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PiEstimater
{
    class Program
    {
        static void Main(string[] args)
        {
            string fileName = string.Empty;
            if ((args.Length == 2) && (!string.IsNullOrEmpty(args[1])))
                fileName = args[1];
            else
            {
                Console.Write("Read image: ");
                fileName = Console.ReadLine();
            }
            if (string.IsNullOrEmpty(fileName))
                Environment.Exit(-1);

            float radius = GetCircleRadius(fileName);
            Console.WriteLine("Radius = {0}", radius);
        }

        static float GetCircleRadius(string imageFile)
        {
            Image img = Image.FromFile(imageFile);
            Bitmap bmp = new Bitmap(img);
            int startX = 0, endX = 0, centerY = 0;
            Color baseColor = bmp.GetPixel(0, 0);
            Color foreColor = Color.Black;

            // First, find the y coordinate, starting at x=0. 
            // This y should be the furthest out x, meaning (maybe)
            // it's the middle of the image.
            for(int x=0; x <= (int)bmp.PhysicalDimension.Width; x++)
            {
                if (centerY == 0)
                {
                    for (int y = 0; y <= (int)bmp.PhysicalDimension.Height; y++)
                    {
                        if (bmp.GetPixel(x, y) != baseColor)
                        {
                            centerY = y;
                            startX = x;
                            foreColor = bmp.GetPixel(startX, centerY);
                            break;
                        }
                    }
                }
                else
                {
                    if (bmp.GetPixel(x, centerY) == baseColor)
                    {
                        endX = (x - 1);
                        break;
                    }
                }
            }

            return ((endX - startX) / 2f);
        }
    }
}
