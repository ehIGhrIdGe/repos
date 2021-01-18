using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Configuration;
using NLog;


namespace QuizSample
{
    public partial class FQuizList : Form
    {
        public FQuizList()
        {
            InitializeComponent();
            
            if (!ManagerDb.TryGetData(ManagerDb.TABLETYPE.QuizCategoryTable, out var dataTable))
            {
                MessageBox.Show("データが取得できませんでした。ログファイルを確認してください。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                //自動列生成の禁止
                dataGridView1.AutoGenerateColumns = false;
                //DataGridView に表示するデータテーブルの定義
                dataGridView1.DataSource = dataTable;
            }
        }

        /// <summary>
        /// 編集・削除ボタン
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 3)
            {
                new FQuizDetail(this, FQuizDetail.DETAILMODE.Edit, e.RowIndex).ShowDialog();
            }
            else if (e.ColumnIndex == 4)
            {
                if (!ManagerDb.TryDelteData(ManagerDb.TABLETYPE.QuizTable, e.RowIndex, out var dataTable))
                {
                    MessageBox.Show("データを削除できませんでした。ログファイルを確認してください。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    dataGridView1.DataSource = dataTable;
                    dataGridView1.CurrentCell = dataGridView1[0, e.RowIndex - 1];
                    MessageBox.Show("データを削除しました。", "削除", MessageBoxButtons.OK, MessageBoxIcon.Information);                    
                }
            }
        }
      
        /// <summary>
        /// フォームロード時
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FQuizList_Load(object sender, EventArgs e)
        {
            cbxCategory.Items.Add("");

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

        /// <summary>
        /// 選択カテゴリ変更
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbxCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable dataTable = new DataTable();

            if (cbxCategory.SelectedIndex > 0)
            {
                ManagerDb.TryGetWhereCategory(ManagerDb.TABLETYPE.QuizCategoryTableCaId, cbxCategory.SelectedIndex, out dataTable);
                dataGridView1.DataSource = dataTable;
            }
            else
            {
                ManagerDb.TryGetData(ManagerDb.TABLETYPE.QuizCategoryTable, out dataTable);
                dataGridView1.DataSource = dataTable;
            }
        }

        /// <summary>
        /// 新規登録
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnNew_Click(object sender, EventArgs e)
        {            
            new FQuizDetail(this ,FQuizDetail.DETAILMODE.New).ShowDialog();
        }

        /// <summary>
        /// 閉じる
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
