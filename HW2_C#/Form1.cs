using System;
using System.Drawing;
using System.Windows.Forms;

namespace HW2
{
    public partial class Form1 : Form
    {
        Button[,] butt = new Button[4, 4];
        int[] numbers = new int[15];
        int index = 0;
        Random rnd = new Random();

        Button movingButton;
        int emptyI;
        int emptyJ;

        Point startP, endP;
        int stepX, stepY, steps;

        System.Windows.Forms.Timer slideTimer = new System.Windows.Forms.Timer();
        bool isAnimating = false;

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
                    if (i == 3 && j == 3)
                        break;

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

            butt[3, 3] = null;
            emptyI = 3;
            emptyJ = 3;
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
            if (isAnimating) return; 

            Button cnt = (Button)sender;
            movingButton = cnt;

            int i = cnt.TabIndex / 4;
            int j = cnt.TabIndex % 4;

            if (IsAdjacentToEmptySpace(i,j))    
                StartSlideAnimation(cnt, i, j);
            
        }


        private bool IsAdjacentToEmptySpace(int row, int col)
        {
            return (Math.Abs(emptyI - row) == 1 && emptyJ == col) || (Math.Abs(emptyJ - col) == 1 && emptyI == row);
        }


        void SlideStep(object sender, EventArgs e)
        {
            if (movingButton == null)
            {
                slideTimer.Stop();
                return;
            }

            if (steps > 0)
            {
                movingButton.Location = new Point(movingButton.Location.X + stepX, movingButton.Location.Y + stepY);
                steps--;
            }
            else
            {
                movingButton.Location = endP;
                slideTimer.Stop();

                butt[emptyI, emptyJ] = movingButton;
                butt[movingButton.TabIndex / 4, movingButton.TabIndex % 4] = null;

                emptyI = movingButton.TabIndex / 4;
                emptyJ = movingButton.TabIndex % 4;

                UpdateLogicalGrid();

                movingButton = null;  

                isAnimating = false; 
                boardcheck();
            }
        }


        void StartSlideAnimation(Button btn, int fromRow, int fromCol)
        {
            isAnimating = true; 

            startP = btn.Location;

            endP = new Point(emptyJ * btn.Width + 210,emptyI * btn.Height + 130);

            if (emptyI == fromRow - 1 && emptyJ == fromCol) //Up
            {
                stepX = 0;
                stepY = (endP.Y - startP.Y) / 10;
            }
            else if (emptyI == fromRow + 1 && emptyJ == fromCol) //Down
            {
                stepX = 0;
                stepY = (endP.Y - startP.Y) / 10;
            }
            else if (emptyI == fromRow && emptyJ == fromCol - 1) //Left
            {
                stepX = (endP.X - startP.X) / 10;
                stepY = 0;
            }
            else if (emptyI == fromRow && emptyJ == fromCol + 1) //Right
            {
                stepX = (endP.X - startP.X) / 10;
                stepY = 0;
            }

            steps = 10; 

            movingButton = btn;

            slideTimer.Interval = 20;
            slideTimer.Tick -= SlideStep;
            slideTimer.Tick += SlideStep;
            slideTimer.Start();

            butt[emptyI, emptyJ] = btn;
            butt[fromRow, fromCol] = null;

        }


        private void UpdateLogicalGrid()
        {
            for (int i = 0; i < 4; i++)
                for (int j = 0; j < 4; j++)
                    if (butt[i, j] != null)
                        butt[i, j].TabIndex = i * 4 + j;
        }        
                

        private void newGameToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            createBoard();
        }


        private void boardcheck()
        {
            if (butt[0, 1] != null && butt[0, 0] != null && butt[0, 0].Text.Trim() == "1" && butt[0, 1].Text.Trim() == "2")
            {
                DialogResult dr = MessageBox.Show("New game?", "Game Over", MessageBoxButtons.YesNo);
                if (dr == DialogResult.Yes)
                    createBoard();
                else
                    this.Close();
            }
        }
    }



}
