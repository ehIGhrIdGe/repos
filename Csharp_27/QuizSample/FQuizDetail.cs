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
        /// /// <param name="owner">親となるフォーム</param>
        /// <param name="inputMode">詳細フォームを開く際のモードを指定する。</param>
        /// <param name="selectedRow">一覧フォームで選択された行の行番号を指定する。モードがEditのときのみ必要になる。</param>        
        public FQuizDetail(FQuizList owner, DETAILMODE inputMode, int selectedRow = 0)
        {
            InitializeComponent();

            Owner = owner;
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

            if (!ManagerDb.TryGetData(ManagerDb.TABLETYPE.QuizCategoryChoicesTable, out var dtQuizList))
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

                        //クリックした問題の詳細を表示
                        cbxCategory.SelectedIndex = (int)dtQuizList.Select($"quiz_id = {SelectedRow + 1} AND choices_id = 1")[0]["category_id"] - 1;
                        lbQuizIdNum.Text = $"{dtQuizList.Select($"quiz_id = {SelectedRow + 1} AND choices_id = 1")[0]["quiz_id"]}";
                        txbQuestion.Text = $"{dtQuizList.Select($"quiz_id = {SelectedRow + 1} AND choices_id = 1")[0]["question"]}";
                        txbChoice1.Text = $"{dtQuizList.Select($"quiz_id = {SelectedRow + 1} AND choices_id = 1")[0]["disp_value"]}";
                        txbChoice2.Text = $"{dtQuizList.Select($"quiz_id = {SelectedRow + 1} AND choices_id = 2")[0]["disp_value"]}";
                        txbChoice3.Text = $"{dtQuizList.Select($"quiz_id = {SelectedRow + 1} AND choices_id = 3")[0]["disp_value"]}";
                        txbChoice4.Text = $"{dtQuizList.Select($"quiz_id = {SelectedRow + 1} AND choices_id = 4")[0]["disp_value"]}";
                        txbComment.Text = $"{dtQuizList.Select($"quiz_id = {SelectedRow + 1} AND choices_id = 1")[0]["comment"]}";

                        //正解の選択肢にチェックを入れる
                        var answer = dtQuizList.Select($"quiz_id = {SelectedRow + 1} AND choices_id = 1")[0]["answer"];
                        var rdbttnChoice = (RadioButton)groupBox1.Controls[$"rdbttnChoice{answer}"];
                        rdbttnChoice.Checked = true;
                        break;
                }
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (!ManagerDb.TryGetData(ManagerDb.TABLETYPE.QuizTable, out var dtQuiz))
            {
                MessageBox.Show("quiz テーブルが読み込めませんでした。ログファイルを確認してください。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (!ManagerDb.TryGetData(ManagerDb.TABLETYPE.ChoicesTable, out var dtChoices))
            {
                MessageBox.Show("choices テーブルが読み込めませんでした。ログファイルを確認してください。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                switch (Mode)
                {
                    case DETAILMODE.New:
                        if (!(rdbttnChoice1.Checked || rdbttnChoice2.Checked || rdbttnChoice3.Checked ||rdbttnChoice4.Checked))
                        {
                            MessageBox.Show("正解の選択肢にチェックを入れてください。", "更新", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            var nRowQuiz = dtQuiz.NewRow();
                            nRowQuiz[0] = dtQuiz.Rows.Count + 1;
                            nRowQuiz[1] = cbxCategory.SelectedIndex + 1;
                            nRowQuiz[2] = txbQuestion.Text;
                            nRowQuiz[3] = 1;
                            nRowQuiz[4] = string.IsNullOrWhiteSpace(txbComment.Text) ? null : txbComment.Text;
                            nRowQuiz[5] = "SUSER_NAME()";
                            nRowQuiz[6] = DateTime.Now;
                            dtQuiz.Rows.Add(nRowQuiz);
                            
                            if (!ManagerDb.TryInsertData(ManagerDb.TABLETYPE.QuizTable, nRowQuiz))
                            {
                                MessageBox.Show("クイズの新規登録ができませんでした。ログファイルを確認してください。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                            else
                            {
                                if (!ManagerDb.TryGetData(ManagerDb.TABLETYPE.QuizCategoryTable, out var tempTable))
                                {
                                    MessageBox.Show("データが取得できませんでした。ログファイルを確認してください。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    break;
                                }
                                else
                                {
                                    var oDataGrid = (DataGridView)Owner.Controls["dataGridView1"];
                                    oDataGrid.DataSource = tempTable;
                                }

                                var nRowChoices = dtChoices.NewRow();
                                for (var i = 1; i <= 4; i++)
                                {
                                    var tempTxb = (TextBox)groupBox1.Controls[$"txbChoice{i}"];
                                    nRowChoices[0] = dtQuiz.Rows.Count;
                                    nRowChoices[1] = i;
                                    nRowChoices[2] = tempTxb.Text;

                                    if(i == 1)
                                    {
                                        dtChoices.Rows.Add(nRowChoices);
                                    }                                    

                                    if (!ManagerDb.TryInsertData(ManagerDb.TABLETYPE.ChoicesTable, nRowChoices))
                                    {
                                        MessageBox.Show($"選択肢{i}番の新規登録ができませんでした。ログファイルを確認してください。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                        break;
                                    }
                                    else if (i >= 4)
                                    {
                                        MessageBox.Show("新規登録顔料しました。", "登録", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        this.Close();
                                    }
                                }
                            }                            
                        }
                        break;
                    case DETAILMODE.Edit:

                        break;
                }
            }
        }
    }
}
