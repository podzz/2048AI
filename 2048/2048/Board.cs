using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _2048
{

    public class Board
    {
        private int[,] board_;
        public Board()
        {
            this.board_ = new int[4, 4];
            for (int i = 0; i < 4; i++)
                for (int j = 0; j < 4; j++)
                    this.board_[i, j] = 0;
            this.pop_random();
            this.pop_random();

        }

        List<Tuple<int, int>> get_empty_cells()
        {
            List<Tuple<int, int>> empty_cells_list = new List<Tuple<int, int>>();
            for (int i = 0; i < 4; i++)
                for (int j = 0; j < 4; j++)
                    if (this.board_[i, j] == 0)
                        empty_cells_list.Add(new Tuple<int, int>(i, j));
            return empty_cells_list;
        }

        bool pop_random()
        {
            Random rand = new Random();
            List<Tuple<int, int>> empty_list = this.get_empty_cells();
            if (empty_list.Count() == 0)
                return false;
            int pop_index = rand.Next(empty_list.Count()) % empty_list.Count();
            this.board_[empty_list[pop_index].Item1, empty_list[pop_index].Item2] = 2;
            return true;
        }

        private void merge_left()
        {
            int[,] new_board = this.board_;
            int[,] moved = new int[4, 4] { { 0, 0, 0, 0 }, { 0, 0, 0, 0 }, { 0, 0, 0, 0 }, { 0, 0, 0, 0 } };
            List<int[,]> list = new List<int[,]>();

            //simulates up move
            for (int j = 0; j < 4; j++)
            {
                for (int i = 3; i >= 0; i--)
                {
                    if (new_board[i, j] != 0)
                        move_left(new_board, moved, i, j);
                }
            }
            this.board_ = new_board;
        }

        void merge_right()
        {
            int[,] new_board = this.board_;
            int[,] moved = new int[4, 4] { { 0, 0, 0, 0 }, { 0, 0, 0, 0 }, { 0, 0, 0, 0 }, { 0, 0, 0, 0 } };
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
            this.board_ = new_board;
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
                    break;
                }
                //deplacement
                else if (board[i, j - 1] == 0)
                {
                    board[i, j - 1] = board[i, j];
                    board[i, j] = 0;
                }
                else
                {
                    break;
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
                    break;
                }
                //deplacement
                else if (board[i, j + 1] == 0)
                {
                    board[i, j + 1] = board[i, j];
                    board[i, j] = 0;
                }
                else
                {
                    break;
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
                    break;
                }
                //deplacement
                else if (board[i - 1, j] == 0)
                {
                    board[i - 1, j] = board[i, j];
                    board[i, j] = 0;
                }
                else
                {
                    break;
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
                    break;
                }
                //deplacement
                else if (board[i + 1, j] == 0)
                {
                    board[i + 1, j] = board[i, j];
                    board[i, j] = 0;
                }
                else
                {
                    break;
                }

                i++;
            }
        }

        void merge_up()
        {
            int[,] new_board = this.board_;
            int[,] moved = new int[4, 4] { { 0, 0, 0, 0 }, { 0, 0, 0, 0 }, { 0, 0, 0, 0 }, { 0, 0, 0, 0 } };
            List<int[,]> list = new List<int[,]>();

            //simulates up move
            for (int i = 0; i < 4; i++)
            {
                for (int j = 3; j >= 0; j--)
                {
                    if (new_board[i, j] != 0)
                        move_up(new_board, moved, i, j);
                }
            }
            this.board_ = new_board;
        }


        void merge_down()
        {
            int[,] new_board = this.board_;
            int[,] moved = new int[4, 4] { { 0, 0, 0, 0 }, { 0, 0, 0, 0 }, { 0, 0, 0, 0 }, { 0, 0, 0, 0 } };
            List<int[,]> list = new List<int[,]>();

            //simulates up move
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (new_board[i, j] != 0)
                        move_down(new_board, moved, i, j);
                }
            }
            this.board_ = new_board;
        }

        public void display_board()
        {
            for (int j = 0; j < 4; j++)
            {
                for (int i = 0; i < 4; i++)
                {
                    Console.Write(this.board_[i, j]);
                    if (i != 3)
                        Console.Write(" | ");
                }
                Console.WriteLine();
            }
        }

        public void move(Keys move)
        {
            int[,] cpy = new int[4, 4];
            for (int i = 0; i < 4; i++)
                for (int j = 0; j < 4; j++)
                    cpy[i, j] = this.board_[i, j];
            if (move == Keys.Up)
                merge_up();
            else if (move == Keys.Down)
                merge_down();
            else if (move == Keys.Right)
                merge_right();
            else if (move == Keys.Left)
                merge_left();
            if (difference(cpy, this.board_))
                this.pop_random();

        }

        private bool difference(int[,] arr1, int[,] arr2)
        {
            for (int i = 0; i < 4; i++)
                for (int j = 0; j < 4; j++)
                    if (arr1[i, j] != arr2[i, j])
                        return true;
            return false;
        }

        private int[,] conversion(int[,] arr)
        {
            int[,] arrcpy = new int[4,4];
            for (int i = 0; i < 4; i++)
                for (int j = 0; j < 4; j++)
                    arrcpy[i, j] = arr[j, i];
            return arrcpy;
        }

        public int[,] get_array()
        {
            return this.board_;
        }

    }
}
