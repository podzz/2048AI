using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2048
{
    public enum Move_Key
    {
        UP,
        DOWN,
        LEFT,
        RIGHT,
        SPACE
    };

    public class Minimax
    {
        List<Move_Key> allowed_moves;
        List<int[,]> up_moves;
        List<int[,]> down_moves;
        List<int[,]> left_moves;
        List<int[,]> right_moves;
        private int[,] board_;
        FitnessEvaluator fit_eval;

        public Minimax(int[,] board)
        {
            board_ = copy_board(board);

            allowed_moves = new List<Move_Key>();
            up_moves = new List<int[,]>();
            down_moves = new List<int[,]>();
            left_moves = new List<int[,]>();
            right_moves = new List<int[,]>();

            fit_eval = new FitnessEvaluator();
        }

        public Move_Key get_best_move()
        {
            float temp = 0.0f;
            float up = 0.0f;
            float down = 0.0f;
            float left = 0.0f;
            float right = 0.0f;
            float max = 0.0f;
            Move_Key move;

            get_new_boards();

            foreach (int[,] board in down_moves)
            {
                temp = (new Minimax(board)).get_best_move_rec(0);

                if (temp > down)
                    down = temp;
            }
            
            foreach (int[,] board in up_moves)
            {
                temp = (new Minimax(board)).get_best_move_rec(0);

                if (temp > up)
                    up = temp;
            }

            foreach (int[,] board in left_moves)
            {
                temp = (new Minimax(board)).get_best_move_rec(0);

                if (temp > left)
                    left = temp;
            }
            
            foreach (int[,] board in right_moves)
            {
                temp = (new Minimax(board)).get_best_move_rec(0);

                if (temp > right)
                    right = temp;
            }

            max = up;
            move = Move_Key.UP;

            if (down > max)
            {
                max = down;
                move = Move_Key.DOWN;
            }
            if (left > max)
            {
                max = left;
                move = Move_Key.LEFT;
            }
            if (right > max)
            {
                move = Move_Key.RIGHT;
            }


            if (allowed_moves.Contains(move))
                return move;
            else if (allowed_moves.Count > 0)
                return allowed_moves[0];
            else
            {
                UtilityUI.print_error();
                return move;
            }

        }

        //gets the best float
        private float get_best_move_rec(int n)
        {
            if (n >= 2)
            {
                return fit_eval.compute(board_);
            }
            else
            {
                float res = 0.0f;
                float temp = 0.0f;

                get_new_boards();

                foreach (int[,] board in up_moves)
                {
                    temp = (new Minimax(board)).get_best_move_rec(n + 1);

                    if (temp > res)
                        res = temp;
                }

                foreach (int[,] board in down_moves)
                {
                    temp = (new Minimax(board)).get_best_move_rec(n + 1);

                    if (temp > res)
                        res = temp;
                }

                foreach (int[,] board in left_moves)
                {
                    temp = (new Minimax(board)).get_best_move_rec(n + 1);

                    if (temp > res)
                        res = temp;
                }

                foreach (int[,] board in right_moves)
                {
                    temp = (new Minimax(board)).get_best_move_rec(n + 1);

                    if (temp > res)
                        res = temp;
                }

                return res;
            }
        }

        private void allowed_move_keys()
        {
            bool up = false;
            bool down = false;
            bool left = false;
            bool right = false;

            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    if (!up && j > 0 && ((board_[i, j - 1] == 0 || board_[i, j - 1] == board_[i, j]) && board_[i, j] != 0))
                        up = true;
                    if (!down && j < 3 && ((board_[i, j + 1] == 0 || board_[i, j + 1] == board_[i, j]) && board_[i, j] != 0))
                        down = true;
                    if (!left && i > 0)
                    {
                        if ((board_[i - 1, j] == 0 || board_[i - 1, j] == board_[i, j]) && board_[i, j] != 0)
                            left = true;
                    }
                    if (!right && i < 3)
                    {
                        if ((board_[i + 1, j] == 0 || board_[i + 1, j] == board_[i, j]) && board_[i, j] != 0)
                            right = true;
                    }
                }
            }

            if (up)
                allowed_moves.Add(Move_Key.UP);
            if (down)
                allowed_moves.Add(Move_Key.DOWN);
            if (left)
                allowed_moves.Add(Move_Key.LEFT);
            if (right)
                allowed_moves.Add(Move_Key.RIGHT);
        }


        private int[,] copy_board(int[,] board)
        {
            int[,] new_board = new int[4,4];


            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    new_board[i, j] = board[i, j];
                }
            }

            return new_board;
        }

        private void move_up(int[,] board, int[,] moved, int i, int j)
        {
            while (j > 0)
            {
                //merge
                if (moved[i, j - 1] == 0 && board[i, j - 1] == board[i, j])
                {
                    board[i, j - 1] = board[i, j] * 2;
                    board[i, j] = 0;
                    moved[i, j - 1] = 1;
                    return;
                }
                //deplacement
                else if (board[i, j - 1] == 0)
                {
                    board[i, j - 1] = board[i, j];
                    board[i, j] = 0;
                }
                else
                {
                    return;
                }

                j--;
            }
        }

        private void move_down(int[,] board, int[,] moved, int i, int j)
        {
            while (j < 3)
            {
                //merge
                if (moved[i, j + 1] == 0 && board[i, j + 1] == board[i, j])
                {
                    board[i, j + 1] = board[i, j] * 2;
                    board[i, j] = 0;
                    moved[i, j + 1] = 1;
                    return;
                }
                //deplacement
                else if (board[i, j + 1] == 0)
                {
                    board[i, j + 1] = board[i, j];
                    board[i, j] = 0;
                }
                else
                {
                    return;
                }

                j++;
            }
        }

        private void move_left(int[,] board, int[,] moved, int i, int j)
        {
            while (i > 0)
            {
                //merge
                if (moved[i - 1, j] == 0 && board[i - 1, j] == board[i, j])
                {
                    board[i - 1, j] = board[i, j] * 2;
                    board[i, j] = 0;
                    moved[i - 1, j] = 1;
                    return;
                }
                //deplacement
                else if (board[i - 1, j] == 0)
                {
                    board[i - 1, j] = board[i, j];
                    board[i, j] = 0;
                }
                else
                {
                    return;
                }

                i--;
            }
        }

        private void move_right(int[,] board, int[,] moved, int i, int j)
        {
            while (i < 3)
            {
                //merge
                if (moved[i + 1, j] == 0 && board[i + 1, j] == board[i, j])
                {
                    board[i + 1, j] = board[i, j] * 2;
                    board[i, j] = 0;
                    moved[i + 1, j] = 1;
                    return;
                }
                //deplacement
                else if (board[i + 1, j] == 0)
                {
                    board[i + 1, j] = board[i, j];
                    board[i, j] = 0;
                }
                else
                {
                    return;
                }

                i++;
            }
        }

        private List<int[,]> merge_up()
        {
            int[,] new_board = copy_board(this.board_);
            int[,] moved = new int[4, 4] { { 0, 0, 0, 0 }, { 0, 0, 0, 0 }, { 0, 0, 0, 0 }, { 0, 0, 0, 0 } };
            List<int[,]> list = new List<int[,]>();

            //simulates up move
            for (int i = 0; i < 4; i++)
            {
                for (int j = 3; j >= 0; j--)
                {
                    if (new_board[i, j] != 0 && moved[i, j] == 0)
                        move_up(new_board, moved, i, j);
                }
            }

            moved = new int[4, 4] { { 1, 1, 1, 1 }, { 1, 1, 1, 1 }, { 1, 1, 1, 1 }, { 1, 1, 1, 1 } };

            for (int j = 0; j < 4; j++)
            {
                for (int i = 3; i >= 0; i--)
                {
                    move_up(new_board, moved, i, j);
                }
            }

            for (int val = 2; val <= 4; val += 2)
            {
                for (int i = 0; i < 4; i++)
                {
                    for (int j = 0; j < 4; j++)
                    {
                        if (new_board[i, j] == 0)
                        {
                            new_board[i, j] = val;
                            list.Add(copy_board(new_board));
                            new_board[i, j] = 0;
                        }
                    }
                }
            }

            return list;
        }

        private List<int[,]> merge_down()
        {
            int[,] new_board = copy_board(this.board_);
            int[,] moved = new int[4, 4] { { 0, 0, 0, 0 }, { 0, 0, 0, 0 }, { 0, 0, 0, 0 }, { 0, 0, 0, 0 } };
            List<int[,]> list = new List<int[,]>();

            //simulates up move
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (new_board[i, j] != 0 && moved[i, j] == 0)
                        move_down(new_board, moved, i, j);
                }
            }

            moved = new int[4, 4] { { 1, 1, 1, 1 }, { 1, 1, 1, 1 }, { 1, 1, 1, 1 }, { 1, 1, 1, 1 } };

            for (int j = 0; j < 4; j++)
            {
                for (int i = 3; i >= 0; i--)
                {
                    move_down(new_board, moved, i, j);
                }
            }

            for (int val = 2; val <= 4; val += 2)
            {
                for (int i = 0; i < 4; i++)
                {
                    for (int j = 0; j < 4; j++)
                    {
                        if (new_board[i, j] == 0)
                        {
                            new_board[i, j] = val;
                            list.Add(copy_board(new_board));
                            new_board[i, j] = 0;
                        }
                    }
                }
            }

            return list;
        }

        private List<int[,]> merge_right()
        {
            int[,] new_board = copy_board(this.board_);
            int[,] moved = new int[4, 4] { { 0, 0, 0, 0 }, { 0, 0, 0, 0 }, { 0, 0, 0, 0 }, { 0, 0, 0, 0 } };
            List<int[,]> list = new List<int[,]>();

            //simulates up move
            for (int j = 0; j < 4; j++)
            {
                for (int i = 0; i < 4; i++)
                {
                    if (new_board[i, j] != 0 && moved[i, j] == 0)
                        move_right(new_board, moved, i, j);
                }
            }

            moved = new int[4, 4] { { 1, 1, 1, 1 }, { 1, 1, 1, 1 }, { 1, 1, 1, 1 }, { 1, 1, 1, 1 } };

            for (int j = 0; j < 4; j++)
            {
                for (int i = 3; i >= 0; i--)
                {
                    move_right(new_board, moved, i, j);
                }
            }

            for (int val = 2; val <= 4; val += 2)
            {
                for (int i = 0; i < 4; i++)
                {
                    for (int j = 0; j < 4; j++)
                    {
                        if (new_board[i, j] == 0)
                        {
                            new_board[i, j] = val;
                            list.Add(copy_board(new_board));
                            new_board[i, j] = 0;
                        }
                    }
                }
            }

            return list;
        }

        private List<int[,]> merge_left()
        {
            int[,] new_board = copy_board(this.board_);
            int[,] moved = new int[4, 4] { { 0, 0, 0, 0 }, { 0, 0, 0, 0 }, { 0, 0, 0, 0 }, { 0, 0, 0, 0 } };
            List<int[,]> list = new List<int[,]>();

            //simulates up move
            for (int j = 0; j < 4; j++)
            {
                for (int i = 3; i >= 0; i--)
                {
                    if (new_board[i, j] != 0 && moved[i, j] == 0)
                        move_left(new_board, moved, i, j);
                }
            }

            moved = new int[4, 4] { { 1, 1, 1, 1 }, { 1, 1, 1, 1 }, { 1, 1, 1, 1 }, { 1, 1, 1, 1 } };

            for (int j = 0; j < 4; j++)
            {
                for (int i = 3; i >= 0; i--)
                {
                    move_left(new_board, moved, i, j);
                }
            }

            for (int val = 2; val <= 4; val += 2)
            {
                for (int i = 0; i < 4; i++)
                {
                    for (int j = 0; j < 4; j++)
                    {
                        if (new_board[i, j] == 0)
                        {
                            new_board[i, j] = val;
                            list.Add(copy_board(new_board));
                            new_board[i, j] = 0;
                        }
                    }
                }
            }

            return list;
        }

        private List<int[,]> simulate_move_right()
        {
            int[,] new_board = copy_board(board_);
            int[,] moved = new int[4, 4] { {0, 0, 0, 0}, {0, 0, 0, 0}, {0, 0, 0, 0}, {0, 0, 0, 0} };
            List<int[,]> list = new List<int[,]>();

            //simulates up move
            for (int j = 0; j < 4; j++)
            {
                for (int i = 0; i < 4; i++)
                {
                    if (new_board[i, j] != 0)
                        move_right(new_board, moved, i, j);
                }
            }

            //insert 2 and 4
            for (int val = 2; val <= 4; val += 2)
            {
                for (int i = 0; i < 4; i++)
                {
                    for (int j = 0; j < 4; j++)
                    {
                        if (new_board[i, j] == 0)
                        {
                            new_board[i, j] = val;
                            list.Add(copy_board(new_board));
                            new_board[i, j] = 0;
                        }
                    }
                }
            }

            return list;
        }


        private void get_new_boards()
        {
            allowed_move_keys();
            foreach (Move_Key move in allowed_moves)
            {
                if (move == Move_Key.UP)
                    up_moves = merge_up();
                else if (move == Move_Key.DOWN)
                    down_moves = merge_down();
                else if (move == Move_Key.LEFT)
                    left_moves = merge_left();
                else if (move == Move_Key.RIGHT)
                    right_moves = merge_right();
            }
        }
    }
}
