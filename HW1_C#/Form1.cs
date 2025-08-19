using System.Globalization;
using System.Windows.Forms;

namespace HW1
{
    public partial class Form1 : Form
    {
        Button[,] butt = new Button[4, 4];
        int[] numbers = new int[15];
        int index = 0;
        Random rnd = new Random();
        int count = 0;

        public Form1()
        {
            InitializeComponent();

            this.Size = new Size(1200, 800);

            createBoard();


        }
        public void createBoard()
        {
            foreach (Button b in butt)
                this.Controls.Remove(b);

            butt = new Button[4, 4];
            index = 0;

            for (int i = 0; i < 15; i++)
                numbers[i] = i + 1;

            ShuffleArray();

            int offsetX = 200;
            int offsetY = 120;

            for (int i = 0; i < 4; i++)
            { 
                for (int j = 0; j < 4; j++)
                {
                    if (butt[i, j] == null)
                    {
                        butt[i, j] = new Button();
                        this.Controls.Add(butt[i, j]);
                    }

                    butt[i, j].Size = new Size(180, 120);
                    butt[i, j].Location = new Point(j * butt[i, j].Width + 10 + offsetX, i * butt[i, j].Height + 10 + offsetY);
                    butt[i, j].Text = rngNumber().ToString();
                    butt[i, j].BackColor = Color.FromArgb(rnd.Next(255), rnd.Next(255), rnd.Next(255));
                    butt[i, j].Font = new Font("Arial", 30);
                    butt[i, j].Click += button_Click;
                    butt[i, j].TabIndex = i * 4 + j;
                }
            }
            butt[3, 3].Visible = false;

        }

        public void ShuffleArray()
        {
            for (int i = 0; i < numbers.Length; i++)
            {
                int j = rnd.Next(i, numbers.Length);
                int temp = numbers[i];
                numbers[i] = numbers[j];
                numbers[j] = temp;
            }
        }

        public int rngNumber()
        {
            if (index >= numbers.Length)
                index = 0;

            return numbers[index++];
        }

        private void button_Click(object sender, EventArgs e)
        {
            Button cnt = (Button)sender;
            int i = cnt.TabIndex / 4;
            int j = cnt.TabIndex % 4;
            count = 0;

            if (j < 3 && butt[i, j + 1].Visible == false)
            {
                butt[i, j].Visible = false;
                butt[i, j + 1].Visible = true;
                butt[i, j + 1].Text = butt[i, j].Text;
                butt[i, j + 1].BackColor = butt[i, j].BackColor;
            }

            if (j > 0 && butt[i, j - 1].Visible == false)
            {
                butt[i, j].Visible = false;
                butt[i, j - 1].Visible = true;
                butt[i, j - 1].Text = butt[i, j].Text;
                butt[i, j - 1].BackColor = butt[i, j].BackColor;
            }

            if (i < 3 && butt[i + 1, j].Visible == false)
            {
                butt[i, j].Visible = false;
                butt[i + 1, j].Visible = true;
                butt[i + 1, j].Text = butt[i, j].Text;
                butt[i + 1, j].BackColor = butt[i, j].BackColor;
            }

            if (i > 0 && butt[i - 1, j].Visible == false)
            {
                butt[i, j].Visible = false;
                butt[i - 1, j].Visible = true;
                butt[i - 1, j].Text = butt[i, j].Text;
                butt[i - 1, j].BackColor = butt[i, j].BackColor;
            }


            boardcheck();

        }

        private void boardcheck()
        {
            if (butt[0, 0].Text.Trim() == "1" && butt[0, 1].Visible == true && butt[0, 0].Visible == true && butt[0, 1].Text.Trim() == "2")
            {
                DialogResult dr = MessageBox.Show("New game?", "Game Over", buttons: MessageBoxButtons.YesNo);
                if (dr == DialogResult.Yes)
                    createBoard();
                else
                    this.Close();
            }



        }

        private void newGameToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            createBoard();
        }
    }






}


