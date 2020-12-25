using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SimpleMemo
{
    public partial class ReplacementForm : Form
    {
        private int nextIndex;
        public ReplacementForm(Form1 f1)
        {
            //引数で受けとったフォームデータががReplacementFormを所有していることを設定するため（今回はForm1）
            this.Owner = f1;
            InitializeComponent();
        }
        private void buttonNextFind_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxFindTxt.Text))
            {
                MessageBox.Show("検索する文字を入力してください。", "検索", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                var oTextBox1 = (TextBox)Owner.Controls["textBox1"];

                if (oTextBox1.Text.IndexOf(textBoxFindTxt.Text, nextIndex) < 0)
                {
                    MessageBox.Show($"\"{textBoxFindTxt.Text}\"が見つかりません。", "検索");
                }
                else
                {
                    oTextBox1.Select(oTextBox1.Text.IndexOf(textBoxFindTxt.Text, nextIndex), textBoxFindTxt.Text.Length);
                    nextIndex = oTextBox1.Text.IndexOf(textBoxFindTxt.Text, nextIndex) + textBoxFindTxt.Text.Length;
                }
            }
        }
        private void buttonReplace_Click(object sender, EventArgs e)
        {            
            if (string.IsNullOrEmpty(textBoxFindTxt.Text))
            {
                MessageBox.Show("検索する文字を入力してください。", "置換", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                var oTextBox1 = (TextBox)Owner.Controls["textBox1"];
                //var oText = oTextBox1.Text;
                //var fText = textBoxFindTxt.Text;
                //var rText = textBoxReplaceTxt.Text;
                //var sIndex = oText.IndexOf(fText, nextIndex);

                if(oTextBox1.Text.IndexOf(textBoxFindTxt.Text, nextIndex) < 0)
                {
                    MessageBox.Show($"\"{textBoxFindTxt.Text}\"が見つかりません。", "置換");
                }
                else
                {
                    //oText = oText.Remove(sIndex, fText.Length);
                    //oTextBox1.Text = oText.Insert(sIndex, rText);

                    oTextBox1.Text = oTextBox1.Text.Insert(oTextBox1.Text.IndexOf(textBoxFindTxt.Text, nextIndex), textBoxReplaceTxt.Text);
                    oTextBox1.Text = oTextBox1.Text.Remove(oTextBox1.Text.IndexOf(textBoxFindTxt.Text, nextIndex), textBoxFindTxt.Text.Length);

                    if(oTextBox1.Text.IndexOf(textBoxFindTxt.Text, nextIndex) < 0)
                    {
                        MessageBox.Show("置換が完了しました。", "置換");
                    }
                    else
                    {
                        oTextBox1.Select(oTextBox1.Text.IndexOf(textBoxFindTxt.Text, nextIndex), textBoxFindTxt.Text.Length);
                    }                    
                }
            }
        }
        private void buttonAllRepleace_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxFindTxt.Text))
            {
                MessageBox.Show("検索する文字を入力してください。", "置換", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                var oTextBox1 = (TextBox)Owner.Controls["textBox1"];
                var rFind = new Regex(textBoxFindTxt.Text);
                var sReplace = textBoxReplaceTxt.Text;

                oTextBox1.Text = rFind.Replace(oTextBox1.Text, sReplace);
            }
        }
        private void buttonCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void ReplacementForm_Activated(object sender, EventArgs e)
        {
            var oTextBox1 = (TextBox)Owner.Controls["textBox1"];
            nextIndex = oTextBox1.SelectionStart;
        }

        private void ReplacementForm_Load(object sender, EventArgs e)
        {
            this.ActiveControl = this.textBoxFindTxt;
        }
    }
}
