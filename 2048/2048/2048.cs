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
        public Form1()
        {
            InitializeComponent();
            thread = new Thread(new ThreadStart(refresh));
            thread.Start();
            
        }

        void refresh()
        {
            IntPtr chrome = IntPtr.Zero;
            foreach (Process pList in Process.GetProcesses())
                if (pList.MainWindowTitle.Contains("Chrome"))
                    chrome = pList.MainWindowHandle;
            while (true)
            {
                Rectangle rect = new Rectangle();
                ScreenShot.GetWindowRect(chrome, out rect);
                var bmp = ScreenShot.PrintWindow(chrome);
                var bmp2 = bmp.Clone(new Rectangle(rect.Width / 2 - 249, 330, 495, 495), bmp.PixelFormat);

                // IMAGE DE 495 x 495
                this.pictureBox1.Image = bmp2;
                Thread.Sleep(1000);
            }
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.thread.Abort();
        }
    }
}
