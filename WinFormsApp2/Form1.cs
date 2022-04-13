using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace WinFormsApp2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            matchBox.Text = "1";
            mismatchBox.Text = "-1";
            gapBox.Text = "-2";
            textBox1.Text = "GCACGCTG";
            textBox2.Text = "GACGCGCG";
        }
        public static string readBackwards(string index)
        {
            string result = "";
            for (int i = index.Length - 1; i >= 0; i--)
                result += index[i];
            return result;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            DateTime then = DateTime.Now;
            dataGridView1.Rows.Clear();
            string base1;
            base1 = Convert.ToString(textBox1.Text);
            string base2;
            base2 = Convert.ToString(textBox2.Text);
            dataGridView1.RowCount = base1.Length + 2;
            dataGridView1.ColumnCount = base2.Length + 2;
            dataGridView1.ColumnHeadersVisible = false;
            for (int i = 2; i < base1.Length + 2; i++)
            {
                dataGridView1.Rows[i].Cells[0].Value = Convert.ToString(base1[i - 2]);

            }
            for (int i = 2; i < base2.Length + 2; i++)
            {
                dataGridView1.Rows[0].Cells[i].Value = Convert.ToString(base2[i - 2]);
            }
            int match = Convert.ToInt32(matchBox.Text);
            int mismatch = Convert.ToInt32(mismatchBox.Text);
            int gap = Convert.ToInt32(gapBox.Text);
            dataGridView1.Rows[1].Cells[1].Value = 0;
            for (int i = 2; i < base2.Length + 2; i++)
            {
                dataGridView1.Rows[1].Cells[i].Value = Convert.ToInt32(dataGridView1.Rows[1].Cells[i - 1].Value) + gap;
            }
            for (int i = 2; i < base1.Length + 2; i++)
            {
                dataGridView1.Rows[i].Cells[1].Value = Convert.ToInt32(dataGridView1.Rows[i - 1].Cells[1].Value) + gap;
            }
            int topValue;
            int leftValue;
            int leftTop;
            for (int i = 2; i < base1.Length + 2; i++)
            {
                for (int j = 2; j < base2.Length + 2; j++)
                {
                    topValue = Convert.ToInt32(dataGridView1.Rows[i - 1].Cells[j].Value) + gap;
                    leftValue = Convert.ToInt32(dataGridView1.Rows[i].Cells[j - 1].Value) + gap;
                    if (Convert.ToString(dataGridView1.Rows[i].Cells[0].Value) != Convert.ToString(dataGridView1.Rows[0].Cells[j].Value))
                    {
                        leftTop = Convert.ToInt32(dataGridView1.Rows[i - 1].Cells[j - 1].Value) + mismatch;

                        if (leftValue >= topValue && leftValue >= leftTop)
                        {
                            dataGridView1.Rows[i].Cells[j].Value = leftValue;
                        }
                        else if (topValue >= leftValue && topValue >= leftTop)
                        {
                            dataGridView1.Rows[i].Cells[j].Value = topValue;
                        }
                        else if (leftTop >= topValue && leftTop >= leftValue)
                        {
                            dataGridView1.Rows[i].Cells[j].Value = leftTop;
                        }
                    }
                    else
                    {
                        leftTop = Convert.ToInt32(dataGridView1.Rows[i - 1].Cells[j - 1].Value) + match;
                        if (leftValue >= topValue && leftValue >= leftTop)
                        {
                            dataGridView1.Rows[i].Cells[j].Value = leftValue;
                        }
                        else if (topValue >= leftValue && topValue >= leftTop)
                        {
                            dataGridView1.Rows[i].Cells[j].Value = topValue;
                        }
                        else if (leftTop >= topValue && leftTop >= leftValue)
                        {
                            dataGridView1.Rows[i].Cells[j].Value = leftTop;
                        }
                    }
                }
            }
            int score = Convert.ToInt32(dataGridView1.Rows[base1.Length + 1].Cells[base2.Length + 1].Value);
            dataGridView1.Rows[base1.Length + 1].Cells[base2.Length + 1].Style.BackColor = Color.LightGreen;
            string text1 = "";
            string text2 = "";
            for (int j = base2.Length + 1, i = base1.Length + 1; j > 1 || i > 1;)
            {
                if (i == 1)
                {
                    dataGridView1.Rows[i].Cells[j - 1].Style.BackColor = Color.LightGreen;
                    j--;
                }
                else if (j == 1)
                {
                    dataGridView1.Rows[i - 1].Cells[j].Style.BackColor = Color.LightGreen;
                    i--;
                }
                else if (Convert.ToString(dataGridView1.Rows[0].Cells[j].Value) == Convert.ToString(dataGridView1.Rows[i].Cells[0].Value))
                {
                    dataGridView1.Rows[i - 1].Cells[j - 1].Style.BackColor = Color.LightGreen;
                    text1 += Convert.ToString(dataGridView1.Rows[0].Cells[j].Value);
                    text2 += Convert.ToString(dataGridView1.Rows[i].Cells[0].Value); i--; j--;
                }
                else
                {
                    if (Convert.ToInt32(dataGridView1.Rows[i - 1].Cells[j - 1].Value) >= Convert.ToInt32(dataGridView1.Rows[i].Cells[j - 1].Value) && (Convert.ToInt32(dataGridView1.Rows[i - 1].Cells[j - 1].Value) >= Convert.ToInt32(dataGridView1.Rows[i - 1].Cells[j].Value)))
                    {
                        dataGridView1.Rows[i - 1].Cells[j - 1].Style.BackColor = Color.LightGreen;
                        text1 += Convert.ToString(dataGridView1.Rows[0].Cells[j].Value);
                        text2 += Convert.ToString(dataGridView1.Rows[i].Cells[0].Value); i--; j--;
                    }
                    else if (Convert.ToInt32(dataGridView1.Rows[i].Cells[j - 1].Value) >= Convert.ToInt32(dataGridView1.Rows[i - 1].Cells[j - 1].Value) && (Convert.ToInt32(dataGridView1.Rows[i].Cells[j - 1].Value) >= Convert.ToInt32(dataGridView1.Rows[i - 1].Cells[j].Value)))
                    {
                        dataGridView1.Rows[i].Cells[j - 1].Style.BackColor = Color.LightGreen;

                        text1 += Convert.ToString(dataGridView1.Rows[0].Cells[j].Value);
                        text2 += "-"; j--;
                    }
                    else
                    {
                        dataGridView1.Rows[i - 1].Cells[j].Style.BackColor = Color.LightGreen;
                        text1 += "-";
                        text2 += Convert.ToString(dataGridView1.Rows[i].Cells[0].Value); i--;
                    }
                }
            }
            score = Convert.ToInt32(dataGridView1.Rows[base1.Length + 1].Cells[base2.Length + 1].Value);
            label8.Text = readBackwards(text1) + "\n" + readBackwards(text2);
            label7.Text = "Score : " + score;
            DateTime now = DateTime.Now;
            label6.Text = "Execute Time: " + (now.Millisecond - then.Millisecond) + "ms";

        }

        private void yolLabel_Click(object sender, EventArgs e)
        {

        }
    }
}
