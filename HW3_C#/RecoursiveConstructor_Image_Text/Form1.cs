using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace RecoursiveConstructor_Image_Text
{
    public partial class Form1 : Form
    {
        private UserControl1[] arrUC;
        private int arrUser_size = 2;
        static private Form1 Cat_Form, Dog_Form;
        private static Random formRand = new Random();


        //*************
        static private UserControl1 Dog, Cat;
        static private int count = 0;
        private List<Label> dogList = new List<Label>();
        private List<Label> catList = new List<Label>();

        //*************
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        public Form1(int counter)
        {
            InitializeComponent();
            this.AutoScaleMode = AutoScaleMode.Dpi;
            this.WindowState = FormWindowState.Normal;

            arrUC = new UserControl1[arrUser_size];
            for (int i = 0; i < arrUser_size; i++)
            {
                arrUC[i] = new UserControl1(formRand);
                arrUC[i].Location = new Point(160, 30 + 120 * i);
                arrUC[i].event_FromUC += new delegate_MyEventHadler(Form_event_FromUC);
                this.Controls.Add(arrUC[i]);
            }

            if (counter == 2)
            {
                Cat_Form = this;

                Form1 temp = new Form1(1);
                temp.Show();
            }

            if (counter == 1)
            {
                this.Text = "Dog";
                Dog_Form = this;

                if (formRand.Next(2) == 0)
                {
                    Cat_Form.Min_Max_label.Text = "Min";
                    Dog_Form.Min_Max_label.Text = "Max";
                }
                else
                {
                    Cat_Form.Min_Max_label.Text = "Max";
                    Dog_Form.Min_Max_label.Text = "Min";
                }

                if (formRand.Next(2) == 0)
                {
                    Cat_Form.Image_Text_label.Text = "Image";
                    Cat_Form.bySize_byBrightness_label.Text = "bySize";
                    Dog_Form.Image_Text_label.Text = "Text";
                    Dog_Form.bySize_byBrightness_label.Text = "byBrightness";
                }
                else
                {
                    Dog_Form.Image_Text_label.Text = "Image";
                    Dog_Form.bySize_byBrightness_label.Text = "bySize";
                    Cat_Form.Image_Text_label.Text = "Text";
                    Cat_Form.bySize_byBrightness_label.Text = "byBrightness";
                }
            }
        }
        private void Form_event_FromUC(object sender, myEventArgs e)
        {
            myEventArgs temp = e;

            if (this.Text == "Dog") 
                Dog = temp.usercon;
            else 
                Cat = temp.usercon;

            count++;

            if (count == 2)
                actionSortRedGreen();
            if (count == 4)
                sortAndDisplay();
            

        }

        void actionSortRedGreen()
        {
            for (int i = 0; i < Dog.arrLabels.Length; i++)
            {
                if (Dog.arrLabels[i].Name == "Dog" && (Dog.arrLabels[i].Tag == "Red" || Dog.arrLabels[i].Tag == "Green"))
                {
                    dogList.Add(Dog.arrLabels[i]);
                }
                else catList.Add(Dog.arrLabels[i]);
            }

            for (int i = 0; i < Cat.arrLabels.Length; i++)
            {
                if (Cat.arrLabels[i].Name == "Cat" && (Cat.arrLabels[i].Tag == "Red" || Cat.arrLabels[i].Tag == "Green"))
                {
                    catList.Add(Cat.arrLabels[i]);
                }
                else dogList.Add(Cat.arrLabels[i]);
            }

            string dogColor = Dog_Form.radioButton_Red.Checked ? "Red" : "Green";
            string catColor = Cat_Form.radioButton_Red.Checked ? "Red" : "Green";

            string dogType = Dog_Form.Image_Text_label.Text; 
            string catType = Cat_Form.Image_Text_label.Text;

            dogList = filter(dogList,dogColor,dogType);
            catList = filter(catList,catColor,catType);

            string minOrMax = Min_Max_label.Text; 
            string mode = bySize_byBrightness_label.Text;

            string dog_minOrMax = Dog_Form.Min_Max_label.Text;
            string dog_mode = Dog_Form.bySize_byBrightness_label.Text;
            Label dog_result = GetMinMaxLabel(dogList, dog_minOrMax, dog_mode);

            string cat_minOrMax = Cat_Form.Min_Max_label.Text;
            string cat_mode = Cat_Form.bySize_byBrightness_label.Text;
            Label cat_result = GetMinMaxLabel(catList, cat_minOrMax, cat_mode);

            if (dog_result != null)
            {
                Dog_Form.MinMax_Result_label.Size = dog_result.Size;
                Dog_Form.MinMax_Result_label.BackColor = dog_result.BackColor;
                Dog_Form.MinMax_Result_label.Image = dog_result.Image;
                Dog_Form.MinMax_Result_label.Text = dog_result.Text;
                Dog_Form.MinMax_Result_label.Font = dog_result.Font;
                Dog_Form.MinMax_Result_label.TextAlign = ContentAlignment.MiddleCenter;
            }

            if (cat_result != null)
            {
                Cat_Form.MinMax_Result_label.Size = cat_result.Size;
                Cat_Form.MinMax_Result_label.BackColor = cat_result.BackColor;
                Cat_Form.MinMax_Result_label.Image = cat_result.Image;
                Cat_Form.MinMax_Result_label.Text = cat_result.Text;
                Cat_Form.MinMax_Result_label.Font = cat_result.Font;
                Cat_Form.MinMax_Result_label.TextAlign = ContentAlignment.MiddleCenter;
            }




        }

        List<Label> filter(List<Label> templist, string selectedColor, string expectedType)
        {
            List<Label> result = new List<Label>();

            foreach (Label lbl in templist)
            {
                bool isImage = lbl.Image != null;
                bool isText = lbl.Image == null;

                if ((expectedType == "Image" && !isImage) || (expectedType == "Text" && !isText))
                    continue; 

                if (isText)
                {
                    if ((selectedColor == "Red" && lbl.BackColor.R > 0 && lbl.BackColor.G == 0 && lbl.BackColor.B == 0) ||
                        (selectedColor == "Green" && lbl.BackColor.G > 0 && lbl.BackColor.R == 0 && lbl.BackColor.B == 0))
                    {
                        result.Add(lbl);
                    }
                }
                else
                {
                    if (lbl.Tag != null && lbl.Tag.ToString() == selectedColor)
                    {
                        result.Add(lbl);
                    }
                }
            }

            return result;
        }

        Label GetMinMaxLabel(List<Label> list, string minOrMax, string mode)
        {
            if (list.Count == 0)
                return null;

            Label best = list[0];
            foreach (Label lbl in list)
            {
                bool isBetter = false;

                if (mode == "bySize")
                {
                    int area1 = lbl.Width * lbl.Height;
                    int area2 = best.Width * best.Height;
                    isBetter = minOrMax == "Min" ? area1 < area2 : area1 > area2;
                }
                else if (mode == "byBrightness")
                {
                    int b1 = (lbl.BackColor.R + lbl.BackColor.G + lbl.BackColor.B) / 3;
                    int b2 = (best.BackColor.R + best.BackColor.G + best.BackColor.B) / 3;
                    isBetter = minOrMax == "Min" ? b1 < b2 : b1 > b2;
                }

                if (isBetter)
                    best = lbl;
            }

            return best;
        }

        List<Label> sortLabelList(List<Label> list, string mode)
        {
            if (mode == "bySize")
            {
                return list.OrderBy(lbl => lbl.Width * lbl.Height).ToList();//פונקצית מיון
            }
            else if (mode == "byBrightness")
            {
                return list.OrderBy(lbl =>(lbl.BackColor.R + lbl.BackColor.G + lbl.BackColor.B) / 3).ToList();//פונקצית מיון
            }

            return list;
        }

        void sortAndDisplay()
        {
            string dogMode = Dog_Form.bySize_byBrightness_label.Text;
            string catMode = Cat_Form.bySize_byBrightness_label.Text;

            List<Label> sortedDog = sortLabelList(dogList, dogMode);
            List<Label> sortedCat = sortLabelList(catList, catMode);

            Dog.DisplaySortedLabels(sortedDog);
            Cat.DisplaySortedLabels(sortedCat);
        }































    }
}

