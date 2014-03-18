using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2048
{
    public enum Move
    {
        UP,
        DOWN,
        LEFT,
        RIGHT
    };

    public class Minimax
    {
        List<int[][]> up_moves;
        List<int[][]> down_moves;
        List<int[][]> left_moves;
        List<int[][]> right_moves;
        private int[][] board_;

        public Minimax(int[][] board)
        {
            board_ = board;

            up_moves = new List<int[][]>();
            down_moves = new List<int[][]>();
            left_moves = new List<int[][]>();
            right_moves = new List<int[][]>();
        }

        private bool is_up_allowed()
        {
            int value = 0;
            int k = 0;

            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    if (board_[i][j] != 0)
                    {
                        value = board_[i][j];
                        k = 1;

                        while (board_[i][j - k] == 0 && k <= j)
                        {
                            k++;
                        }

                        if (board_[i][j - k] == value)
                            return true;
                    }
                }
            }

            return false;
        }

        private bool is_down_allowed()
        {
            int value = 0;
            int k = 0;

            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    if (board_[i][j] != 0)
                    {
                        value = board_[i][j];
                        k = 1;

                        while (board_[i][j + k] == 0 && j + k < 4)
                        {
                            k++;
                        }

                        if (board_[i][j + k] == value)
                            return true;
                    }
                }
            }

            return false;
        }

        private bool is_left_allowed()
        {
            int value = 0;
            int k = 0;

            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    if (board_[i][j] != 0)
                    {
                        value = board_[i][j];
                        k = 1;

                        while (board_[i - k][j] == 0 && k <= i)
                        {
                            k++;
                        }

                        if (board_[i - k][j] == value)
                            return true;
                    }
                }
            }

            return false;
        }

        private bool is_right_allowed()
        {
            int value = 0;
            int k = 0;

            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    if (board_[i][j] != 0)
                    {
                        value = board_[i][j];
                        k = 1;

                        while (board_[i + k][j] == 0 && k + i < 4)
                        {
                            k++;
                        }

                        if (board_[i + k][j] == value)
                            return true;
                    }
                }
            }

            return false;
        }

        private List<Move> get_allowed_moves()
        {
            List<Move> list = new List<Move>();

            if (is_up_allowed())
                list.Add(Move.UP);
            if (is_down_allowed())
                list.Add(Move.DOWN);
            if (is_left_allowed())
                list.Add(Move.LEFT);
            if (is_right_allowed())
                list.Add(Move.RIGHT);

            return list;
        }

        private int[][] copy_board(int[][] board)
        {
            int[][] new_board = new int[4][];

            for (int i = 0; i < 4; i++)
            {
                new_board[i] = new int[4];
            }

                for (int i = 0; i < 4; i++)
                {
                    for (int j = 0; j < 4; j++)
                    {
                        new_board[i][j] = board[i][j];
                    }
                }

            return new_board;
        }

        private List<int[][]> simulate_move_up()
        {
            int value = 0;
            int k = 0;
            List<int[][]> list = new List<int[][]>();

            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    if (board_[i][j] != 0)
                    {
                        value = board_[i][j];
                        k = 1;

                        while (board_[i][j - k] == 0 && k <= j)
                        {
                            k++;
                        }

                        if (board_[i][j - k] == value)
                        {
                            board_[i][j] = 0;
                            board_[i][j - k] = value * value;
                        }
                    }
                }
            }

            for (int val = 2; val <= 4; val += 2)
            {
                for (int i = 0; i < 4; i++)
                {
                    for (int j = 0; j < 4; j++)
                    {
                        if (board_[i][j] == 0)
                        {
                            board_[i][j] = val;
                            list.Add(copy_board(board_));
                            board_[i][j] = 0;
                        }
                    }
                }
            }

            return list;
        }

        private List<int[][]> simulate_move_down()
        {
            int value = 0;
            int k = 0;
            List<int[][]> list = new List<int[][]>();

            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    if (board_[i][j] != 0)
                    {
                        value = board_[i][j];
                        k = 1;

                        while (board_[i][j + k] == 0 && j + k < 4)
                        {
                            k++;
                        }

                        if (board_[i][j + k] == value)
                        {
                            board_[i][j] = 0;
                            board_[i][j + k] = value * value;
                        }
                    }
                }
            }

            for (int val = 2; val <= 4; val += 2)
            {
                for (int i = 0; i < 4; i++)
                {
                    for (int j = 0; j < 4; j++)
                    {
                        if (board_[i][j] == 0)
                        {
                            board_[i][j] = val;
                            list.Add(copy_board(board_));
                            board_[i][j] = 0;
                        }
                    }
                }
            }

            return list;
        }

        private List<int[][]> simulate_move_left()
        {
            int value = 0;
            int k = 0;
            List<int[][]> list = new List<int[][]>();

            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    if (board_[i][j] != 0)
                    {
                        value = board_[i][j];
                        k = 1;

                        while (board_[i - k][j] == 0 && k <= i)
                        {
                            k++;
                        }

                        if (board_[i - k][j] == value)
                        {
                            board_[i][j] = 0;
                            board_[i - k][j] = value * value;
                        }
                    }
                }
            }

            for (int val = 2; val <= 4; val += 2)
            {
                for (int i = 0; i < 4; i++)
                {
                    for (int j = 0; j < 4; j++)
                    {
                        if (board_[i][j] == 0)
                        {
                            board_[i][j] = val;
                            list.Add(copy_board(board_));
                            board_[i][j] = 0;
                        }
                    }
                }
            }

            return list;
        }

        private List<int[][]> simulate_move_right()
        {
            int value = 0;
            int k = 0;
            List<int[][]> list = new List<int[][]>();

            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    if (board_[i][j] != 0)
                    {
                        value = board_[i][j];
                        k = 1;

                        while (board_[i + k][j] == 0 && i + k < 4)
                        {
                            k++;
                        }

                        if (board_[i + k][j] == value)
                        {
                            board_[i][j] = 0;
                            board_[i + k][j] = value * value;
                        }
                    }
                }
            }

            for (int val = 2; val <= 4; val += 2)
            {
                for (int i = 0; i < 4; i++)
                {
                    for (int j = 0; j < 4; j++)
                    {
                        if (board_[i][j] == 0)
                        {
                            board_[i][j] = val;
                            list.Add(copy_board(board_));
                            board_[i][j] = 0;
                        }
                    }
                }
            }

            return list;
        }

        private void get_new_boards()
        {
            if (is_up_allowed())
                up_moves = simulate_move_up();
            if (is_down_allowed())
                down_moves = simulate_move_down();
            if (is_left_allowed())
                left_moves = simulate_move_left();
            if (is_right_allowed())
                right_moves = simulate_move_right();
        }
    }
}
