using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2048
{
    class Feature5 : Feature
    {
        public Feature5()
        {
        }

        private bool withinBounds(Tuple<int, int> position)
        {
            return position.Item1 >= 0 && position.Item1 < 4 && position.Item2 >= 0 && position.Item2 < 4;
        }

        private Tuple<Tuple<int, int>, Tuple<int, int>> findFarthestPosition(Tuple<int, int> cell, Tuple<int, int> vector, int[,] board)
        {
            Tuple<int, int> previous;

            do
            {
                previous = new Tuple<int, int>(cell.Item1, cell.Item2);
                cell = new Tuple<int, int>(previous.Item1 + vector.Item1, previous.Item2 + vector.Item2);

            } while (withinBounds(cell) && board[cell.Item1, cell.Item2] == 0);

            return new Tuple<Tuple<int, int>, Tuple<int, int>>(previous, cell);
        }

        private Tuple<int, int> vectors(int n)
        {
            if (n == 0)
                return new Tuple<int, int>(0, -1);
            else if (n == 1)
                return new Tuple<int, int>(1, 0);
            else if (n == 2)
                return new Tuple<int, int>(0, 1);
            else
                return new Tuple<int, int>(-1, 0);
        }

        public float compute(int[,] board)
        {
            double smoothness = 0.0;
            double value = 0.0;

            for (int x = 0; x < 4; x++)
            {
                for (int y = 0; y < 4; y++)
                {
                    if (board[x, y] != 0)
                    {
                        value = Math.Log(board[x, y]) / Math.Log(2);

                        for (int direction = 1; direction <= 2; direction++)
                        {
                            Tuple<int, int> vector = vectors(direction);
                            Tuple<int, int> targetCell =
                                findFarthestPosition(new Tuple<int, int>(x, y), vector, board).Item2;

                            if (withinBounds(targetCell) && board[targetCell.Item1, targetCell.Item2] != 0)
                            {
                                int target = board[targetCell.Item1, targetCell.Item2];
                                double targetValue = Math.Log(target) / Math.Log(2);
                                smoothness -= Math.Abs(value - targetValue);
                            }
                        }
                    }
                }
            }

            return (float) smoothness;
        }
    }
}
