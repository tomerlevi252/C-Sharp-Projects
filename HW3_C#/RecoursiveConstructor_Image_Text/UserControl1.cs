namespace RecoursiveConstructor_Image_Text
{
    public delegate void delegate_MyEventHadler(object sender, myEventArgs e);
    public partial class UserControl1 : UserControl
    {
        public Label[] arrLabels;
        private static Random tempRand = new Random();

        public event delegate_MyEventHadler event_FromUC;

        public UserControl1(Random ucRand)
        {
            InitializeComponent();
            int arrSize = ucRand.Next(18, 30);
            arrLabels = new Label[arrSize];

            int currPosition = 2;
            for (int i = 0; i < arrSize; i++)
            {
                arrLabels[i] = new Label();
                arrLabels[i].TextAlign = ContentAlignment.MiddleCenter;

                if (tempRand.Next(2) == 0)
                    arrLabels[i].Name = "Cat";
                else
                    arrLabels[i].Name = "Dog";

                arrLabels[i].Font = new Font("Arial", 12, FontStyle.Bold);
                arrLabels[i].ForeColor = Color.White;
                arrLabels[i].Location = new Point(currPosition, 3);
                arrLabels[i].Size = new Size(tempRand.Next(75, 110), tempRand.Next(75, 110));

                switch (tempRand.Next(4))
                {
                    case 0: arrLabels[i].BackColor = Color.FromArgb(tempRand.Next(150, 256), 0, 0); break;
                    case 1: arrLabels[i].BackColor = Color.FromArgb(0, tempRand.Next(150, 256),0); break;
                    case 2: arrLabels[i].BackColor = Color.White;
                        arrLabels[i].Tag = "Red"; break;
                    case 3: arrLabels[i].BackColor = Color.White;
                        arrLabels[i].Tag = "Green"; break;
                }

                if (arrLabels[i].BackColor != Color.White)
                    arrLabels[i].Text = arrLabels[i].Name;
                else
                    arrLabels[i].Image = getImage(arrLabels[i].Size, "../../../" + arrLabels[i].Name + "_" + arrLabels[i].Tag + ".jpg");

                currPosition += arrLabels[i].Size.Width + 3;
                this.Controls.Add(arrLabels[i]);
            }
        }

        private Image getImage(Size size, string path)
        {
            Image tempImage = Image.FromFile(path);
            tempImage = (Image)new Bitmap(tempImage, size);
            return tempImage;
        }

        private void UserControl1_Click(object sender, EventArgs e)
        {
            myEventArgs temp = new myEventArgs();
            temp.usercon = this;

            if (event_FromUC != null)
                event_FromUC(this, temp);
        }
        public void DisplaySortedLabels(List<Label> sorted)
        {
            this.Controls.Clear();
            int currX = 2;

            foreach (Label original in sorted)
            {
                Label lbl = new Label();
                lbl.Size = original.Size;
                lbl.BackColor = original.BackColor;
                lbl.ForeColor = original.ForeColor;
                lbl.Font = original.Font;
                lbl.Text = original.Text;
                lbl.TextAlign = ContentAlignment.MiddleCenter;
                lbl.Image = original.Image;
                lbl.Name = original.Name;
                lbl.Tag = original.Tag;

                lbl.Location = new Point(currX, 3);
                currX += lbl.Width + 3;

                this.Controls.Add(lbl);
            }
        }



    }
}
