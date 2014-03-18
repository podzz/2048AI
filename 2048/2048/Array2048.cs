﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2048
{
    class Array2048
    {
        private int[,] array_int;
        public Array2048()
        {
            array_int = new int[4,4];
            for (int i = 0; i <= 3; i++)
                for (int j = 0; j <= 3; j++)
                    array_int[i, j] = 0;
        }

        public List<Point> update_array(Bitmap bmp)
        {
            int padd_x = 120;
            int padd_y = 120;

            int x = 70;
            int y = 30;

            List<Point> list_point = new List<Point>();

            for (int i = 0; i <= 3; i++)
            {
                for (int j = 0; j <= 3; j++)
                {
                    this.array_int[i,j] = get_value_by_color(bmp.GetPixel(x, y));
                    list_point.Add(new Point(x, y));
                    x += padd_x;
                }
                x = 70;
                y+= padd_y;
            }
            display_array();
            return list_point;
            
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

        public int[,] get_arr()
        {
            return this.array_int;
        }

        public int get_value_by_color(Color pix)
        {
            if (pix.R == 204 && pix.G == 192 && pix.B == 179)
                return 0;
            else if (pix.R == 238 && pix.G == 228 && pix.B == 218)
                return 2;
            else if (pix.R == 237 && pix.G == 224 && pix.B == 200)
                return 4;
            else if (pix.R == 242 && pix.G == 177 && pix.B == 121)
                return 8;
            else if (pix.R == 245 && pix.G == 149 && pix.B == 99)
                return 16;
            else if (pix.R == 246 && pix.G == 124 && pix.B == 95)
                return 32;
            else if (pix.R == 246 && pix.G == 94 && pix.B == 59)
                return 64;
            else if (pix.R == 237 && pix.G == 207 && pix.B == 114)
                return 128;
            else if (pix.R == 237 && pix.G == 204 && pix.B == 97)
                return 256;
            else if (pix.R == 237 && pix.G == 200 && pix.B == 80)
                return 512;
            else if (pix.R == 237 && pix.G == 197 && pix.B == 63)
                return 1024;
            else
                return 0;
        }

    }
}
