using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms; //Позволяет использовать нашу форму

namespace LifeGame
{
    class Life
    {

        public int[,] Area { get; set; }
        public int N { get; set; }

        public void GridToArea(DataGridView grid, int n)    //Из таблицы в массив
        {
            N = n;
            Area = new int[n, n];

            for(int col=0;col<n;col++)
            {
                for(int row=0;row<n;row++)
                {
                    if (grid[col, row].Style.BackColor == Color.Black)
                        Area[row, col] = 1; //Если ячейка чёрная, то ставим 1
                    else
                        Area[row, col] = 0; //Если ячейка белая, то 0
                }
            }
        }

        public void AreaToGrid(DataGridView grid)   //Из массива в таблицу
        {
            for (int col = 0; col < N; col++)
            {
                for (int row = 0; row < N; row++)
                {
                    if (Area[row,col] == 1)
                        grid[col, row].Style.BackColor = Color.Black;
                    else
                        grid[col, row].Style.BackColor = Color.White;
                }
            }
        }

        public void NextDay()
        {
            int[,] buffer = new int[N, N];   //Завтра
            int count;  //Соседи
            for (int col = 0; col < N; col++)
            { 
                for (int row = 0; row < N; row++)
                {
                    count = 0;
                    if(row < N-1)              count += Area[col, row+1];
                    if(row > 0)                count += Area[col, row-1];
                    
                    if(row < N-1 && col < N-1) count += Area[col+1, row+1];
                    if(row > 0 && col < N-1)   count += Area[col+1, row-1];
                    if(col < N-1)              count += Area[col+1, row];
                    
                    if(row < N - 1 && col >0)  count += Area[col-1, row+1];
                    if(row > 0 && col > 0)     count += Area[col-1, row-1];
                    if(col > 0)                count += Area[col-1, row];
                    if(Area[col,row] == 0 && count == 3)
                    {
                        buffer[col, row] = 1;
                    }
                    else if(Area[col,row] == 1 && (count == 2 || count == 3))
                    {
                        buffer[col, row] = 1;
                    }
                    else
                    {
                        buffer[col, row] = 0;
                    }
                }
            }
            Area = buffer;  //Данные завтрашнего дня присваиваем в текущий
        }
    }
}
