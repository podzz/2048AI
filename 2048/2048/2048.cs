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
using System.IO;

namespace _2048
{
    public partial class Form1 : Form
    {
        protected IntPtr nav;
        protected ImageProcess imageprocess;
        protected Board board;

        public Form1()
        {
            InitializeComponent();

            int code = UtilityUI.run_fast_compute();
            if (code == -1)
                UtilityUI.print_error();
            else if (code == 0)
            {
                UtilityUI.print_information();
                this.nav = UtilityUI.auto_start();
                imageprocess = new ImageProcess(ScreenShot.PrintWindow(nav), false);
                game();
            }
            else if (code == 1)
            {
                this.board = new Board();
                imageprocess = new ImageProcess(Properties.Resources.log, true);
                this.pictureBox1.Image = imageprocess.get_img();
            }
        }

        private int[,] refresh()
        {
            Thread.Sleep(500);
            imageprocess.process_cell(ScreenShot.PrintWindow(nav));
            return imageprocess.get_array();
        }

        private void game()
        {
            int[,] arr = refresh();
            while (true)
            {
                Move_Key move = (new Minimax(arr)).get_best_move();
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
                arr = refresh();
            }
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (this.board != null)
            {
                this.board.move(e.KeyCode);
                this.board.display_board();
                this.imageprocess.draw_board(this.board.get_array());
                this.pictureBox1.Image = imageprocess.get_img();
            }
        }

    }
}