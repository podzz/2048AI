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
        protected Bitmap bmp_display;
        protected Tuple<int, int> crop;

        public Form1()
        {
            InitializeComponent();
            bmp_display = null;
            nav = IntPtr.Zero;
            array = new Array2048();
            foreach (Process pList in Process.GetProcesses())
                if (pList.MainWindowTitle.Contains("Chrome"))
                    nav = pList.MainWindowHandle;
            dataGridView1.Rows.Add();
            dataGridView1.Rows.Add();
            dataGridView1.Rows.Add();
            dataGridView1.Rows.Add();
            refresh();
            dataGridView1.ReadOnly = true;
            this.Focus();


        }

        private Tuple<int, int> get_bound_board(Bitmap bmp)
        {
            bool not_found = true;
            int i = 0;
            int j = 0;

            int save_x = 0;
            int save_y = 0;
            while (i < bmp.Width && not_found)
            {
                while (j < bmp.Height && not_found)
                {
                    if (bmp.GetPixel(i, j) == Color.FromArgb(187, 173, 160)) // CODE RGB BORDURE
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
            return new Tuple<int, int>(save_x, save_y);
        }

        private void print_red_pixel(List<Point> list_point)
        {
            foreach (Point p in list_point)
                bmp_display.SetPixel(p.X, p.Y, Color.Red);
        }

        private void refresh()
        {
            if (nav == null)
                return;
            Thread.Sleep(400);
            Bitmap bmp = ScreenShot.PrintWindow(nav);
            if (bmp_display == null)
                this.crop = get_bound_board(bmp);
            bmp_display = bmp.Clone(new Rectangle(this.crop.Item1, this.crop.Item2, 500, 500), bmp.PixelFormat);
            bmp.Dispose();

            /* COORD INIT bmp2.SetPixel(70, 30, Color.Red);
             ESPACE ENTRE 2 POINTS 120
             IMAGE DE 495 x 495 */

            List<Point> list_point_debug = array.update_array(bmp_display);
            print_red_pixel(list_point_debug);

            for (int i = 0; i <= 3; i++)
                for (int j = 0; j <= 3; j++)
                {
                    this.dataGridView1.Rows[i].Cells[j].Value = array.get_arr()[i, j];
                }

                /* !!!!!!!!!!!!!!!!!!!!!!!!! */

                this.pictureBox1.Image = bmp_display;
            ScreenShot.SetForegroundWindow(this.Handle);
           
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
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
            refresh();
        }

        private void dataGridView1_KeyDown(object sender, KeyEventArgs e)
        {
            Form1_KeyDown(null, e);
            e.Handled = true;
            e.SuppressKeyPress = true;
        }



    }

    public class NoArrowKeysDataGridView : DataGridView
    {
        protected override void OnKeyDown(KeyEventArgs e)
        {
            switch (e.KeyData & Keys.KeyCode)
            {
                case Keys.Up:
                case Keys.Right:
                case Keys.Down:
                case Keys.Left:
                    if (!this.IsCurrentCellInEditMode)
                    {
                        // Swallow arrow keys.
                        e.Handled = true;
                        e.SuppressKeyPress = true;
                    }
                    break;
            }
            base.OnKeyDown(e);
            
        }
    }
}