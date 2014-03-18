using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2048
{
    public class Feature2 : Feature
    {
        //Count of pieces
        public Feature2()
        {
        }

        public float compute(int[][] board)
        {
            float res = 0.0f;

            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    if (board[i][j] != 0)
                        res++;
                }
            }

            return res;
        }
    }
}
