using System;
using System.Collections.Generic;
using System.Drawing;   //Позволяет использовать класс Color
using System.Windows.Forms; //Позволяет использовать нашу форму

namespace LifeGame
{
    class Life
    {
        public int[,] Area { get; set; }    //В любой момент можем получить поле и изменить его
        public int N { get; set; }  //Размер поля

        public void GridToArea(DataGridView grid, int n)    //Из таблицы в массив
        //Чтобы передать в параметр метода таблицу, нужно подключить нашу форму!
        {
            N = n;
            Area = new int[n, n];   //Инициализируем массив на n строк и n столбиков

            for(int col=0;col<n;col++)
            {
                for(int row=0;row<n;row++)
                {//!!!grid работает сначала со столбиками, потом со строками!!!
                    if (grid[col, row].Style.BackColor == Color.Black)
                 //!!!Массив работает сначала со строками, затем со столбиками!!!
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
                    //Считаем "соседей" вокруг чёрной клетки:
                    if(row < N-1)              count += Area[col, row+1];
                    if(row > 0)                count += Area[col, row-1];
                    
                    if(row < N-1 && col < N-1) count += Area[col+1, row+1];
                    if(row > 0 && col < N-1)   count += Area[col+1, row-1];
                    if(col < N-1)              count += Area[col+1, row];
                    
                    if(row < N - 1 && col >0)  count += Area[col-1, row+1];
                    if(row > 0 && col > 0)     count += Area[col-1, row-1];
                    if(col > 0)                count += Area[col-1, row];

                    if(Area[col,row] == 0 && count == 3)    //Если ячейка мертвая и рядом 3 соседа
                    {
                        buffer[col, row] = 1;   //Завтра ячейка оживает
                    }
                    else if(Area[col,row] == 1 && (count == 2 || count == 3))   //Если ячейка живая и рядом есть 2 или 3 соседа 
                    {
                        buffer[col, row] = 1;   //Завтра ячейка будет жива
                    }
                    else
                    {
                        buffer[col, row] = 0;   //Иначе умрёт
                    }
                }
            }
            Area = buffer;  //Данные завтрашнего дня присваиваем в текущий
        }
    }
}
