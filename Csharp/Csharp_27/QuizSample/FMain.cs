using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuizSample
{
    public partial class FMain : Form
    {
        public FMain()
        {
            InitializeComponent();
        }

        private void btnEnd_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnPlayStart_Click(object sender, EventArgs e)
        {
            new FPlayStart().ShowDialog();
        }

        private void btnQuizList_Click(object sender, EventArgs e)
        {
            new FQuizList().ShowDialog();
        }
    }
}
