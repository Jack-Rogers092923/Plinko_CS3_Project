using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace Plinkoooo
{
    public partial class Form1 : Form
    {
        public static double x = 0;
        public static double y = 0;
        public static double t = 0;
        public static double p = 0;
        public static double q = 0;
        public static int HighestAmount = 10000;
        public Form1()
        {
            InitializeComponent();
        }



        // Players Balance
       
        double pBalance = 10000;
        double bet = 0;
       

        
        // Make a constant fall speed for the balls
        int fallSpeed = 3;
        int spawnCount = 10;

        // Make a random for the ball going either left or right
        int NachLinksOrderRecht = 0;
        Random random = new Random();

        // Make list for balls wall and multiplyer 
        List<PictureBox> walls = new List<PictureBox>();
        List<Label> balls = new List<Label>();
        List<Label> multiplier = new List<Label>();
        private void Form1_Load(object sender, EventArgs e)
        {
            // Display Players Current Balance on form load
            lblCurrentBalance.Text = ("Current Balance:  " + "$" + pBalance.ToString("N0"));

            // Add all the walls into the list
            for (int i = 1; i <= 28; i++)
            {
                walls.Add((PictureBox)this.Controls["pb" + i.ToString()]);
            }

            // Add all of the buckets into a list for multiplication
            for (int i = 1; i <= 8; i++)
            {
                multiplier.Add((Label)this.Controls["lbl" + i.ToString()]);
            }




        }
        private void button1_Click(object sender, EventArgs e)
        { }

        private void tmrBallDrop_Tick(object sender, EventArgs e)
        {
          
            try {

                for (int i = 0; i <= balls.Count; i++)
                {
                    balls[i].Top += fallSpeed;
                    if (balls[i].Location.Y > this.ClientSize.Height)
                    {
                        this.Controls.Remove(balls[i]);
                        balls[i].Dispose();
                        balls.RemoveAt(i);
                    }
                }
            }
            catch { }
            try
            {
                for (int i = 0; i < balls.Count; i++)
                {


                    for (int j = 0; j < walls.Count; j++)
                    {
                        NachLinksOrderRecht = random.Next(1, 3);

                        if (balls[i].Bounds.IntersectsWith(walls[j].Bounds))
                        {
                            if (NachLinksOrderRecht == 1)
                            {
                                balls[i].Location = new Point(balls[i].Location.X + walls[j].Width / 2, balls[i].Location.Y);
                            }
                            if (NachLinksOrderRecht == 2)
                            {
                                balls[i].Location = new Point(balls[i].Location.X - walls[j].Width / 2, balls[i].Location.Y);
                            }
                        }

                    }
                    for (int c = 0; c < multiplier.Count; c++)
                    {

                        //if (balls[i].Bounds.IntersectsWith(multiplier[c].Bounds))
                        //{
                            if (balls[i].Bounds.IntersectsWith(multiplier[c].Bounds))
                            {

                            
                                // Remove and dispose of the ball
                                this.Controls.Remove(balls[i]);
                                balls[i].Dispose();
                                balls.RemoveAt(i);
                                
                                // Adjust balance based on multiplier
                                double multiplierFactor;
                                switch (c)
                                {
                                    case 7:
                                    case 8:
                                        multiplierFactor = 3;
                                    p += 1;
                                    q += 1;
                                        break;
                                    case 6:
                                    case 5:
                                        multiplierFactor = 2;
                                    t += 1;
                                    q += 1;
                                    break;
                                    case 1:
                                    case 4:
                                        multiplierFactor = 1;
                                    y += 1;
                                    q += 1;
                                    break;
                                case 3:
                                case 2:
                                    multiplierFactor = 0.5;
                                    x += 1;
                                    q += 1;
                                    break;
                                default: // Assuming that the only reason this will happen is because stupid code
                                        multiplierFactor = 1;
                                    y += 1;
                                    q += 1;
                                    // Keep bet because stupid code
                                    break;
                                }
                                 
                                pBalance += Convert.ToInt32(Math.Round(multiplierFactor * bet));
                            if (pBalance >= HighestAmount)
                            {
                                HighestAmount = Convert.ToInt32(pBalance);
                            }

                            // Consolidate UI update outside the conditional blocks
                            
                            lblCurrentBalance.Text = $"Current Balance: ${pBalance.ToString("N0")}";
                            
                               
                               
                            }
                            //bets for each bucket
                           // if (c == 7 || c == 8)
                           // { pBalance += Convert.ToInt32(Math.Round(3 * bet));
                            //    lblCurrentBalance.Text = ("Current Balance:  " + "$" + pBalance.ToString());
                            //}
                            //else if (c == 6 || c == 5)
                           // { pBalance += Convert.ToInt32(Math.Round(1.5 * bet));
                            //    lblCurrentBalance.Text = ("Current Balance:  " + "$" + pBalance.ToString());
                            //}
                            //else if (c == 3 || c == 4)
                            //{ pBalance += Convert.ToInt32(Math.Round(1 * bet));
                             //   lblCurrentBalance.Text = ("Current Balance:  " + "$" + pBalance.ToString());
                            //}
                            //else if (c == 1 || c == 2)
                            //{ pBalance += Convert.ToInt32(Math.Round(bet / 2));
                           //     lblCurrentBalance.Text = ("Current Balance:  " + "$" + pBalance.ToString());
                           // }
                            //this.Controls.Remove(balls[i]);
                            //balls[i].Dispose();
                            //balls.RemoveAt(i);
                        //}
                    }
                }
            }
            catch { }


        }
        private void Form1_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                bet = Convert.ToDouble(txtBetAmount.Text);
                if (bet > pBalance)
                {
                    MessageBox.Show("You do not have enough money for this bet loser");
                }

                if (txtBetAmount.TextLength > 0 & pBalance >= bet)
                {

                    bet = Convert.ToDouble(txtBetAmount.Text);
                    Spawn();
                    pBalance -= bet;
                    lblCurrentBalance.Text = ("Current Balance:  " + "$" + pBalance.ToString("N0"));
                }

            }
            catch { }

            
            
           
        }
           
        
        private void Spawn()
        {
            //create the ball drop
            Label ball = new Label();
            ball.Size = new Size(10, 10);
            int x = this.ClientSize.Width / 2 - ball.Width / 2;
            ball.Location = new Point(x, 0);
            ball.BackColor = Color.Black;
            this.Controls.Add(ball); //add the ball to the list and apply gravity 
            balls.Add(ball);

        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {

        }

        private void btnStats_Click(object sender, EventArgs e)
        {
            Form2 f2 = new Form2();
            f2.ShowDialog();
            if(Form2.EE == true)
            {
                pBalance = 10000000;
            }
        }

        private void btnAutoDrop_Click(object sender, EventArgs e)
        {
            
            bet = Convert.ToDouble(txtBetAmount.Text);
            tmrWait.Start();

            if (pBalance < bet * 10)
                {
                tmrWait.Stop();
                MessageBox.Show("You do not have enough for this bet please enter a new value.");
                    
                }
                
               else if (pBalance >= (bet * 10))
                {
                    pBalance -= bet * 10;
                    lblCurrentBalance.Text = ("Current Balance:  " + "$" + pBalance.ToString("N0"));
                    
                }
                 

              

                       
            
            
            
        }

        private void tmrWait_Tick(object sender, EventArgs e)
        {
            if(spawnCount > 0)
            {
                Spawn();
                spawnCount--;
            }
            else
            {
                tmrWait.Stop();
                spawnCount = 10;
            }
        }
    }
}
