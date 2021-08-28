using System.Drawing;

namespace CameraTest
{
    class Filter
    {
        private Color p;
        private int x, y, a, r, g, b, avg, gS, tr, tg, tb;

        public Bitmap GreySacale(Bitmap bmp)
        {
            for (x = 0; x < bmp.Width; x++)
            {
                for (y = 0; y < bmp.Height; y++)
                {
                    p = bmp.GetPixel(x, y);
                    gS = (int)((p.R * 0.3) + (p.G * 0.59) + (p.B * 0.11));
                    bmp.SetPixel(x, y, Color.FromArgb(p.A, gS, gS, gS));
                }
            }

            return bmp;
        }

        public Bitmap BlackAndWhite(Bitmap bmp)
        {
            for (x = 0; x < bmp.Width; x++)
            {
                for (y = 0; y < bmp.Height; y++)
                {
                    p = bmp.GetPixel(x, y);
                    
                    avg = (p.R + p.G + p.B) / 3;
                    avg = avg < 128 ? 0 : 255;

                    bmp.SetPixel(x, y, Color.FromArgb(p.A, avg, avg, avg));
                }
            }

            return bmp;
        }

        public Bitmap Negative(Bitmap bmp)
        {
            for (int y = 0; y < bmp.Height; y++)
            {
                for (int x = 0; x < bmp.Width; x++)
                {
                    p = bmp.GetPixel(x, y);

                    a = p.A;
                    r = p.R;
                    g = p.G;
                    b = p.B;

                    r = 255 - r;
                    g = 255 - g;
                    b = 255 - b;

                    bmp.SetPixel(x, y, Color.FromArgb(a, r, g, b));
                }
            }

            return bmp;
        }

        public Bitmap Sepia(Bitmap bmp)
        {
            for (int y = 0; y < bmp.Height; y++)
            {
                for (int x = 0; x < bmp.Width; x++)
                {
                    p = bmp.GetPixel(x, y);

                    a = p.A;
                    r = p.R;
                    g = p.G;
                    b = p.B;

                    tr = (int)(0.393 * r + 0.769 * g + 0.189 * b);
                    tg = (int)(0.349 * r + 0.686 * g + 0.168 * b);
                    tb = (int)(0.272 * r + 0.534 * g + 0.131 * b);

                    if (tr > 255)
                    {
                        r = 255;
                    }
                    else
                    {
                        r = tr;
                    }

                    if (tg > 255)
                    {
                        g = 255;
                    }
                    else
                    {
                        g = tg;
                    }

                    if (tb > 255)
                    {
                        b = 255;
                    }
                    else
                    {
                        b = tb;
                    }

                    bmp.SetPixel(x, y, Color.FromArgb(a, r, g, b));
                }
            }

            return bmp;
        }
    }
}
