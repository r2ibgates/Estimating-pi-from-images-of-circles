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
                Console.Write("Read image [..\\..\\5GScbUe.png]: ");
                fileName = Console.ReadLine();
                if (string.IsNullOrEmpty(fileName))
                    fileName = "..\\..\\5GScbUe.png";
            }
            if (string.IsNullOrEmpty(fileName))
                Environment.Exit(-1);
            fileName = System.IO.Path.Combine(Environment.CurrentDirectory, fileName);

            var circle = GetCircle(fileName);
            Console.WriteLine("Area = {0}, Center = {1}, {2}, Radius = {3}", circle.Area(), circle.Center().X, circle.Center().Y, circle.Radius());
            Console.WriteLine("Pi = {0}", circle.Pi());
            Console.ReadLine();
        }

        static Circle GetCircle(string imageFile)
        {
            Image img = Image.FromFile(imageFile);
            Bitmap bmp = new Bitmap(img);
            Color baseColor = bmp.GetPixel(0, 0);
            Color foreColor = Color.Black;
            Circle ret = new Circle();

            for(int x=0; x < (int)bmp.PhysicalDimension.Width; x++)
            {
                for (int y = 0; y < (int)bmp.PhysicalDimension.Height; y++)
                {
                    if (bmp.GetPixel(x, y).ToArgb() == foreColor.ToArgb())
                    {
                        ret.Points.Add(new Point(x, y));
                    }
                }
            }

            return ret;
        }
    }

    public class Circle
    {
        public List<Point> Points { get; set; }

        public Circle()
        {
            Points = new List<Point>();
        }

        public int Area()
        {
            return Points.Count;
        }

        public Point Center()
        {
            int startX = this.Points.Select(p => p.X).Min();
            int endX = this.Points.Select(p => p.X).Max();
            int startY = this.Points.Select(p => p.Y).Min();
            int endY = this.Points.Select(p => p.Y).Max();
            int x = (int)Math.Ceiling(((float)(endX - startX)) / 2f);
            int y = (int)Math.Ceiling(((float)(endY - startY)) / 2f);

            return new Point(x, y);
        }
       
        public int Radius()
        {
            int startX = this.Points.Select(p => p.X).Min();
            return this.Center().X - startX;
        }

        public float Pi()
        {
            return ((float)this.Area()) / (float)(Math.Pow((float)this.Radius(), 2));
        }
    }
}
