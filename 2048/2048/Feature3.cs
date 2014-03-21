using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2048
{
    public class Feature3 : Feature
    {
        public Feature3()
        {
        }

        public float compute(int[,] board)
        {
            int max = 0;

            for (int x = 0; x < 4; x++)
            {
                for (int y = 0; y < 4; y++)
                {
                    if (board[x, y] != 0)
                    {
                        int value = board[x, y];
                        if (value > max)
                            max = value;
                    }
                }
            }

            return (float)(Math.Log((float) max) / Math.Log(2));
        }
    }
}
