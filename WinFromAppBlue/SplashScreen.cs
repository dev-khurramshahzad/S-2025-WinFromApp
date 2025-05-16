using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFromAppBlue
{
    public partial class SplashScreen : Form
    {
        int progress = 0; 
        public SplashScreen()
        {
            InitializeComponent();
            timer1.Interval = 50;
            timer1.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            progress = progress + 5;

            if (progress >= 100)
            {
                timer1.Stop();
                this.Close();
            }


            label2.Text = progress.ToString();
            progressBar1.Value = progress;
        }
    }
}
