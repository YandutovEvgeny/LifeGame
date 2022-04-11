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
        //TODO: Сделать игру //DONE
        int N = 5;
        int gameStep = 0;
        int score = 0;
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
            label1.Text = "";
            label2.Text = "";
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
                    dataGridView1[i, j].Value = null;
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
            timer1.Enabled = true;
            label1.Text = $"Количество шагов: {gameStep = 0}";
            label2.Text = $"Количество очков: {score = 0}";
        }

        void PoofCell(int col, int row, Color color)
        {
            if (col < 0 || row < 0 || col > dataGridView1.ColumnCount - 1 || row > dataGridView1.RowCount - 1)
                return;
            if (dataGridView1[col, row].Style.BackColor == Color.White)
                return;
            if (dataGridView1[col, row].Style.BackColor == color)
                dataGridView1[col, row].Style.BackColor = Color.White;
            else return;
            PoofCell(col - 1, row,color);
            PoofCell(col, row - 1,color);
            PoofCell(col + 1, row,color);
            PoofCell(col, row + 1,color);
        }
        
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int col = e.ColumnIndex;
            int row = e.RowIndex;
            PoofCell(col, row, dataGridView1[col, row].Style.BackColor);
            label1.Text = $"Количество шагов: {gameStep += 1}";
            label2.Text = $"Количество очков: {score += 20}";
            if(timer1.Enabled == false)
            {
                label1.Text = $"Количество шагов: 0";
                label2.Text = $"Количество очков: 0";
            }
            dataGridView1.ClearSelection();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            int emptyColumn = 0;
            for(int col = 0; col < dataGridView1.ColumnCount; col++)
            {
                if (dataGridView1[col, dataGridView1.RowCount-1].Style.BackColor == Color.White &&
                    dataGridView1[col,dataGridView1.RowCount-3].Style.BackColor == Color.White)
                    emptyColumn++;

                for(int row = dataGridView1.RowCount - 1; row >= 0; row--)
                {
                    if(dataGridView1[col,row].Style.BackColor == Color.White)
                    {
                        while (row > 0)
                        {
                            dataGridView1[col, row].Style.BackColor = dataGridView1[col, row - 1].Style.BackColor;
                            dataGridView1[col, row - 1].Style.BackColor = Color.White;
                            row--;
                        }
                    }
                }
            }
            if(emptyColumn == dataGridView1.ColumnCount)
            {
                timer1.Enabled = false;
                //MessageBox.Show($"Поздравляем, вы победили за {gameStep} шагов, ваши очки {score}");
                MessageBox.Show($"Поздравляем, вы победили за {gameStep} шагов, ваши очки {score}", "Победа!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DyedGrid();
            label1.Text = $"Количество шагов: {gameStep = 0}";
            label2.Text = $"Количество очков: {score = 0}";
            timer1.Enabled = true;
        }
    }
}
