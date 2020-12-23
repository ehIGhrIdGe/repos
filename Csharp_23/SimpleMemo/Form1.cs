using System;
using System.IO;
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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }



        private void btnNew_Click(object sender, EventArgs e)
        {
            if (Regex.IsMatch(ActiveForm.Text, @"[\W\w]*\[編集中\]$"))
            {
                DialogResultInfo.MsgDialogResult = MessageBox.Show("編集中のファイルを保存しますか？", "簡易メモ帳", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information);

                if (DialogResultInfo.MsgDialogResult == DialogResult.Yes)
                {
                    btnSaveAs.PerformClick();
                }
                else if (DialogResultInfo.MsgDialogResult == DialogResult.No)
                {
                    textBox1.Clear();
                    FileInfo.FilePath = string.Empty;
                    FileInfo.FileName = string.Empty;
                    ActiveForm.Text = "簡易メモ帳-（無題）";
                }
            }
            else
            {
                textBox1.Clear();
                FileInfo.FilePath = string.Empty;
                FileInfo.FileName = string.Empty;
                ActiveForm.Text = "簡易メモ帳-（無題）";
            }
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            try
            {
                var x = ActiveForm.Text;
                if (Regex.IsMatch(ActiveForm.Text, @"[\W\w]*\[編集中\]$"))
                {
                    DialogResultInfo.MsgDialogResult = MessageBox.Show("編集中のファイルを保存しますか？", "簡易メモ帳", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information);

                    if (DialogResultInfo.MsgDialogResult == DialogResult.Yes)
                    {
                        btnSaveAs.PerformClick();

                        /*名前を付けて保存しなかった場合、openFileDialog1.ShowDialog()を実行しないために条件を設けている。*/
                        if (DialogResultInfo.SaveDialogResult == DialogResult.Yes)
                        {
                            if (openFileDialog1.ShowDialog() == DialogResult.OK)
                            {
                                using (var reader = new StreamReader(openFileDialog1.FileName))
                                {
                                    textBox1.Text = reader.ReadToEnd();
                                }

                                FileInfo.FilePath = openFileDialog1.FileName;
                                FileInfo.FileName = Path.GetFileName(openFileDialog1.FileName);
                                ActiveForm.Text = $"{FileInfo.FileName} -簡易メモ帳";
                            }
                        }
                        /*SaveAsせず、開いているファイルが上書き保存可能である場合、自動的に上書き保存する機能を入れてたいときはコメントアウトを外す*/
                        //else if (!string.IsNullOrEmpty(FileInfo.FilePath))
                        //{
                        //    using (var writer = new StreamWriter(FileInfo.FilePath, false, Encoding.UTF8))
                        //    {
                        //        writer.Write(textBox1.Text);
                        //    }

                        //    ActiveForm.Text = $"{FileInfo.FileName} -簡易メモ帳";
                        //}
                    }
                    else if (DialogResultInfo.MsgDialogResult == DialogResult.No)
                    {
                        if (openFileDialog1.ShowDialog() == DialogResult.OK)
                        {
                            using (var reader = new StreamReader(openFileDialog1.FileName))
                            {
                                textBox1.Text = reader.ReadToEnd();
                            }

                            FileInfo.FilePath = openFileDialog1.FileName;
                            FileInfo.FileName = Path.GetFileName(openFileDialog1.FileName);
                            ActiveForm.Text = $"{FileInfo.FileName} -簡易メモ帳";

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

                        FileInfo.FilePath = openFileDialog1.FileName;
                        FileInfo.FileName = Path.GetFileName(openFileDialog1.FileName);
                        ActiveForm.Text = $"{FileInfo.FileName} -簡易メモ帳";
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

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(FileInfo.FilePath))
                {
                    btnSaveAs.PerformClick();
                }
                else
                {
                    using (var writer = new StreamWriter(FileInfo.FilePath, false, Encoding.UTF8))
                    {
                        writer.Write(textBox1.Text);
                    }

                    ActiveForm.Text = $"{FileInfo.FileName} -簡易メモ帳";
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

        private void btnSaveAs_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResultInfo.SaveDialogResult = saveFileDialog1.ShowDialog();

                if (DialogResultInfo.SaveDialogResult == DialogResult.OK)
                {
                    using (var writer = new StreamWriter(saveFileDialog1.FileName, false, Encoding.UTF8))
                    {
                        writer.Write(textBox1.Text);
                    }

                    FileInfo.FilePath = saveFileDialog1.FileName;
                    FileInfo.FileName = Path.GetFileName(saveFileDialog1.FileName);
                    ActiveForm.Text = $"{FileInfo.FileName} -簡易メモ帳";
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

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(FileInfo.FileName))
            {
                ActiveForm.Text = $"簡易メモ帳-（無題）[編集中]";
            }
            else
            {
                ActiveForm.Text = $"{FileInfo.FileName} -簡易メモ帳 [編集中]";
            }

        }
    }
}
