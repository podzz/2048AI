using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2048
{
    public class Feature6 : Feature
    {
        public Feature6()
        {

        }
        public float compute(int[,] board)
        {
            int empty = 0;
            for (int i = 0; i < 3; i++)
                for (int j = 0; j < 3; j++)
                    if (board[i, j] == 0)
                        empty++;
            return (float) Math.Log(empty);
        }
    }
}
