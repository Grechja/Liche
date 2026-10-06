using System.Security.Cryptography.X509Certificates;

namespace Life
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            dataGridView1.AllowUserToResizeColumns = false;
            dataGridView1.AllowUserToResizeRows = false;

            //textBox1.Text = "50";
            textBox2.Text = "10";
        }

        people peeps1, peeps2;
        int[,] peeptick;
        int peep, cell, cinf, cimm, chea;

        private void button1_Click(object sender, EventArgs e)
        {
            //peep = Convert.ToInt32(textBox1.Text);
            peep = 1;
            cell = Convert.ToInt32(textBox2.Text);
            dataGridView1.RowCount = cell;
            dataGridView1.ColumnCount = cell;
            dataGridView1.Rows[0].Cells[0].Selected = false;
            dataGridView1.Rows[0].Cells[0].Selected = false;

            int h = dataGridView1.Size.Width / dataGridView1.ColumnCount;
            for (int i = 0; i < dataGridView1.RowCount; i++)
            {
                dataGridView1.Rows[i].Height = h;
                dataGridView1.Columns[i].Width = h;
            }

            peeps1 = new people(peep, cell); // (Люди) (Клетки) 
            peeps2 = new people(0, cell);
            peeptick = new int[cell, cell];

            (cinf, cimm, chea) = (1, 0, cell * cell - 1);
            label1.Text += Convert.ToString(chea);
            label3.Text += Convert.ToString(cinf);
            label4.Text += Convert.ToString(cimm);
            peeps1[cell / 2, cell / 2] = '1';
            peeps1.draw(dataGridView1);

        }

        private void button2_Click(object sender, EventArgs e)
        {
            birth();
        }

        private void dataGridView1_CellClick_1(object sender, DataGridViewCellEventArgs e)
        {
            //if (dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.BackColor == Color.White)
            //{
            //    peeps1[e.RowIndex, e.ColumnIndex] = '*';
            //    dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.BackColor = Color.Black;
            //    dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Selected = false;
            //    return;
            //}
            //else
            //{
            //    peeps1[e.RowIndex, e.ColumnIndex] = '\0';
            //    dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.BackColor = Color.White;
            //    dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Selected = false;
            //}

        }
        public void birth()
        {
            Random ran = new Random();
            for (int i = 0; i < cell; i++)
            {
                for (int j = 0; j < cell; j++)
                {
                    char cur = peeps1[i, j];
                    int t = peeptick[i, j];

                    peeps2[i, j] = cur;

                    if (cur == '1') // заражённая
                    {
                        if (t + 1 >= 6)
                        {   
                            cinf -= 1; cimm += 1;
                            peeps2[i, j] = '2';   // через 6 тиков — иммунитет
                            peeptick[i, j] = 0;
                        }
                        else
                        {
                            peeps2[i, j] = '1';
                            peeptick[i, j] = t + 1;
                        }
                    }
                    else if (cur == '2') // иммунная
                    {
                        if (t + 1 >= 4)
                        {
                            cimm -= 1; chea += 1;
                            peeps2[i, j] = '\0';  // через 4 тика — снова здоровая
                            peeptick[i, j] = 0;
                        }
                        else
                        {
                            peeps2[i, j] = '2';
                            peeptick[i, j] = t + 1;
                        }
                    }
                    else // здоровая
                    {
                        peeps2[i, j] = '\0';
                        peeptick[i, j] = 0;

                        // Здоровую может заразить любая заражённая соседка с вероятностью 0.5
                        int nb = peeps1.Neighbours(i, j);
                        if (nb > 0)
                        {
                            bool infect = false;
                            for (int k = 0; k < nb; k++)
                            {
                                if (ran.Next(0, 10) > 7) { infect = true; break; }
                            }
                            if (infect)
                            {
                                chea -= 1;  cinf += 1;
                                peeps2[i, j] = '1';
                                peeptick[i, j] = 0;
                            }
                        }
                    }
                }
            }
            label1.Text = Convert.ToString("Здоровых клеток: " + chea);
            label3.Text = Convert.ToString("Зараженных клеток: " + cinf);
            label4.Text = Convert.ToString("Иммунных клеток: " + cimm);

            peeps2.draw(dataGridView1);
            peeps1 = peeps2;
            peeps2 = new people(0, peeps1.N);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            birth();

        }

        private void button3_Click(object sender, EventArgs e)
        {
            timer1.Enabled = !timer1.Enabled;
            if (timer1.Enabled) button3.Text = "Пауза";
            else button3.Text = "Авто";
        }
    }
}
