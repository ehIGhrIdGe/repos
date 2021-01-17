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
    public partial class FQuizDetail : Form
    {
        public enum DETAILMODE
        {
            New, Edit
        }

        private DETAILMODE Mode { get; set; }
        private int SelectedRow { get; set; }

        /// <summary>
        /// クイズ詳細フォーム
        /// </summary>
        /// <param name="inputMode">詳細フォームを開く際のモードを指定する。</param>
        /// <param name="inputDt">詳細フォームで編集したいデータテーブルを指定する。</param>
        /// <param name="selectedRow">一覧フォームで選択された行の行番号を指定する。モードがEditのときのみ必要になる。</param>
        public FQuizDetail(DETAILMODE inputMode, int selectedRow = 0)
        {
            InitializeComponent();

            Mode = inputMode;
            SelectedRow = selectedRow;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FQuizDetail_Load(object sender, EventArgs e)
        {
            //categoryテーブル、quizテーブルのデータを取得し、データテーブルにセットする。
            if (!ManagerDb.TryGetData(ManagerDb.TABLETYPE.CategoryTable, out var dtCategory))
            {
                MessageBox.Show("カテゴリが読み込めませんでした。ログファイルを確認してください。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                //comboBoxCategoryのアイテムの設定
                for (var i = 0; i < dtCategory.Rows.Count; i++)
                {
                    cbxCategory.Items.Add(dtCategory.Rows[i]["category_name"]);
                }

                cbxCategory.SelectedIndex = 0;
            }

            if(!ManagerDb.TryGetData(ManagerDb.TABLETYPE.QuizCategoryChoicesTable, out var dtQuizList))
            {
                MessageBox.Show("データが読み込めませんでした。ログファイルを確認してください。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                switch (Mode)
                {
                    case DETAILMODE.New:
                        //クエリで選択肢まで含めた結果を取得しているため、その総数を選択肢の数で割ることで現在の問題数を求めている
                        lbQuizIdNum.Text = $"{dtQuizList.Rows.Count / 4 + 1}";
                        break;
                    case DETAILMODE.Edit:
                        //EnumerableRowCollection<DataRow> dataRows = dtQuizList.AsEnumerable();
                        

                        lbQuizIdNum.Text = $"{dtQuizList.Select($"quiz_id = {SelectedRow + 1} AND choices_id = 1")[0]["quiz_id"]}";
                        //cbxCategory.SelectedIndex = (int)dtQuizList.Rows[SelectedRow]["category_id"];
                        //txbQuestion.Text = $"{dtQuizList.Rows[SelectedRow]["qu"]}";
                        
                        break;
                }
            }
        }
    }
}
