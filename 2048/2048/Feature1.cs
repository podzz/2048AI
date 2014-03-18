using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2048
{
    //Sum of pieces on the board
    public class Feature1 : Feature
    {
        public Feature1()
        {
        }

        public float compute(int[][] board)
        {
            float res = 0.0f;

            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    res += board[i][j];
                }
            }

            return res;
        }
    }
}
