﻿using System;
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

        private void ReplacementForm_Activated(object sender, EventArgs e)
        {
            //Form1 に直接 menuEditReplace ボタンを配置した場合は、直下のコードで動く
            //var oTextBox1 = (TextBox)Owner.Controls["textBox1"];
            //nextIndex = oTextBox1.SelectionStart;

            //Form1 の tabControl1 内に menuEditReplace ボタンを配置した場合は、tabControl1 から textBox1 まで順にさかのぼっていかなくてはいけない
            //var oTabControl = (TabControl)Owner.Controls["tabControl1"];
            //var oTabPage1 = oTabControl.Controls["tabPage1"];            
            //var oTextBox1 = (TextBox)oTabPage1.Controls["textBox1"];
            //nextIndex = oTextBox1.SelectionStart;

            //Form1 の tabControl1 に配置された UserControl の内の textBox1 を操作するためには以下のように記述する。
            //デザインフォームで配置されている順番を追って、コントールを定義していく。
            var oTabControl = (TabControl)Owner.Controls["tabControl1"];
            var oTabPage = oTabControl.SelectedTab.Controls["userControl1"].Controls["panel1"];
            //var oTabPage = oTabControl.Controls[$"tabPage{(oTabControl.SelectedIndex + 1).ToString()}"].Controls["userControl1"].Controls["panel1"];
            var oTextBox1 = (TextBox)oTabPage.Controls["textBox1"];
            nextIndex = oTextBox1.SelectionStart;

            ////ちなみに上記は以下のように記述しても同じ。
            //var oTabControl = (TabControl)Owner.Controls["tabControl1"];
            //var oTabPage = oTabControl.Controls[$"tabPage{(oTabControl.SelectedIndex + 1).ToString()}"];
            //var oUserControl = oTabPage.Controls["userControl1"];
            //var oPanel = oUserControl.Controls["panel1"];
            //var oTextBox1 = (TextBox)oPanel.Controls["textBox1"];
            //nextIndex = oTextBox1.SelectionStart;
        }


        private void buttonNextFind_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxFindTxt.Text))
            {
                MessageBox.Show("検索する文字を入力してください。", "検索", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                var oTabControl = (TabControl)Owner.Controls["tabControl1"];
                var oTabPage = oTabControl.SelectedTab.Controls["userControl1"].Controls["panel1"];
                var oTextBox1 = (TextBox)oTabPage.Controls["textBox1"];


                if(chkBox1.Checked != true)
                {
                    if (oTextBox1.Text.IndexOf(textBoxFindTxt.Text, nextIndex, StringComparison.OrdinalIgnoreCase) < 0)
                    {
                        MessageBox.Show($"\"{textBoxFindTxt.Text}\"が見つかりません。", "検索");
                    }
                    else
                    {
                        oTextBox1.Select(oTextBox1.Text.IndexOf(textBoxFindTxt.Text, nextIndex, StringComparison.OrdinalIgnoreCase), textBoxFindTxt.Text.Length);
                        nextIndex = oTextBox1.Text.IndexOf(textBoxFindTxt.Text, nextIndex, StringComparison.OrdinalIgnoreCase) + textBoxFindTxt.Text.Length;
                    }
                }
                else
                {
                    if (oTextBox1.Text.IndexOf(textBoxFindTxt.Text, nextIndex, StringComparison.Ordinal) < 0)
                    {
                        MessageBox.Show($"\"{textBoxFindTxt.Text}\"が見つかりません。", "検索");
                    }
                    else
                    {
                        oTextBox1.Select(oTextBox1.Text.IndexOf(textBoxFindTxt.Text, nextIndex, StringComparison.Ordinal), textBoxFindTxt.Text.Length);
                        nextIndex = oTextBox1.Text.IndexOf(textBoxFindTxt.Text, nextIndex, StringComparison.Ordinal) + textBoxFindTxt.Text.Length;
                    }
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
                var oTabControl = (TabControl)Owner.Controls["tabControl1"];
                var oTabPage = oTabControl.SelectedTab.Controls["userControl1"].Controls["panel1"];
                var oTextBox1 = (TextBox)oTabPage.Controls["textBox1"];

                if (oTextBox1.Text.IndexOf(textBoxFindTxt.Text, nextIndex) < 0)
                {
                    MessageBox.Show($"\"{textBoxFindTxt.Text}\"が見つかりません。", "置換");
                }
                else
                {
                    if(chkBox1.Checked != true)
                    {
                        oTextBox1.Text = oTextBox1.Text.Insert(oTextBox1.Text.IndexOf(textBoxFindTxt.Text, nextIndex, StringComparison.OrdinalIgnoreCase), textBoxReplaceTxt.Text);
                        oTextBox1.Text = oTextBox1.Text.Remove(oTextBox1.Text.IndexOf(textBoxFindTxt.Text, nextIndex, StringComparison.OrdinalIgnoreCase), textBoxFindTxt.Text.Length);

                        if (oTextBox1.Text.IndexOf(textBoxFindTxt.Text, nextIndex) < 0)
                        {
                            MessageBox.Show("置換が完了しました。", "置換");
                        }
                        else
                        {
                            oTextBox1.Select(oTextBox1.Text.IndexOf(textBoxFindTxt.Text, nextIndex, StringComparison.OrdinalIgnoreCase), textBoxFindTxt.Text.Length);
                        }
                    }
                    else
                    {
                        oTextBox1.Text = oTextBox1.Text.Insert(oTextBox1.Text.IndexOf(textBoxFindTxt.Text, nextIndex, StringComparison.Ordinal), textBoxReplaceTxt.Text);
                        oTextBox1.Text = oTextBox1.Text.Remove(oTextBox1.Text.IndexOf(textBoxFindTxt.Text, nextIndex, StringComparison.Ordinal), textBoxFindTxt.Text.Length);

                        if (oTextBox1.Text.IndexOf(textBoxFindTxt.Text, nextIndex) < 0)
                        {
                            MessageBox.Show("置換が完了しました。", "置換");
                        }
                        else
                        {
                            oTextBox1.Select(oTextBox1.Text.IndexOf(textBoxFindTxt.Text, nextIndex, StringComparison.Ordinal), textBoxFindTxt.Text.Length);
                        }
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
                var oTabControl = (TabControl)Owner.Controls["tabControl1"];
                var oTabPage = oTabControl.SelectedTab.Controls["userControl1"].Controls["panel1"];
                var oTextBox1 = (TextBox)oTabPage.Controls["textBox1"];

                if (chkBox1.Checked != true)
                {
                    oTextBox1.Text = Regex.Replace(oTextBox1.Text, textBoxFindTxt.Text, textBoxReplaceTxt.Text, RegexOptions.IgnoreCase);
                }
                else
                {
                    oTextBox1.Text = Regex.Replace(oTextBox1.Text, textBoxFindTxt.Text, textBoxReplaceTxt.Text);
                }

                
            }
        }
        private void buttonCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        
        private void ReplacementForm_Load(object sender, EventArgs e)
        {
            this.ActiveControl = this.textBoxFindTxt;
        }
    }
}
