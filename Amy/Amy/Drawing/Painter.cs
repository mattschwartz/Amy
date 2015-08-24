using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Amy.Drawing
{
    public class Painter : IDisposable
    {
        private Bitmap _bitmap;
        public Bitmap BitMap
        {
            get
            {
                return _bitmap;
            }
        }

        public Painter(int width, int height)
        {
            _bitmap = new Bitmap(width, height);
        }

        public void Do(Inputs input)
        {
            int pix;
            var ran = new Random();
            int red, green, blue;
            // Range of possible hues, from 0 to 255
            int maxHueRange = (int)(((float)input.Color / 100) * 255); // 0 - 255
            int michal = (int)((float)input.Randomness * 2.55);

            // Start at a random position
            int x = ran.Next(_bitmap.Width);
            int y = ran.Next(_bitmap.Height);
            int dirX = ran.Next(3) - 1;
            int dirY = ran.Next(3) - 1;
            int last = ran.Next(256);

            int startX = x;
            int startY = y;

            for (int i = 0; i < _bitmap.Width * _bitmap.Height * .10;) {
                if (ran.Next(100) <= input.Randomness) {
                    dirX = ran.Next(3) - 1;
                    dirY = ran.Next(3) - 1;
                }

                red = ran.Next(last, last + maxHueRange) % 256;
                green = (ran.Next(red, red + maxHueRange)) % 256;
                blue = (ran.Next(red, red + maxHueRange)) % 256;

                x += dirX;
                y += dirY;

                if (x < 0 || y < 0 || x >= _bitmap.Width || y >= _bitmap.Height) {
                    x = ran.Next(_bitmap.Width);
                    y = ran.Next(_bitmap.Height);
                    dirX = ran.Next(3) - 1;
                    dirY = ran.Next(3) - 1;
                    continue;
                }

                pix = (255 << 24) | (red << 16) | (green << 8) | (blue);
                _bitmap.SetPixel(x, y, Color.FromArgb(pix));
                ++i;
            }
        }

        public int GetSimilar(int x, int y)
        {
            int i = 1;
            int a = 0, r = 0, g = 0, b = 0;
            int pix = 0;

            for (var xx = x - 1; xx < x + 2; ++xx) {
                for (var yy = y - 1; yy < y + 2; ++yy) {
                    if (xx >= 0 && yy >= 0 && xx < _bitmap.Width && yy < _bitmap.Height) {
                        pix = _bitmap.GetPixel(xx, yy).ToArgb();
                        a += (pix >> 24);
                        r += (pix >> 16) & 255;
                        g += (pix >> 8) & 255;
                        b += (pix) & 255;
                        ++i;
                    }
                }
            }

            a /= i;
            r /= i;
            g /= i;
            b /= i;

            return (a << 24) | (r << 16) | (g << 8) | (b);
        }

        public void Save(string filename)
        {
            _bitmap.Save(filename);
        }

        public void Dispose()
        {
            _bitmap.Dispose();
            _bitmap = null;
        }
    }
}
