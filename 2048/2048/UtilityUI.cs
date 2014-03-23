using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _2048
{
    public static class UtilityUI
    {
        public static Tuple<int, int> get_bound_board(Bitmap bmp)
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

        public static IntPtr auto_start()
        {
            Tuple<int, int> crop;
            Process.Start("chrome.exe", "http://gabrielecirulli.github.io/2048/");
            IntPtr nav = UtilityUI.get_chrome();
            if (nav == IntPtr.Zero)
                UtilityUI.print_error();
            crop = UtilityUI.get_bound_board(ScreenShot.PrintWindow(nav));
            while (crop.Item1 == -1 && crop.Item2 == -1)
            {
                Thread.Sleep(1000);
                crop = UtilityUI.get_bound_board(ScreenShot.PrintWindow(nav));
            }
            return nav;
        }
        
        public static IntPtr get_chrome()
        {
            IntPtr nav = IntPtr.Zero;

            foreach (Process pList in Process.GetProcesses())
                if (pList.MainWindowTitle.Contains("Chrome"))
                    nav = pList.MainWindowHandle;
            return nav;
        }

        public static void print_error()
        {
            MessageBox.Show("IA Abort", "IA Abord", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Environment.Exit(0);
        }

        public static void print_information()
        {
            MessageBox.Show("Chrome va être lancé automatiquement.\nPour arrêter l'exécution, veuillez ne plus afficher 2048 dans votre navigateur.\n" +
                            "Si aucune action n'est effectué dans 2048 cliquez sur la fenêtre de chrome pour aider ce programme à prendre le focus",
                            "Information de démarrage", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public static int run_fast_compute()
        {
            DialogResult dr = MessageBox.Show("FAST ?", "FAST ?", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);
            if (dr == DialogResult.Yes)
                return 1;
            else if (dr == DialogResult.No)
                return 0;
            else if (dr == DialogResult.Cancel)
                return -1;
            return -1;
        }

    }
}
