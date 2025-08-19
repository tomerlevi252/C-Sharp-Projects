







namespace CatDog_ImageText_Transport
{
    public partial class UserControl1 : UserControl
    {
        static Random tempRand = new Random();
        public Label[] arrLabels;
        public UserControl1(int width, int MaxCounter, string FullEmpty)
        {
            InitializeComponent();
            arrLabels = new Label[MaxCounter];
            this.Width = width;
            if (FullEmpty == "Full")
            {
                int lastX = 2;
                for (int i = 0; i < MaxCounter; i++)
                {
                    int tempWidth = tempRand.Next(70, 90);
                    if (tempWidth > width - lastX)
                       break;
                    int tempHeight= tempRand.Next(70, 90);

                    arrLabels[i] = new Label();

                    arrLabels[i].Location = new Point(lastX, 3);
                    arrLabels[i].Size = new Size(tempWidth, tempHeight);
                    lastX += arrLabels[i].Width + 2;
                    arrLabels[i].Font = new Font("Arial", 12, FontStyle.Bold);
                    arrLabels[i].ForeColor = Color.White;
                    arrLabels[i].TextAlign = ContentAlignment.MiddleCenter;

                    if (tempRand.Next(2) == 0)
                        arrLabels[i].Name = "Cat";
                    else
                        arrLabels[i].Name = "Dog";

                    switch (tempRand.Next(4))
                    {
                        case 0: arrLabels[i].BackColor = Color.Red; break;
                        case 1: arrLabels[i].BackColor = Color.Green; break;
                        case 2:
                            arrLabels[i].BackColor = Color.White;
                            arrLabels[i].Tag = "Red"; break;
                        case 3:
                            arrLabels[i].BackColor = Color.White;
                            arrLabels[i].Tag = "Green"; break;
                    }

                    if (arrLabels[i].BackColor != Color.White)
                        arrLabels[i].Text = arrLabels[i].Name;
                    else
                        arrLabels[i].Image = getImage(arrLabels[i].Size, "../../../" + arrLabels[i].Name + "_" + arrLabels[i].Tag + ".jpg");

                    this.Controls.Add(arrLabels[i]);
                }
            }
        }

        private Image getImage(Size size, string path)
        {
            Image tempImage = Image.FromFile(path);
            tempImage = (Image)new Bitmap(tempImage, size);
            return tempImage;
        }

    }
}
