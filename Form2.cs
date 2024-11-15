using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Plinkoooo
{
    public partial class Form2 : Form
    {
       public static bool EE = false;
        public Form2()
        {
            InitializeComponent();
        }
        
        private void Form2_Load(object sender, EventArgs e)
        {
            // How many times it landed in that certain multiplyer
            this.lblStat5.Text = "Times Landed on 0.5x: " + Form1.x.ToString();
            this.lblStat1.Text = "Times Landed on 1x: " + Form1.y.ToString();
            this.lblStat2x.Text = "Times Landed on 2x: " + Form1.t.ToString();
            this.lblStat3.Text = "Times Landed on 3x: " + Form1.p.ToString();

            // The Percent landed on each
            this.lblPercent5.Text = "Percent Landed on 0.5x: " + Convert.ToString(Math.Round((Form1.x / Form1.q) * 100, 2) + "%");
            this.lblPercent1.Text = "Percent Landed on 1x: " + Convert.ToString(Math.Round((Form1.y/Form1.q) * 100 ,2) + "%");
            this.lblPercent2.Text = "Percent Landed on 2x: " + Convert.ToString(Math.Round((Form1.t / Form1.q) * 100, 2) + "%");
            this.lblPercent3.Text = "Percent Landed on 3x: " + Convert.ToString(Math.Round((Form1.p / Form1.q) * 100, 2)+"%");

            // Total Amount of Balls dropped
            this.lblTotalBalls.Text = "Total Balls Dropped: " + Form1.q.ToString("N0");

            // Highest Amount of Money made during the game
            this.lblHAmount.Text = "Highest Amount: " + Form1.HighestAmount.ToString("N0");

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            EE = true;
        }
    }
}
