using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2048
{
    public class ImageProcess
    {

        Bitmap bmp_display;
        List<Tuple<int,int,int>> cell_img;
        Color color_board = Color.FromArgb(187, 173, 160);
        Rectangle crop;
        Point top_left;
        int border_size;
        int size_cell;
        
        public ImageProcess(Bitmap bmp, bool withoutInterface)
        {
            if (withoutInterface)
            {
                crop = new Rectangle(0, 0, Properties.Resources.log.Width, Properties.Resources.log.Height);
                border_size = 16;
                size_cell = 106;
                top_left = new Point(15, 10);
                this.bmp_display = bmp;
            }
            else
            {
                crop = Rectangle.Empty;
                this.bmp_display = null;
                border_size = 0;
                size_cell = 0;
                process_bound_board(bmp);
                process_bound_board_extend(bmp);
            }
        }

        private void process_bound_board(Bitmap bmp)
        {
            Point top_left = Point.Empty;
            Point bottom_left = Point.Empty;
            Point top_right = Point.Empty;
            bool found_top_left = false;
            bool found_bottom_left = false;
            int i = 0;
            int j = 0;
            // FIND FIRST COIN TOP LEFT
            while (i < bmp.Width && !found_bottom_left)
            {
                while (j < bmp.Height && !found_bottom_left)
                {
                    if (bmp.GetPixel(i, j) == this.color_board && !found_top_left)
                    {
                        // FIND THE TOP LEFT OF THE BOARD
                        top_left = new Point(i, j);
                        found_top_left = true;
                    }
                    else if (bmp.GetPixel(i,j) != this.color_board && found_top_left)
                    {
                        // FIND THE RIGHT OF THE BOARD
                        bottom_left = new Point(i, j - 1);
                        found_bottom_left = true;
                    }
                    j++;
                }
                j = 0;
                i++;
            }
            // FIND COIN BOTTOM LEFT
            i = top_left.X;
            j = top_left.Y;
            while (i <= bmp.Width)
            {
                if (bmp.GetPixel(i, j) != this.color_board)
                {
                    top_right = new Point(i, j);
                    break;
                }
                i++;
            }

            this.crop = new Rectangle(top_left.X, top_left.Y, top_right.X - top_left.X, bottom_left.Y - top_left.Y);
        }

        private void process_bound_board_extend(Bitmap bmp)
        {
            this.bmp_display = bmp.Clone(crop, bmp.PixelFormat);
            int i = 0;
            int j = 0;
            Point top_right = Point.Empty;

            bool top_left_found = false;
            bool top_right_found = false;
            bool border_size_found = false;

            while (j < this.bmp_display.Height && !border_size_found)
            {
                while (i < this.bmp_display.Width && !border_size_found)
                {
                    if (bmp_display.GetPixel(i, j) != this.color_board && !top_left_found)
                    {
                        top_left = new Point(i, j);
                        top_left_found = true;
                    }
                    else if (bmp_display.GetPixel(i, j) == this.color_board && top_left_found && !top_right_found)
                    {
                        top_right = new Point(i - 1, j);
                        size_cell = top_right.X - top_left.X;
                        top_right_found = true;
                    }
                    else if (bmp_display.GetPixel(i, j) != this.color_board && top_right_found)
                    {
                        border_size = i - top_right.X;
                        border_size_found = true;
                    }
                    i++;
                }
                i = 0;
                j++;
            }
        }

        public void process_cell(Bitmap bmp_arg)
        {
            this.cell_img = new List<Tuple<int, int, int>>();
            this.bmp_display = bmp_arg.Clone(crop, bmp_arg.PixelFormat);
            

            int count_i = 0;
            int count_j = 0;
            for (int i = top_left.X; i < bmp_display.Width && i + size_cell < bmp_display.Width; i += (border_size + size_cell))
            {
                for (int j = top_left.Y; j < bmp_display.Height && j + size_cell < bmp_display.Height; j += (border_size + size_cell))
                {
                    this.cell_img.Add(new Tuple<int,int,int>(count_i, count_j, this.get_value_by_color(this.bmp_display.GetPixel(i + 20, j + 20))));
                    count_j++;
                }
                count_j = 0;
                count_i++;
            }
        }

        private int get_value_by_color(Color pix)
        {
            if (pix.R == 204 && pix.G == 192 && pix.B == 179)
                return 0;
            else if (pix.R == 238 && pix.G == 228 && pix.B == 218)
                return 2;
            else if (pix.R == 237 && pix.G == 224 && pix.B == 200)
                return 4;
            else if (pix.R == 242 && pix.G == 177 && pix.B == 121)
                return 8;
            else if (pix.R == 245 && pix.G == 149 && pix.B == 99)
                return 16;
            else if (pix.R == 246 && pix.G == 124 && pix.B == 95)
                return 32;
            else if (pix.R == 246 && pix.G == 94 && pix.B == 59)
                return 64;
            else if (pix.R == 237 && pix.G == 207 && pix.B == 114)
                return 128;
            else if (pix.R == 237 && pix.G == 204 && pix.B == 97)
                return 256;
            else if (pix.R == 237 && pix.G == 200 && pix.B == 80)
                return 512;
            else if (pix.R == 237 && pix.G == 197 && pix.B == 63)
                return 1024;
            else
                return 0;
        }

        private Color get_color_by_value(int value)
        {
            switch (value)
            {
                case 0:
                    return Color.FromArgb(204, 192, 179);
                case 2:
                    return Color.FromArgb(238, 228, 218);
                case 4:
                    return Color.FromArgb(237, 224, 200);
                case 8:
                    return Color.FromArgb(242, 177, 121);
                case 16:
                    return Color.FromArgb(245, 149, 99);
                case 32:
                    return Color.FromArgb(246, 124, 95);
                case 64:
                    return Color.FromArgb(246, 94, 59);
                case 128:
                    return Color.FromArgb(237, 207, 114);
                case 256:
                    return Color.FromArgb(237, 204, 97);
                case 512:
                    return Color.FromArgb(237, 200, 80);
                case 1024:
                    return Color.FromArgb(237, 197, 63);
                default:
                    return Color.FromArgb(204, 192, 179);
            }
        }

        public Bitmap get_img()
        {
            return this.bmp_display;
        }

        public int[,] get_array()
        {
            int[,] new_array = new int[4, 4];
            foreach(Tuple<int, int, int> r in cell_img)
                new_array[r.Item1, r.Item2] = r.Item3;
            return new_array;
        }

        public void draw_board(int[,] array)
        {
            this.bmp_display = Properties.Resources.log;
            int padd = border_size + size_cell;
            Graphics g = Graphics.FromImage(this.bmp_display);
            g.DrawImage(this.bmp_display, new Point(0, 0));
            for (int i = 0; i < 4; i++)
                for (int j = 0; j < 4; j++)
                    g.FillRectangle(new SolidBrush(this.get_color_by_value(array[i,j])), new Rectangle(top_left.X + padd * i, top_left.Y + padd * j, size_cell, size_cell));
            for (int i = 0; i < 4; i++)
                for (int j = 0; j < 4; j++)
                    g.DrawString(array[i, j].ToString(), new Font(new FontFamily("Times New Roman"), 16.0f, FontStyle.Regular | FontStyle.Bold), Brushes.Black, new Point(top_left.X + padd * i + 40, top_left.Y + padd * j + 40));
            
        }

    }
}
