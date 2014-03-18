using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2048
{
    class Array2048
    {
        public enum CellValue
        {
            Undefined,
            N0,
            N2,
            N4,
            N8,
            N16,
            N32,
            N64,
            N128,
            N256,
            N512,
            N1024,
            N2048
        };
        private CellValue[,] array_int;
        public Array2048()
        {
            array_int = new CellValue[4,4];
            for (int i = 0; i <= 3; i++)
                for (int j = 0; j <= 3; j++)
                    array_int[i,j] = CellValue.Undefined;
        }

        public void update_array(Bitmap bmp)
        {
            int padd_x = bmp.Width / 4 - 50;
            int padd_y = bmp.Height / 4 - 50;

            int x = bmp.Width / 4 - 50;
            int y = bmp.Height / 4 - 50;


            for (int i = 0; i <= 3; i++)
            {
                for (int j = 0; j <= 3; j++)
                {
                    this.array_int[i,j] = get_value_by_color(bmp.GetPixel(x, y));
                    y+= padd_y;
                }
                y = 0;
                x+= padd_x;
            }
            display_array();
            
        }

        public void display_array()
        {
            for (int i = 0; i <= 3; i++)
            {
                for (int j = 0; j <= 3; j++)
                {

                    Console.Write(this.array_int[i, j] + " | ");
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }

        public CellValue[,] get_arr()
        {
            return this.array_int;
        }

        public CellValue get_value_by_color(Color pix)
        {
            if (pix.R == 204 && pix.G == 192 && pix.B == 179)
                return CellValue.N0;
            else
                return CellValue.Undefined;
        }

    }
}
