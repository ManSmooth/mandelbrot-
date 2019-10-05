using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

namespace Project1
{
    public partial class Form1 : Form
    {
        private Bitmap image1 = new Bitmap(1600, 900);
        int n = 0;
        double minx = -2.5;
        double maxx = 1;
        double midx,midy;
        double offsetx, offsety;
        double scaling, rangex, rangey;
        int maxiter = 250;
        double tempiter;
        Color[] Palette;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            midy = 0;
            rangex = maxx - minx;
            midx = maxx - (rangex / 2);
            scaling = image1.Width / rangex;
            rangex = image1.Width / scaling;
            offsetx = midx - (rangex / 2);
            rangey = image1.Height / scaling;
            offsety = midy - (rangey / 2);
            tempiter = maxiter;
            Console.WriteLine(offsetx + "," + offsety);
            Console.WriteLine(scaling + " " + midx + " " + rangex);
            Console.WriteLine("LOADED");
            this.uppodeto();
            this.KeyPress += pictureBox1_KeyPress;
        }

        private void PictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void uppodeto()
        {
            int[] numiterofpix = new Int32[maxiter];
            int[,] iterc = new Int32[1600,900];
            Console.WriteLine(scaling);
            Console.WriteLine(maxiter);
            Console.WriteLine("x = [" + (midx - (rangex / 2)) + "," + (midx + (rangex / 2)) + "]");
            Console.WriteLine("y = [" + (midy - (rangey / 2)) + "," + (midy + (rangey / 2)) + "]");
            for (int x = 0; x < image1.Width; x++)
            {
                double scaled_x = ((x / scaling) + offsetx);
                for (int y = 0; y < image1.Height; y++)
                {
                    double scaled_y = ((-y / scaling) - offsety);
                    int iter = 0;
                    double a = 0;
                    double b = 0;
                    while ((a * a) + (b * b) <= 2 * 2 && iter < maxiter)
                    {
                        double xtemp = (a * a) - (b * b) + scaled_x;
                        b = (2 * a * b) + scaled_y;
                        a = xtemp;
                        iter++;
                    }
                    double diter = iter;
                    double dmaxiter = maxiter;
                    int c = Convert.ToInt32((256*(diter/dmaxiter)%256));
                    if(c >= 256)
                    {
                        c = 0;
                    }
                    Color newColor = Color.FromArgb(c, c, c);
                    image1.SetPixel(x, y, newColor);
                }
            }
            this.pictureBox1.Image = image1;
            this.Refresh();
            Console.WriteLine("DONE");
        }

        private void pictureBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            Console.WriteLine($"KeyPress keychar: {e.KeyChar}" + "\r\n");
            if (e.KeyChar == '+')
            {
                scaling *= 2;
                rangex = image1.Width / scaling;
                offsetx = midx - (rangex / 2);
                rangey = image1.Height / scaling;
                offsety = midy - (rangey / 2);
                this.uppodeto();
            }
            if (e.KeyChar == '-')
            {
                scaling /= 2;
                rangex = image1.Width / scaling;
                offsetx = midx - (rangex / 2);
                rangey = image1.Height / scaling;
                offsety = midy - (rangey / 2);
                this.uppodeto();
            }
            if (e.KeyChar == 'w')
            {
                midy -= (400 / scaling) / 2;
                offsety = midy - (rangey / 2);
                this.uppodeto();
            }
            if (e.KeyChar == 'a')
            {
                midx -= (400 / scaling) / 2;
                offsetx = midx - (rangex / 2);
                this.uppodeto();
            }
            if (e.KeyChar == 's')
            {
                midy += (400 / scaling) / 2;
                offsety = midy - (rangey / 2);
                this.uppodeto();
            }
            if (e.KeyChar == 'd')
            {
                midx += (400 / scaling) / 2;
                offsetx = midx - (rangex / 2);
                this.uppodeto();
            }
            if (e.KeyChar == 'q')
            {
                tempiter /= 2;
                maxiter = (int)tempiter;
                this.uppodeto();
            }
            if (e.KeyChar == 'e')
            {
                tempiter *= 2;
                maxiter = (int)tempiter;
                this.uppodeto();
            }
        }
    }
}
