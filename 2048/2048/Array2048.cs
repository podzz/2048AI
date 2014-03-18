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
        private int[,] array_int;
        public Array2048()
        {
            array_int = new int[4,4];
            for (int i = 0; i <= 3; i++)
                for (int j = 0; j <= 3; j++)
                    array_int[i,j] = 0;
        }

        public void update_array(Bitmap bmp)
        {
            
        }

        public int[,] get_arr()
        {
            return this.array_int;
        }

    }
}
