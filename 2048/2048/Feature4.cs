using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2048
{
    public class Feature4 : Feature
    {
        public Feature4()
        {

        }
        public float compute(int[,] board)
        {
            // scores for all four directions
            double[] totals = new double[4];
            for (int i = 0; i < 4; i++)
                totals[i] = 0;
            for (int x = 0; x < 4; x++)
            {
                int current = 0;
                int next = current + 1;
                while (next < 4)
                {
                    while (next < 4 && !(board[x, next] != 0))
                        next++;
                    if (next >= 4)
                        next--;
                    double currentValue = (board[x, current] != 0) ? Math.Log(board[x, current]) / Math.Log(2) : 0;
                    double nextValue = (board[x, next] != 0) ? Math.Log(board[x, next]) / Math.Log(2) : 0;
                    if (currentValue > nextValue)
                        totals[0] += nextValue - currentValue;
                    else if (nextValue > currentValue)
                        totals[1] += currentValue - nextValue;
                    current = next;
                    next++;
                }
            }

            for (int y = 0; y < 4; y++)
            {
                int current = 0;
                int next = current + 1;
                while (next < 4)
                {
                    while (next < 4 && !(board[next,y] != 0))
                        next++;
                    if (next >= 4)
                        next--;
                    double currentValue = (board[current, y] != 0) ? Math.Log(board[current, y]) / Math.Log(2) : 0;
                    double nextValue = (board[next, y] != 0) ? Math.Log(board[next, y]) / Math.Log(2) : 0;
                    if (currentValue > nextValue)
                        totals[2] += nextValue - currentValue;
                    else if (nextValue > currentValue)
                        totals[3] += currentValue - nextValue;
                    current = next;
                    next++;
                }
            }
            return (float)(Math.Max(totals[0], totals[1])) + (float)(Math.Max(totals[2], totals[3]));
        }
    }
}
