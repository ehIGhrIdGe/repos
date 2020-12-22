using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HelloWorld
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var message = string.Empty;
            var dtNowHour = DateTime.Now.Hour;

            if (dtNowHour >= 5 && dtNowHour <= 9)
            {
                message = "Good Morning";
            }
            else if(dtNowHour >= 10 && dtNowHour <= 17)
            {
                message = "Hello";
            }
            else
            {
                message = "Good evening";
            }


            var uesrReaction = MessageBox.Show(message, "Greeting", MessageBoxButtons.OK, MessageBoxIcon.Information);
            if(uesrReaction == DialogResult.OK) { Application.Exit(); }
        }
    }
}
