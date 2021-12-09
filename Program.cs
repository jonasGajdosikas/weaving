using System;
using System.Drawing;

namespace weaving
{
    internal class Program
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Interoperability", "CA1416:Validate platform compatibility", Justification = "<Pending>")]
        static void Main()
        {
            int fac = 10;
            int width = 16 * fac, height = 9 * fac;
            int[] horizontalMod = randomBools(width, 0.4f),
               verticalMod = randomBools(height, 0.6f);
            int cellSize = 4;
            int imgW = width * cellSize + 1,
                imgH = height * cellSize + 1;
            Bitmap bitmap = new Bitmap(imgW, imgH);
            cell[,] cells = new cell[width, height];
            Color borderColor = Color.Black;
            Color lineColor1 = Color.FromArgb(165, 70, 87);
            Color lineColor2 = Color.FromArgb(12, 116, 137);
            Color fillColor1 = Color.FromArgb(215, 217, 206);
            Color fillColor2 = Color.FromArgb(105, 72, 115);
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    cells[x, y] = new cell();
                    cells[x, y].right = (y + horizontalMod[x]) % 2 == 1;
                    cells[x, y].bottom = (x + verticalMod[y]) % 2 == 1;
                    if (x > 0) cells[x, y].color = cells[x - 1, y].right ^ cells[x - 1, y].color;
                    else if (y > 0) cells[x, y].color = cells[x, y - 1].bottom ^ cells[x, y - 1].color;
                    else cells[x, y].color = true;
                    for (int dx = 1; dx < cellSize; dx++)
                    {
                        for (int dy = 1; dy < cellSize; dy++)
                        {
                            bitmap.SetPixel(x * cellSize + dx, y * cellSize + dy, (cells[x, y].color ? fillColor1 : fillColor2));
                        }
                    }
                    for (int dx = 0; dx <= cellSize; dx++)
                        bitmap.SetPixel(x * cellSize + dx, (y + 1) * cellSize, (cells[x, y].bottom) ? lineColor1 : (cells[x, y].color ? fillColor1 : fillColor2));
                    for (int dy = 0; dy <= cellSize; dy++)
                        bitmap.SetPixel((x + 1) * cellSize, y * cellSize + dy, (cells[x, y].right) ? lineColor2 : (cells[x, y].color ? fillColor1 : fillColor2));
                }
            }
            for (int x = 0; x < imgW; x++)
                bitmap.SetPixel(x, 0, borderColor);
            for (int y = 1; y < imgH; y++)
                bitmap.SetPixel(0, y, borderColor);
            for (int x = 0; x < imgW; x++)
                bitmap.SetPixel(x, imgH - 1, borderColor);
            for (int y = 1; y < imgH; y++)
                bitmap.SetPixel(imgW - 1, y, borderColor);
            bitmap.Save("output.png");
            Console.WriteLine("Hello World!");
        }
        static int[] randomBools(int amt, float fill)
        {
            int[] arr = new int[amt];
            Random rand = new Random();
            for (int i = 0; i < amt; i++)
                arr[i] = (rand.NextDouble() < fill) ? 1 : 0;
            return arr;
        }
    }
    class cell
    {
        public bool color;
        public bool right;
        public bool bottom;
        public cell()
        {
            color = false;
            right = false;
            bottom = false;
        }
    }
}
