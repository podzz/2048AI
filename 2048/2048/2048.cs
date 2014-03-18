using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.Drawing.Imaging;

namespace _2048
{
    public partial class Form1 : Form
    {
        private Array2048 array;
        protected IntPtr nav;

        public Form1()
        {
            InitializeComponent();
            nav = IntPtr.Zero;
            array = new Array2048();
            foreach (Process pList in Process.GetProcesses())
                if (pList.MainWindowTitle.Contains("Chrome"))
                    nav = pList.MainWindowHandle;
            refresh();
            

        }

        private void refresh()
        {
            if (nav == null)
                return;
            Rectangle rect = new Rectangle();
            ScreenShot.GetWindowRect(this.nav, out rect);
            Bitmap bmp = null;
            bmp = ScreenShot.PrintWindow(nav);
            // R/G/B : 187/173/160
            bool not_found = true;
            int i = 0;
            int j = 0;

            int save_x = 0;
            int save_y = 0;
            while (i < bmp.Width && not_found)
            {
                while (j < bmp.Height && not_found)
                {
                    if (bmp.GetPixel(i, j) == Color.FromArgb(187, 173, 160))
                    {
                        Console.WriteLine("First coord : x = " + i + " y = " + j);
                        not_found = false;
                        save_x = i;
                        save_y = j;
                    }
                    j++;
                }
                j = 0;
                i++;
            }
            Bitmap bmp2 = bmp.Clone(new Rectangle(save_x, save_y, 495, 495), bmp.PixelFormat);
            bmp.Dispose();
            //          array.update_array(bmp2);
            this.pictureBox1.Image = bmp2;
            // IMAGE DE 495 x 495
        }

        private void splitContainer1_KeyDown(object sender, KeyEventArgs e)
        {
            ScreenShot.SetForegroundWindow(nav);
            if (e.KeyCode == Keys.Up)
                SendKeys.SendWait("{UP}");
            else if (e.KeyCode == Keys.Down)
                SendKeys.SendWait("{DOWN}");
            else if (e.KeyCode == Keys.Left)
                SendKeys.SendWait("{LEFT}");
            else if (e.KeyCode == Keys.Right)
                SendKeys.SendWait("{RIGHT}");
            else if (e.KeyCode == Keys.Space)
                SendKeys.SendWait(" ");
            ScreenShot.SetForegroundWindow(this.Handle);
            Thread.Sleep(500);
            refresh();
        }

        private void hScrollBar1_ValueChanged(object sender, EventArgs e)
        {
            refresh();
        }

        private void hScrollBar2_ValueChanged(object sender, EventArgs e)
        {
            refresh();
        }
    }
}