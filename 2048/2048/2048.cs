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
        protected Thread thread;
        protected IntPtr nav;

        public Form1()
        {
            InitializeComponent();
            nav = IntPtr.Zero;
            foreach (Process pList in Process.GetProcesses())
                if (pList.MainWindowTitle.Contains("Chrome"))
                    nav = pList.MainWindowHandle;
            thread = new Thread(new ThreadStart(refresh));
            thread.Start();

        }

        private void refresh()
        {
            Rectangle rect = new Rectangle();
            ScreenShot.GetWindowRect(this.nav, out rect);
            Rectangle cut = new Rectangle(rect.Width / 2 - 249, 330, 495, 495);
            Bitmap bmp = null;
            while (true)
            {
                bmp = ScreenShot.PrintWindow(nav);
                Bitmap bmp2 = bmp.Clone(cut, bmp.PixelFormat);
                bmp.Dispose();
                this.pictureBox1.Image = bmp2;
                // IMAGE DE 495 x 495
            }
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.thread.Abort();
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
        }
    }
}