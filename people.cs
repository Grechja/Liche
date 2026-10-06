using System;
using System.Collections.Generic;
using System.Text;

namespace Life
{
    internal class people
    {
        int count = 0;
        char[,] dom;
        int n = 0;
        public people(int count, int n)
        {
            this.count = count;
            this.n = n;
            dom = new char[n, n];
            //Place(count);
        }
        public int Count { get { return count; } }
        public int N { get { return n; } }
        public char this[int i, int j] 
        { get { return dom[i, j]; }
            set { dom[i, j] = value; }
        }

            
        //void Place(int count) 
        //{

        //    Random ran = new Random();
        //    int x, y, cur_count = 0;
        //    if (count >= n * n)
        //    {
        //        for (int i = 0; i < n; i++)
        //            for (int j = 0; j < n; j++)
        //            {
        //                dom[i, j] = '1';
        //            }

        //    }
        //    else
        //        while (cur_count != count)
        //        {
        //            x = ran.Next(n);
        //            y = ran.Next(n);
        //            if (dom[x, y] != '1')
        //            {
        //                dom[x, y] = '1';
        //                cur_count++;
        //            }
        //        }
        //}
        public int Neighbours(int x, int y)
        {
            int nb = 0;
            if (dom[check(x - 1), check(y - 1)] == '1') nb++;
            if (dom[check(x - 1), check(y)] == '1') nb++;
            if (dom[check(x - 1), check(y + 1)] == '1') nb++;
            if (dom[check(x), check(y - 1)] == '1') nb++;
            if (dom[check(x), check(y + 1)] == '1') nb++;
            if (dom[check(x + 1), check(y - 1)] == '1') nb++;
            if (dom[check(x + 1), check(y)] == '1') nb++;
            if (dom[check(x + 1), check(y + 1)] == '1') nb++;

            return nb;
        }
        int check(int index)
        {
            if (index > n - 1) index = 0;
            if (index < 0) index = n - 1;
            return index;
        }

        public void draw(DataGridView dgv)
        {
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    if (dom[i, j] == '1') { dgv.Rows[i].Cells[j].Style.BackColor = Color.Purple; } // Заражённая
                    else if (dom[i, j] == '2') { dgv.Rows[i].Cells[j].Style.BackColor = Color.Yellow; } // Иммунитет
                    else { dgv.Rows[i].Cells[j].Style.BackColor = Color.PaleGreen; } // Здоровая
                }
            }
        }
    }
}
