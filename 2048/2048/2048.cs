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
            UtilityUI.print_information();

            this.bmp_display = null;
            this.array = new Array2048();
            this.nav = UtilityUI.auto_start(ref this.crop);

            //Add 4 rows in the datagrid
            dataGridView1.Rows.Add(4);

            //Load
            game();
        }

        private void refresh()
        {
            Thread.Sleep(500);
            Bitmap bmp = ScreenShot.PrintWindow(nav);
            this.crop = UtilityUI.get_bound_board(bmp);
            if (this.crop.Item1 == -1 && this.crop.Item2 == -1)
                UtilityUI.print_error();
            bmp_display = bmp.Clone(new Rectangle(this.crop.Item1, this.crop.Item2, 500, 500), bmp.PixelFormat);
            bmp.Dispose();
            array.update_array(bmp_display);
            for (int i = 0; i <= 3; i++)
                for (int j = 0; j <= 3; j++)
                    this.dataGridView1.Rows[i].Cells[j].Value = array.get_arr()[i, j];

            this.pictureBox1.Image = bmp_display;
            ScreenShot.SetForegroundWindow(this.Handle);
            this.Focus();
        }

        private int[,] fran_to_po(int[,] board)
        {
            int[,] new_board = new int[4, 4];

            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    new_board[i, j] = board[j, i];
                }
            }

            return new_board;
        }

        private void game()
        {
            while (true)
            {
                /* GET_MOVE IA */
                //Move_Key move = Move_Key.DOWN; //= get_move()

                Move_Key move = (new Minimax(fran_to_po(array.get_arr()))).get_best_move();

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
    }
}