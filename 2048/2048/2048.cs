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

            //Initialisation
            bmp_display = null;
            nav = IntPtr.Zero;
            array = new Array2048();
            dataGridView1.ReadOnly = true;

            //Check If Chrome is open and bind it to public var *nav* else exit with error
            foreach (Process pList in Process.GetProcesses())
                if (pList.MainWindowTitle.Contains("Chrome"))
                    nav = pList.MainWindowHandle;
            if (nav == null)
                print_error();

            //Check If Board is on the page else exit with error
            this.crop = get_bound_board(ScreenShot.PrintWindow(nav));
            if (this.crop.Item1 == -1 && this.crop.Item2 == -1)
                print_error();
            else
            {
                //Add 4 rows in the datagrid
                dataGridView1.Rows.Add(4);

                //Load
                refresh();

                //Focus the main form to keep bug if using keyboard
                this.Focus();
                loop_game();
            }

            
        }

        private void print_error()
        {
            MessageBox.Show("Lancez Chrome et une page du jeu 2048 puis réessayer", "Fail configuration", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            this.Close();
            Environment.Exit(0);
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
                        not_found = false;
                        save_x = i;
                        save_y = j;
                    }
                    j++;
                }
                j = 0;
                i++;
            }
            if (!not_found)
                return new Tuple<int, int>(save_x, save_y);
            else
                return new Tuple<int, int>(-1, -1);
        }

        private void refresh()
        {
            if (nav == null)
                return;
            Thread.Sleep(500);
            Bitmap bmp = ScreenShot.PrintWindow(nav);
            this.crop = get_bound_board(bmp);
            bmp_display = bmp.Clone(new Rectangle(this.crop.Item1, this.crop.Item2, 500, 500), bmp.PixelFormat);
            bmp.Dispose();
            array.update_array(bmp_display);
            for (int i = 0; i <= 3; i++)
                for (int j = 0; j <= 3; j++)
                    this.dataGridView1.Rows[i].Cells[j].Value = array.get_arr()[i, j];

            this.pictureBox1.Image = bmp_display;
            ScreenShot.SetForegroundWindow(this.Handle);
        }

        private int[,] norm_board(int[,] board)
        {
            int [,] new_board = new int[4, 4];

            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    new_board[j, i] = board[i, j];
                }
            }

            return new_board;
        }

        private void loop_game()
        {
            while (true)
            {
                //Move_Key move = Move_Key.DOWN; //= get_move()
                Move_Key move = (new Minimax(norm_board(array.get_arr()))).get_best_move();
                ScreenShot.SetForegroundWindow(nav);
                if (move == Move_Key.UP)
                    SendKeys.SendWait("{UP}");
                else if (move == Move_Key.DOWN)
                    SendKeys.SendWait("{DOWN}");
                else if (move == Move_Key.LEFT)
                    SendKeys.SendWait("{LEFT}");
                else if (move == Move_Key.RIGHT)
                    SendKeys.SendWait("{RIGHT}");
                else if (move == Move_Key.SPACE)
                    SendKeys.SendWait(" ");
                refresh();
            }

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
            else if (e.KeyCode == Keys.Escape)
                print_error();
            refresh();
        }

        private void dataGridView1_KeyDown(object sender, KeyEventArgs e)
        {
            Form1_KeyDown(null, e);
            e.Handled = true;
            e.SuppressKeyPress = true;
        }
    }
}