using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2048
{
    public static class Debug
    {
        public static void write_debug_file(Bitmap bmp, int[,] array)
        {
            string content_file = "";
            for (int i = 0; i <= 3; i++)
            {
                for (int j = 0; j <= 3; j++)
                {
                    content_file += array[i, j].ToString();
                    if (j != 3)
                        content_file += " | ";
                }
                content_file += "\n";
            }
            File.WriteAllText("C:\\PharmaGarde\\log.txt", content_file);
            bmp.Save("C:\\PharmaGarde\\log.bmp");

        }
    }
}
