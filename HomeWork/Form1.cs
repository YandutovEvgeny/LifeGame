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
        //TODO: Создать проект, добавить и настроить в неё dataGridView
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
    }
}
