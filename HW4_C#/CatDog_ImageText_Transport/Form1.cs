


using System.Diagnostics.Eventing.Reader;

namespace CatDog_ImageText_Transport
{

    public delegate void addDel(UserControl1 UC_To, int counter_To, UserControl1 UC_From, int index_From);
    public delegate void removeDel(UserControl1 UC, int index);

    public partial class Form1 : Form
    {
        private const int Width_From = 1700, Width_To = 1800, Width_Transport = 420;
        private const int N_From_To = 40, N_Transport = 50;

        private UserControl1[] arrUC_From = new UserControl1[4];
        private UserControl1[] arrUC_Transport = new UserControl1[4];
        private UserControl1[] arrUC_To = new UserControl1[4];

        private string[] arrRedGreen_CatDog = { "Cat_Red", "Cat_Green", "Dog_Red", "Dog_Green" };

        private Thread[] toTransport = new Thread[4], fromTransport = new Thread[4];

        private AutoResetEvent[] arrAutoResetEvent_1 = new AutoResetEvent[4], arrAutoResetEvent_2 = new AutoResetEvent[4];

        int[] arrTransportCounter = new int[4];
        int[] arrToCounter = new int[4];
        bool[] arrIsEnd = new bool[4];



        public Form1()
        {
            InitializeComponent();

            for (int i = 0; i < 4; i++)
            {
                arrUC_From[i] = new UserControl1(Width_From, N_From_To, "Full");
                arrUC_From[i].Location = new Point(80, 20 + 95 * i);
                this.Controls.Add(arrUC_From[i]);

                arrUC_To[i] = new UserControl1(Width_To, N_From_To, "Empty");
                arrUC_To[i].Location = new Point(80, 550 + 100 * i);
                this.Controls.Add(arrUC_To[i]);

                arrUC_Transport[i] = new UserControl1(Width_Transport, N_Transport, "Empty");
                arrUC_Transport[i].Location = new Point(80 + 425 * i, 440);
                this.Controls.Add(arrUC_Transport[i]);

                arrAutoResetEvent_1[i] = new AutoResetEvent(false);
                arrAutoResetEvent_2[i] = new AutoResetEvent(false);


            }


        }



        private void startToolStripMenuItem_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < 4; i++)
            {
                toTransport[i] = new Thread(toTransport_Function);
                fromTransport[i] = new Thread(fromTransport_Function);

                toTransport[i].Start(i);
                fromTransport[i].Start(i);

            }
        }



        void toTransport_Function(object o)
        {
            int index = (int)o;

            for (int k = 0; k < 4; k++)
            {
                for (int i = 0; i < N_From_To; i++)
                {
                    Label l1 = arrUC_From[k].arrLabels[i];
                    if (l1 == null) continue;

                    if (index == 0 && l1.Name == "Cat")
                    {
                        if (l1.Tag == "Red" || l1.BackColor == Color.Red)
                        {
                            this.Invoke(new addDel(add), arrUC_Transport[index], arrTransportCounter[index], arrUC_From[k], i);
                            this.Invoke(new removeDel(remove), arrUC_From[k], i);
                            arrTransportCounter[index]++;
                            Thread.Sleep(100);
                            if (arrTransportCounter[index] == 8)
                            {
                                arrAutoResetEvent_1[index].Set();
                                arrAutoResetEvent_2[index].WaitOne();
                                arrTransportCounter[index] = 0;
                            }
                        }

                    }

                    if (index == 1 && l1.Name == "Cat")
                    {
                        if (l1.Tag == "Green" || l1.BackColor == Color.Green)
                        {
                            this.Invoke(new addDel(add), arrUC_Transport[index], arrTransportCounter[index], arrUC_From[k], i);
                            this.Invoke(new removeDel(remove), arrUC_From[k], i);
                            arrTransportCounter[index]++;
                            Thread.Sleep(100);
                            if (arrTransportCounter[index] == 8)
                            {
                                arrAutoResetEvent_1[index].Set();
                                arrAutoResetEvent_2[index].WaitOne();
                                arrTransportCounter[index] = 0;

                            }
                        }

                    }

                    if (index == 2 && l1.Name == "Dog")
                    {
                        if (l1.Tag == "Red" || l1.BackColor == Color.Red)
                        {
                            this.Invoke(new addDel(add), arrUC_Transport[index], arrTransportCounter[index], arrUC_From[k], i);
                            this.Invoke(new removeDel(remove), arrUC_From[k], i);
                            arrTransportCounter[index]++;
                            Thread.Sleep(100);
                            if (arrTransportCounter[index] == 8)
                            {
                                arrAutoResetEvent_1[index].Set();
                                arrAutoResetEvent_2[index].WaitOne();
                                arrTransportCounter[index] = 0;

                            }
                        }

                    }

                    if (index == 3 && l1.Name == "Dog")
                    {
                        if (l1.Tag == "Green" || l1.BackColor == Color.Green)
                        {
                            this.Invoke(new addDel(add), arrUC_Transport[index], arrTransportCounter[index], arrUC_From[k], i);
                            this.Invoke(new removeDel(remove), arrUC_From[k], i);
                            arrTransportCounter[index]++;
                            Thread.Sleep(100);
                            if (arrTransportCounter[index] == 8)
                            {
                                arrAutoResetEvent_1[index].Set();
                                arrAutoResetEvent_2[index].WaitOne();
                                arrTransportCounter[index] = 0;

                            }
                        }

                    }

                }

            }
            arrIsEnd[index] = true;
            arrAutoResetEvent_1[index].Set();
        }




        void fromTransport_Function(object o)
        {
            int index = (int)o;
            int indexImage = 0;
            int indexColor = arrUC_To[index].arrLabels.Length - 1;

            int xImage = 2;
            int xColor = Width_To - 95;
            
            while (true)
            {

                arrAutoResetEvent_1[index].WaitOne();

                for (int i = 0; i < arrUC_Transport[index].arrLabels.Length; i++)
                {
                    Label l2 = arrUC_Transport[index].arrLabels[i];
                    if (l2 == null) continue;

                    if (l2.BackColor == Color.White)
                    {
                        this.Invoke(new addDel(add), arrUC_To[index], indexImage, arrUC_Transport[index], i);
                        this.Invoke(new removeDel(remove), arrUC_Transport[index], i);
                        this.Invoke(new Action( () =>{ arrUC_To[index].arrLabels[indexImage].Location = new Point(xImage, 3);}));
                        xImage += l2.Width + 2;
                        indexImage++;
                    }
                    else
                    {
                        this.Invoke(new addDel(add), arrUC_To[index], indexColor, arrUC_Transport[index], i);
                        this.Invoke(new removeDel(remove), arrUC_Transport[index], i);
                        this.Invoke(new Action(() => { arrUC_To[index].arrLabels[indexColor].Location = new Point(xColor, 3); }));
                        xColor -= l2.Width + 2;
                        indexColor--;
                    }
                    Thread.Sleep(100);
                }

                if (!arrIsEnd[index])
                {
                    arrTransportCounter[index] = 0;
                    arrAutoResetEvent_2[index].Set();
                }
                else break;


            }

        }

        private void add(UserControl1 UC_To, int counter_To, UserControl1 UC_From, int index_From)
        {
            UC_To.arrLabels[counter_To] = UC_From.arrLabels[index_From];

            UC_To.Controls.Add(UC_From.arrLabels[index_From]);

            UC_To.arrLabels[counter_To].Location = new Point(3 + 95 * counter_To, 3);
        }



        private void remove(UserControl1 UC, int index)
        {
            UC.Controls.Remove(UC.arrLabels[index]);
            UC.arrLabels[index] = null;
        }



    }
}
