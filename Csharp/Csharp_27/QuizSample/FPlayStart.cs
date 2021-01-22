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
    public partial class FPlayStart : Form
    {
        public FPlayStart()
        {
            InitializeComponent();
        }

        private void FPlayStart_Load(object sender, EventArgs e)
        {
            cbxCategory.Items.Add("ぜんぶ");

            if (!ManagerDb.TryGetData(ManagerDb.TABLETYPE.CategoryTable, out var dataTable))
            {
                MessageBox.Show("カテゴリが読み込めませんでした。ログファイルを確認してください。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                for (var i = 0; i < dataTable.Rows.Count; i++)
                {
                    cbxCategory.Items.Add(dataTable.Rows[i]["category_name"]);
                }
            }
        }        

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.Close();
            new FPlay(cbxCategory.SelectedIndex).ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// カテゴリを選択しないとOKボタンを押せないようにする
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbxCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnOK.Enabled = true;
        }
    }
}
