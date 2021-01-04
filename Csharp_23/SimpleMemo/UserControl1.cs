using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.IO;

namespace SimpleMemo
{
    public partial class UserControl1 : UserControl
    {
        private DialogResult saveDialogResult;
        private DialogResult msgDialogResult;
        private string filePath;
        private string fileName;

        public UserControl1()
        {
            InitializeComponent();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            menuFileSave.PerformClick();
        }

        private void btnReplace_Click(object sender, EventArgs e)
        {
            menuEditReplace.PerformClick();
        }

        private void menuFileNew_Click(object sender, EventArgs e)
        {
            var pForm = (Form1)this.ParentForm;
            var pTabControl = (TabControl)pForm.Controls["tabControl1"];

            if (Regex.IsMatch(pForm.Text, @"[\W\w]*\[編集中\]$"))
            {
                msgDialogResult = MessageBox.Show("編集中のファイルを保存しますか？", "簡易メモ帳", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information);

                if (msgDialogResult == DialogResult.Yes)
                {
                    menuFileSaveAs.PerformClick();
                }
                else if (msgDialogResult == DialogResult.No)
                {
                    textBox1.Clear();
                    filePath = string.Empty;
                    fileName = string.Empty;
                    pTabControl.SelectedTab.Text = "簡易メモ帳-（無題）";
                    pForm.Text = pTabControl.SelectedTab.Text;
                }
            }
            else
            {
                textBox1.Clear();
                filePath = string.Empty;
                fileName = string.Empty;
                pTabControl.SelectedTab.Text = "簡易メモ帳-（無題）";
                pForm.Text = pTabControl.SelectedTab.Text;
            }
        }

        private void menuFileOpen_Click(object sender, EventArgs e)
        {
            var pForm = (Form1)this.ParentForm;
            var pTabControl = (TabControl)pForm.Controls["tabControl1"];

            try
            {
                if (Regex.IsMatch(pForm.Text, @"[\W\w]*\[編集中\]$"))
                {
                    msgDialogResult = MessageBox.Show("編集中のファイルを保存しますか？", "簡易メモ帳", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information);

                    if (msgDialogResult == DialogResult.Yes)
                    {
                        menuFileSaveAs.PerformClick();

                        /*名前を付けて保存しなかった場合、openFileDialog1.ShowDialog()を実行しないために条件を設けている。*/
                        if (saveDialogResult == DialogResult.OK)
                        {
                            if (openFileDialog1.ShowDialog() == DialogResult.OK)
                            {
                                using (var reader = new StreamReader(openFileDialog1.FileName))
                                {
                                    textBox1.Text = reader.ReadToEnd();
                                }

                                filePath = openFileDialog1.FileName;
                                fileName = Path.GetFileName(openFileDialog1.FileName);
                                pTabControl.SelectedTab.Text = $"{fileName} -簡易メモ帳";
                                pForm.Text = pTabControl.SelectedTab.Text;
                            }
                        }
                        /*SaveAsせず、開いているファイルが上書き保存可能である場合、自動的に上書き保存する機能を入れてたいときはコメントアウトを外す*/
                        //else if (!string.IsNullOrEmpty(filePath))
                        //{
                        //    using (var writer = new StreamWriter(filePath, false, Encoding.UTF8))
                        //    {
                        //        writer.Write(textBox1.Text);
                        //    }

                        //    this.Text = $"{fileName} -簡易メモ帳";
                        //}
                    }
                    else if (msgDialogResult == DialogResult.No)
                    {
                        if (openFileDialog1.ShowDialog() == DialogResult.OK)
                        {
                            using (var reader = new StreamReader(openFileDialog1.FileName))
                            {
                                textBox1.Text = reader.ReadToEnd();
                            }

                            filePath = openFileDialog1.FileName;
                            fileName = Path.GetFileName(openFileDialog1.FileName);
                            pTabControl.SelectedTab.Text = $"{fileName} -簡易メモ帳";
                            pForm.Text = pTabControl.SelectedTab.Text;
                        }
                    }
                }
                else
                {
                    if (openFileDialog1.ShowDialog() == DialogResult.OK)
                    {
                        using (var reader = new StreamReader(openFileDialog1.FileName))
                        {
                            textBox1.Text = reader.ReadToEnd();
                        }

                        filePath = openFileDialog1.FileName;
                        fileName = Path.GetFileName(openFileDialog1.FileName);
                        pTabControl.SelectedTab.Text = $"{fileName} -簡易メモ帳";
                        pForm.Text = pTabControl.SelectedTab.Text;
                    }
                }
            }
            catch (EndOfStreamException)
            {
                throw;
            }
            catch (DirectoryNotFoundException)
            {
                throw;
            }
            catch (FileNotFoundException)
            {
                throw;
            }
            catch (IOException)
            {
                throw;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void menuFileSave_Click(object sender, EventArgs e)
        {
            var pForm = (Form1)this.ParentForm;
            var pTabControl = (TabControl)pForm.Controls["tabControl1"];

            try
            {
                if (string.IsNullOrEmpty(filePath))
                {
                    menuFileSaveAs.PerformClick();
                }
                else
                {
                    using (var writer = new StreamWriter(filePath, false, Encoding.UTF8))
                    {
                        writer.Write(textBox1.Text);
                    }

                    pTabControl.SelectedTab.Text = $"{fileName} -簡易メモ帳";
                    pForm.Text = pTabControl.SelectedTab.Text;
                }
            }
            catch (EndOfStreamException)
            {
                throw;
            }
            catch (DirectoryNotFoundException)
            {
                throw;
            }
            catch (FileNotFoundException)
            {
                throw;
            }
            catch (IOException)
            {
                throw;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void menuFileSaveAs_Click(object sender, EventArgs e)
        {
            var pForm = (Form1)this.ParentForm;
            var pTabControl = (TabControl)pForm.Controls["tabControl1"];

            try
            {
                saveDialogResult = saveFileDialog1.ShowDialog();

                if (saveDialogResult == DialogResult.OK)
                {
                    using (var writer = new StreamWriter(saveFileDialog1.FileName, false, Encoding.UTF8))
                    {
                        writer.Write(textBox1.Text);
                    }

                    filePath = saveFileDialog1.FileName;
                    fileName = Path.GetFileName(saveFileDialog1.FileName);
                    pTabControl.SelectedTab.Text = $"{fileName} -簡易メモ帳";
                    pForm.Text = pTabControl.SelectedTab.Text;
                }
            }
            catch (EndOfStreamException)
            {
                throw;
            }
            catch (DirectoryNotFoundException)
            {
                throw;
            }
            catch (FileNotFoundException)
            {
                throw;
            }
            catch (IOException)
            {
                throw;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void menuEditReplace_Click(object sender, EventArgs e)
        {
            //ParentForm で取得したフォームは暗黙的に Form1 型へ変換できないため、明示敵にキャストする必要がある。
            var pForm = (Form1)this.ParentForm;            
            var replaceForm = new ReplacementForm(pForm);
            replaceForm.Show();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            var pForm = (Form1)this.ParentForm;
            var pTabControl = (TabControl)pForm.Controls["tabControl1"];

            if (string.IsNullOrEmpty(fileName))
            {
                pTabControl.SelectedTab.Text = $"簡易メモ帳-（無題）[編集中]";
                pForm.Text = pTabControl.SelectedTab.Text;                
            }
            else
            {
                pTabControl.SelectedTab.Text = $"{fileName} -簡易メモ帳 [編集中]";
                pForm.Text = pTabControl.SelectedTab.Text;
            }
        }

        private void menuFileNewTab_Click(object sender, EventArgs e)
        {
            var pForm = (Form1)this.ParentForm;            
            var pTabControl = (TabControl)pForm.Controls["tabControl1"];
            
            pTabControl.TabPages.Add(new TabPage("簡易メモ帳-（無題）"));
            pTabControl.SelectedIndex = pTabControl.TabCount - 1;

            pTabControl.SelectedTab.Controls.Add(new UserControl1());
            pTabControl.SelectedTab.Controls["UserControl1"].Anchor = AnchorStyles.Top| AnchorStyles.Bottom| AnchorStyles.Right| AnchorStyles.Left;
            pTabControl.SelectedTab.Controls["UserControl1"].Size = new Size(468, 375);
            pTabControl.SelectedTab.Controls["UserControl1"].Location = new Point(-4, 0);
        }
    }
}
