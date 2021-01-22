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
        //private DialogResult saveDialogResult;
        //private DialogResult msgDialogResult;
        //private string filePath;
        //private string fileName;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (Regex.IsMatch(this.Text, @"[\W\w]*\[編集中\]$"))
            {
                var msgDialogResult = MessageBox.Show("編集中のファイルを保存しますか？", "簡易メモ帳", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information);

                if (msgDialogResult == DialogResult.Yes)
                {
                    var ucBtnSaveAs = (Button)userControl1.Controls["menuFileSaveAs"];
                    ucBtnSaveAs.PerformClick();
                }
                else if (msgDialogResult == DialogResult.Cancel)
                {
                    e.Cancel = true;
                }
            }
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.Text = tabControl1.SelectedTab.Text;
        }

        //private void textBox1_TextChanged(object sender, EventArgs e)
        //{
        //    if (string.IsNullOrEmpty(fileName))
        //    {
        //        this.Text = $"簡易メモ帳-（無題）[編集中]";
        //    }
        //    else
        //    {
        //        this.Text = $"{fileName} -簡易メモ帳 [編集中]";
        //    }

        //}

        //private void menuEditReplace_Click(object sender, EventArgs e)
        //{
        //    var replaceForm = new ReplacementForm(this);            
        //    replaceForm.Show();
        //}

        //private void menuFileNew_Click(object sender, EventArgs e)
        //{
        //    if (Regex.IsMatch(this.Text, @"[\W\w]*\[編集中\]$"))
        //    {
        //        msgDialogResult = MessageBox.Show("編集中のファイルを保存しますか？", "簡易メモ帳", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information);

        //        if (msgDialogResult == DialogResult.Yes)
        //        {
        //            menuFileSaveAs.PerformClick();
        //        }
        //        else if (msgDialogResult == DialogResult.No)
        //        {
        //            textBox1.Clear();
        //            filePath = string.Empty;
        //            fileName = string.Empty;
        //            this.Text = "簡易メモ帳-（無題）";
        //        }
        //    }
        //    else
        //    {
        //        textBox1.Clear();
        //        filePath = string.Empty;
        //        fileName = string.Empty;
        //        this.Text = "簡易メモ帳-（無題）";
        //    }
        //}

        //private void menuFileOpen_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        if (Regex.IsMatch(this.Text, @"[\W\w]*\[編集中\]$"))
        //        {
        //            msgDialogResult = MessageBox.Show("編集中のファイルを保存しますか？", "簡易メモ帳", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information);

        //            if (msgDialogResult == DialogResult.Yes)
        //            {
        //                menuFileSaveAs.PerformClick();

        //                /*名前を付けて保存しなかった場合、openFileDialog1.ShowDialog()を実行しないために条件を設けている。*/
        //                if (saveDialogResult == DialogResult.OK)
        //                {
        //                    if (openFileDialog1.ShowDialog() == DialogResult.OK)
        //                    {
        //                        using (var reader = new StreamReader(openFileDialog1.FileName))
        //                        {
        //                            textBox1.Text = reader.ReadToEnd();
        //                        }

        //                        filePath = openFileDialog1.FileName;
        //                        fileName = Path.GetFileName(openFileDialog1.FileName);
        //                        this.Text = $"{fileName} -簡易メモ帳";
        //                    }
        //                }
        //                /*SaveAsせず、開いているファイルが上書き保存可能である場合、自動的に上書き保存する機能を入れてたいときはコメントアウトを外す*/
        //                //else if (!string.IsNullOrEmpty(filePath))
        //                //{
        //                //    using (var writer = new StreamWriter(filePath, false, Encoding.UTF8))
        //                //    {
        //                //        writer.Write(textBox1.Text);
        //                //    }

        //                //    this.Text = $"{fileName} -簡易メモ帳";
        //                //}
        //            }
        //            else if (msgDialogResult == DialogResult.No)
        //            {
        //                if (openFileDialog1.ShowDialog() == DialogResult.OK)
        //                {
        //                    using (var reader = new StreamReader(openFileDialog1.FileName))
        //                    {
        //                        textBox1.Text = reader.ReadToEnd();
        //                    }

        //                    filePath = openFileDialog1.FileName;
        //                    fileName = Path.GetFileName(openFileDialog1.FileName);
        //                    this.Text = $"{fileName} -簡易メモ帳";

        //                }
        //            }
        //        }
        //        else
        //        {
        //            if (openFileDialog1.ShowDialog() == DialogResult.OK)
        //            {
        //                using (var reader = new StreamReader(openFileDialog1.FileName))
        //                {
        //                    textBox1.Text = reader.ReadToEnd();
        //                }

        //                filePath = openFileDialog1.FileName;
        //                fileName = Path.GetFileName(openFileDialog1.FileName);
        //                this.Text = $"{fileName} -簡易メモ帳";
        //            }
        //        }
        //    }
        //    catch (EndOfStreamException)
        //    {
        //        throw;
        //    }
        //    catch (DirectoryNotFoundException)
        //    {
        //        throw;
        //    }
        //    catch (FileNotFoundException)
        //    {
        //        throw;
        //    }
        //    catch (IOException)
        //    {
        //        throw;
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}

        //private void menuFileSave_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        if (string.IsNullOrEmpty(filePath))
        //        {
        //            menuFileSaveAs.PerformClick();
        //        }
        //        else
        //        {
        //            using (var writer = new StreamWriter(filePath, false, Encoding.UTF8))
        //            {
        //                writer.Write(textBox1.Text);
        //            }

        //            this.Text = $"{fileName} -簡易メモ帳";
        //        }
        //    }
        //    catch (EndOfStreamException)
        //    {
        //        throw;
        //    }
        //    catch (DirectoryNotFoundException)
        //    {
        //        throw;
        //    }
        //    catch (FileNotFoundException)
        //    {
        //        throw;
        //    }
        //    catch (IOException)
        //    {
        //        throw;
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}

        //private void menuFileSaveAs_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        saveDialogResult = saveFileDialog1.ShowDialog();

        //        if (saveDialogResult == DialogResult.OK)
        //        {
        //            using (var writer = new StreamWriter(saveFileDialog1.FileName, false, Encoding.UTF8))
        //            {
        //                writer.Write(textBox1.Text);
        //            }

        //            filePath = saveFileDialog1.FileName;
        //            fileName = Path.GetFileName(saveFileDialog1.FileName);
        //            this.Text = $"{fileName} -簡易メモ帳";
        //        }
        //    }
        //    catch (EndOfStreamException)
        //    {
        //        throw;
        //    }
        //    catch (DirectoryNotFoundException)
        //    {
        //        throw;
        //    }
        //    catch (FileNotFoundException)
        //    {
        //        throw;
        //    }
        //    catch (IOException)
        //    {
        //        throw;
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}

        //private void btnSave_Click(object sender, EventArgs e)
        //{
        //    menuFileSave.PerformClick();
        //}

        //private void btnReplace_Click(object sender, EventArgs e)
        //{
        //    menuEditReplace.PerformClick();
        //}

        //private void Form1_Load(object sender, EventArgs e)
        //{
        //    string[] cmds = Environment.GetCommandLineArgs();

        //    try
        //    {
        //        if (cmds.Length > 1)
        //        {
        //            filePath = cmds[1].Trim(':');

        //            using (var reader = new StreamReader(filePath))
        //            {
        //                textBox1.Text = reader.ReadToEnd();
        //            }

        //            textBox1.SelectionStart = 0;
        //            fileName = Path.GetFileName(filePath);
        //            this.Text = $"{fileName} -簡易メモ帳";
        //        }
        //    }
        //    catch (EndOfStreamException)
        //    {
        //        throw;
        //    }
        //    catch (DirectoryNotFoundException)
        //    {
        //        throw;
        //    }
        //    catch (FileNotFoundException)
        //    {
        //        throw;
        //    }
        //    catch (IOException)
        //    {
        //        throw;
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}
    }
}
