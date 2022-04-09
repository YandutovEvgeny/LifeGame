using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HomeWork
{
    public partial class Form1 : Form
    {
        //TODO: Создать проект, добавить и настроить в неё dataGridView         //DONE
        //TODO: При создании таблицы, ячейки закрашиваются рандомными цветами   //DONE
        //TODO: Сделать игру
        int N = 5;
        public Form1()
        {
            InitializeComponent();
            CreateGrid(N);
        }
        void CreateGrid(int n)
        {
            //В таблице dataGridView1:
            dataGridView1.ColumnCount = n;  //Количество колонок равно n
            dataGridView1.RowCount = n;     //Количество строк равно n
            //Хотим, чтобы таблица занимала всё возможное пространство по форме
            int w = dataGridView1.Width > dataGridView1.Height ? dataGridView1.Height : dataGridView1.Width;
            for (int i = 0; i < n; i++) //Циклом проходим по колонкам и по строкам
            {
                dataGridView1.Rows[i].Height = w / n;
                dataGridView1.Columns[i].Width = w / n;
            }
        }
        void DyedGrid()
        {
            Random random = new Random();
            for (int i = 0; i < dataGridView1.ColumnCount; i++)
            {
                for (int j = 0; j < dataGridView1.RowCount; j++)
                {
                    dataGridView1[i, j].Value = random.Next(1,4);
                    switch(dataGridView1[i,j].Value)
                    {
                        case 1: dataGridView1[i, j].Style.BackColor = Color.Green; break;
                        case 2: dataGridView1[i, j].Style.BackColor = Color.Red; break;
                        case 3: dataGridView1[i, j].Style.BackColor = Color.Blue; break;
                    }
                    //dataGridView1[i, j].Value = null;
                }
            }

        }
        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            N = trackBar1.Value;    //Значение положения trackBar1 присваиваем в переменную(запоминаем)
            CreateGrid(N);
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            //Событие resize говорит нам, что когда мы будем изменять окно, таблица будет подстраиваться
            //под форму
            CreateGrid(N);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DyedGrid();
        }

        void PoofCellGreen(int col, int row)
        {
            if (col < 0 || row < 0 || col > dataGridView1.ColumnCount - 1 || row > dataGridView1.RowCount - 1)
                return;
            if (dataGridView1[col, row].Style.BackColor == Color.White)
                return;
            if (dataGridView1[col, row].Style.BackColor == Color.Green)
            {
                dataGridView1[col, row].Style.BackColor = Color.White;
            }
            else return;
            PoofCellGreen(col - 1, row);
            PoofCellGreen(col, row - 1);
            PoofCellGreen(col + 1, row);
            PoofCellGreen(col, row + 1);
        }
        void PoofCellBlue(int col, int row)
        {
            if (col < 0 || row < 0 || col > dataGridView1.ColumnCount - 1 || row > dataGridView1.RowCount - 1)
                return;
            if (dataGridView1[col, row].Style.BackColor == Color.White)
                return;
            if (dataGridView1[col, row].Style.BackColor == Color.Blue)
            {
                dataGridView1[col, row].Style.BackColor = Color.White;
            }
            else return;
            PoofCellBlue(col - 1, row);
            PoofCellBlue(col, row - 1);
            PoofCellBlue(col + 1, row);
            PoofCellBlue(col, row + 1);
        }
        void PoofCellRed(int col, int row)
        {
            if (col < 0 || row < 0 || col > dataGridView1.ColumnCount - 1 || row > dataGridView1.RowCount - 1)
                return;
            if (dataGridView1[col, row].Style.BackColor == Color.White)
                return;
            if (dataGridView1[col, row].Style.BackColor == Color.Red)
            {
                dataGridView1[col, row].Style.BackColor = Color.White;
            }
            else return;
            PoofCellRed(col - 1, row);
            PoofCellRed(col, row - 1);
            PoofCellRed(col + 1, row);
            PoofCellRed(col, row + 1);
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int col = e.ColumnIndex;
            int row = e.RowIndex;
            PoofCellGreen(col, row);
            PoofCellBlue(col, row);
            PoofCellRed(col, row);
            dataGridView1.ClearSelection();
        }
        
    }
}
