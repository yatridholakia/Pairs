using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections;
using System.Threading;
using System.Media;

namespace Pairs
{
    public partial class Form1 : Form
    {
        int[] Images = {1,2,3,4,5,6,7,8,9,10,11,12,13,14,15,16,17,18,19,20,21,22,23,24};
        int flipCount = 0, counter=0;
        DataTable Players = new DataTable();
        DataTable pair = new DataTable();
       // SoundPlayer pop;
        //SoundPlayer PairSound;
         int buttonCounter = 0;
       
        public Form1()
        {
            InitializeComponent();
           // pop = new SoundPlayer("pop.wav");
            //PairSound = new SoundPlayer("Got_Item.wav");
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
            
            pair.Columns.Add("ButtonName", typeof(string));
            pair.Columns.Add("ImageNo", typeof(int));
            pair.Columns.Add("State",typeof(bool));

            Players.Columns.Add("PlayerName", typeof(string));
            Players.Columns.Add("Points", typeof(int));
            Players.Columns.Add("CurrentPlayer", typeof(bool));

            
            Random rnd = new Random();
            int[] MyRandomArray = Images.OrderBy(x => rnd.Next()).ToArray();
            for (int i = 1; i <= 24; i++)
            {
                pair.Rows.Add("btn" + i, MyRandomArray[i-1], false);
            }
                

                dataGridView1.DataSource = pair;

               Players.Rows.Add(Program.Player1, 0, true);
               Players.Rows.Add(Program.Player2, 0, false);

               //for (int i = 0; i < Players.Rows.Count; i++)
               //{
               //    if ((Convert.ToBoolean(Players.Rows[i]["CurrentPlayer"]) == true))
               //    {
               //        MessageBox.Show(Players.Rows[i]["PlayerName"].ToString() + " Goes First!");
                      
               //    }

               //}

               Player1Name.Text = Players.Rows[0]["PlayerName"].ToString();
               Player2name.Text = Players.Rows[1]["PlayerName"].ToString();
               player1score.Text = Players.Rows[0]["Points"].ToString();
               Player2score.Text = Players.Rows[1]["Points"].ToString();

               dataGridView2.DataSource = Players;
               
                
        }

        private void btn1_Click(object sender, EventArgs e)
        {
            DoThis(btn1);
        }


        void flip(Button buttonname)
        {
           
            buttonname.BackgroundImage = Image.FromFile(Application.StartupPath + "\\Images\\" + getImageNo(buttonname.Name).ToString() + ".png");
           // pop.Play();
            for (int i = 0; i < pair.Rows.Count; i++)
            {
                if (buttonname.Name == pair.Rows[i][0].ToString())
                {
                    pair.Rows[i]["State"] = true;
                    buttonname.Enabled = false;
                    flipCount++;
                }
            }
            if (flipCount == 2)
            {
                checkForPair();
                
                Thread.Sleep(700);
                unFlip();
            }
            //else if (flipCount > 2)
            //{
              //  unFlip();
            //}
            
           
        }

        void unFlip()
        {
            
            Image img = Image.FromFile(Application.StartupPath+"\\card-small.jpg");
            
            for (int i = 0; i < pair.Rows.Count; i++)
            {
                if ((Convert.ToBoolean(pair.Rows[i]["State"]))==true)
                {
                    pair.Rows[i]["State"] = false;
                }
            }

            foreach (Control c in this.Controls)
            {
                if (c is Button)
                {
                    ((Button)c).Enabled = true;
                   ((Button)c).BackgroundImage = img;
                }
            }

            flipCount = 0;

            if (counter == 0)
            {
                SwitchPlayer();
            }
        }

        void checkForPair()
        {
            ArrayList pairChecker = new ArrayList();
            Image img = Image.FromFile(Application.StartupPath + "\\card-small.jpg");
            int internalCounter = 0;
            
            for (int i = 0; i < pair.Rows.Count; i++)
            {
                if ((Convert.ToBoolean(pair.Rows[i]["State"])) == true)
                {
                    pairChecker.Add(pair.Rows[i][1].ToString());
                }
            }

            if(Convert.ToInt32(pairChecker[0])>12)
            {
                if (Convert.ToInt32(pairChecker[0]) - 12 == Convert.ToInt32(pairChecker[1]))
                {
                  //  MessageBox.Show("It's A Pair!!");
                    for (int i = 0; i < Players.Rows.Count; i++)
                    {
                        if ((Convert.ToBoolean(Players.Rows[i]["CurrentPlayer"])) == true)
                        {
                            Players.Rows[i]["Points"] = Convert.ToInt32(Players.Rows[i]["Points"].ToString()) + 1;
                        }
                    }
                    updatePoints();
                    counter++; internalCounter++;
                    HideCards(int.Parse(pairChecker[0].ToString()), int.Parse(pairChecker[1].ToString()));
                }
            }

            else if ((Convert.ToInt32(pairChecker[0]) <= 12))
            {
                if (Convert.ToInt32(pairChecker[0]) + 12 == Convert.ToInt32(pairChecker[1]))
                {
                    //MessageBox.Show("It's A Pair!!");
                    for (int i = 0; i < Players.Rows.Count; i++)
                    {
                        if ((Convert.ToBoolean(Players.Rows[i]["CurrentPlayer"])) == true)
                        {
                            Players.Rows[i]["Points"] = Convert.ToInt32(Players.Rows[i]["Points"].ToString()) + 1;
                        }
                    }
                    updatePoints();
                    counter++; internalCounter++;
                    HideCards(int.Parse(pairChecker[0].ToString()), int.Parse(pairChecker[1].ToString()));
                }
            }

            if(internalCounter == 0)
            {
                counter = 0;
            }

            

            //foreach (String str in pairChecker)
            //{
            //    label1.Text += str + "\n";
            //}
            
        }

        void SwitchPlayer()
        {
            for (int i = 0; i < Players.Rows.Count; i++)
            {
                if ((Convert.ToBoolean(Players.Rows[i]["CurrentPlayer"])) == true)
                {
                    Players.Rows[i]["CurrentPlayer"] = false;
                }

                else if ((Convert.ToBoolean(Players.Rows[i]["CurrentPlayer"])) == false)
                {
                    Players.Rows[i]["CurrentPlayer"] = true;
                    ChangeArrowPointer(Players.Rows[i]["PlayerName"].ToString());
                
                   // MessageBox.Show(Players.Rows[i]["PlayerName"].ToString() + "'s Turn!");

                }
            }
        }

        int getImageNo(string buttonname)
        {
            int imageNo = 0;
            for (int i = 0; i < pair.Rows.Count; i++)
            {
                if (buttonname == pair.Rows[i][0].ToString())
                {
                    imageNo = Convert.ToInt32(pair.Rows[i][1].ToString());
                }
            }

            return imageNo;
        }

        void HideCards(int c1, int c2)
        {
            ArrayList buttonNames = new ArrayList();
            
            for (int i = 0; i < pair.Rows.Count; i++)
            {
                if (Convert.ToInt32(pair.Rows[i][1].ToString()) == c1 || Convert.ToInt32(pair.Rows[i][1].ToString()) == c2)
                {
                    buttonNames.Add(pair.Rows[i][0].ToString());
                }
            }

            foreach (Control c in this.Controls)
            {
                if (c is Button)
                {
                    if (((Button)c).Name == buttonNames[0].ToString() || ((Button)c).Name == buttonNames[1].ToString())
                    {
                        ((Button)c).Visible = false;
                        buttonCounter++;
                    }
                }
            }
            label1.Text = buttonCounter+"";

            if (buttonCounter == 24)
            {
                String winner = "";
                if (Convert.ToInt32(Players.Rows[0]["Points"].ToString()) > Convert.ToInt32(Players.Rows[1]["Points"].ToString()))
                {
                    winner = Players.Rows[0]["PlayerName"].ToString() + "\nWon The Game!";
                }

                else if (Convert.ToInt32(Players.Rows[0]["Points"].ToString()) < Convert.ToInt32(Players.Rows[1]["Points"].ToString()))
                {
                    winner = Players.Rows[1]["PlayerName"].ToString() + "\nWon The Game!";
                }

                else if (Convert.ToInt32(Players.Rows[0]["Points"].ToString()) == Convert.ToInt32(Players.Rows[1]["Points"].ToString()))
                {
                    winner = "It's a Tie!";
                }

                foreach (Control c in this.Controls)
                {
                    c.Visible = false;
                }

                lblwinner.Text = winner;
                panelwinner.Visible = true;

            }
        }

        private void btn2_Click(object sender, EventArgs e)
        {
            DoThis(btn2);
        }

        private void btn3_Click(object sender, EventArgs e)
        {
            DoThis(btn3);
        }

        private void btn4_Click(object sender, EventArgs e)
        {
            DoThis(btn4);
        }

        private void btn5_Click(object sender, EventArgs e)
        {
            DoThis(btn5);
        }

        private void btn6_Click(object sender, EventArgs e)
        {
            DoThis(btn6);
        }

        private void btn7_Click(object sender, EventArgs e)
        {
            DoThis(btn7);
        }

        private void btn8_Click(object sender, EventArgs e)
        {
            DoThis(btn8);
        }

        private void btn9_Click(object sender, EventArgs e)
        {
            DoThis(btn9);
        }

        private void btn10_Click(object sender, EventArgs e)
        {
            DoThis(btn10);
        }

        private void btn11_Click(object sender, EventArgs e)
        {
            DoThis(btn11);
        }

        private void btn12_Click(object sender, EventArgs e)
        {
            DoThis(btn12);
        }

        private void btn13_Click(object sender, EventArgs e)
        {
            DoThis(btn13);
        }

        private void btn14_Click(object sender, EventArgs e)
        {
            DoThis(btn14);
        }

        private void btn15_Click(object sender, EventArgs e)
        {
            DoThis(btn15);
        }

        private void btn16_Click(object sender, EventArgs e)
        {
            DoThis(btn16);
        }

        private void btn17_Click(object sender, EventArgs e)
        {
            DoThis(btn17);
        }

        private void btn18_Click(object sender, EventArgs e)
        {
            DoThis(btn18);
        }

        private void btn19_Click(object sender, EventArgs e)
        {
            DoThis(btn19);
        }

        private void btn20_Click(object sender, EventArgs e)
        {
            DoThis(btn20);
        }

        private void btn21_Click(object sender, EventArgs e)
        {
            DoThis(btn21);
        }

        private void btn22_Click(object sender, EventArgs e)
        {
            DoThis(btn22);
        }

        private void btn23_Click(object sender, EventArgs e)
        {
            DoThis(btn23);
        }

        private void btn24_Click(object sender, EventArgs e)
        {
            DoThis(btn24);
        }

        void DoThis(Button btn)
        {
            flip(btn);
            
           // unFlip();
        }

        void updatePoints()
        {
            player1score.Text = Players.Rows[0]["Points"].ToString();
            Player2score.Text = Players.Rows[1]["Points"].ToString();
        }

        void ChangeArrowPointer(String playerName)
        {
            if (playerName == Players.Rows[0]["PlayerName"].ToString())
            {
                lblplayer1.Visible = true;
                lblplayer2.Visible = false;
            }

            else if (playerName == Players.Rows[1]["PlayerName"].ToString())
            {
                lblplayer2.Visible = true;
                lblplayer1.Visible = false;
            }
        }

        private void btnplayagain_Click(object sender, EventArgs e)
        {
            (new Form1()).Show();
            this.Hide();
        }

        private void btnexit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (soundcheck.Checked == false)
            {
                soundcheck.BackgroundImage = Image.FromFile("sound-on.png");
                Program.soundplayer.Stop();
            }

            if (soundcheck.Checked == true)
            {
                soundcheck.BackgroundImage = Image.FromFile("sound-off.png");
                Program.soundplayer.PlayLooping();
            }
        }

        private void restartcheck_CheckedChanged(object sender, EventArgs e)
        {
            (new Form1()).Show();
            this.Hide();
        }

        private void checkBox1_CheckedChanged_1(object sender, EventArgs e)
        {
            MessageBox.Show("Your Changes Will Be Lost!");
            (new Game()).Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            (new Game()).Show();
            this.Hide();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Program.soundplayer.Stop();
            Application.Exit();
        }
        
    }
}
