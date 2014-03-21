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
        protected Tuple<int, int> crop;
        protected ImageProcess imageprocess;

        public Form1()
        {
            InitializeComponent();
            UtilityUI.print_information();
            this.nav = UtilityUI.auto_start(ref this.crop);
            imageprocess = new ImageProcess(ScreenShot.PrintWindow(nav));
            game();
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
    }
}