using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LifeGame
{
    //TODO: Создать проект и добавить и настроить в неё dataGridView как в этом проекте
    public partial class Form1 : Form
    {
        int N = 5;
        Life life;  //Создаём объект класса
        public Form1()
        {
            InitializeComponent();
            CreateGrid(N);
            life = new Life();  //инициализация объекта класса
        }
        void CreateGrid(int n)
        {
            dataGridView1.ColumnCount = n;
            dataGridView1.RowCount = n;
            int width = dataGridView1.Width > dataGridView1.Height ? dataGridView1.Height : dataGridView1.Width;
            for (int i = 0; i < n; i++)
            {
                dataGridView1.Rows[i].Height = width / n;
                dataGridView1.Columns[i].Width = width / n;
            }
        }
        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            N = trackBar1.Value;
            CreateGrid(N);
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            CreateGrid(N);
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int row = e.RowIndex;
            int col = e.ColumnIndex;
            if(dataGridView1[col, row].Style.BackColor == Color.Black) //Если клетка была чёрной
                dataGridView1[col, row].Style.BackColor = Color.White; //То красим в белую
            else
                dataGridView1[col, row].Style.BackColor = Color.Black; //Иначе красим в чёрную
            dataGridView1.ClearSelection();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)  //Если галочка стоит 
                timer1.Enabled = true;  //таймер запущен
            else
                timer1.Enabled = false; //иначе нет
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            life.GridToArea(dataGridView1, N);  //Считываем данные с таблицы
            life.NextDay();                     //Обновляем день
            life.AreaToGrid(dataGridView1);     //Заносим данные в таблицу
        }
    }
}
