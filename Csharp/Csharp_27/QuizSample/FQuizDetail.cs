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
        private int SelectedQuizId { get; set; }

        /// <summary>
        /// クイズ詳細フォーム
        /// </summary>
        /// /// <param name="owner">親となるフォーム</param>
        /// <param name="inputMode">詳細フォームを開く際のモードを指定する。</param>
        /// <param name="selectedQuizId">一覧フォームで選択したクイズの quiz_id。モードがEditのときのみ必要になる。</param>        
        public FQuizDetail(FQuizList owner, DETAILMODE inputMode, int selectedQuizId = 0)
        {
            InitializeComponent();

            Owner = owner;
            Mode = inputMode;
            SelectedQuizId = selectedQuizId;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FQuizDetail_Load(object sender, EventArgs e)
        {
            if (!ManagerDb.TryGetData(ManagerDb.TABLETYPE.ChoicesTable, out var dtChoices) || !ManagerDb.TryGetData(ManagerDb.TABLETYPE.QuizTable, out var dtQuiz) || !ManagerDb.TryGetData(ManagerDb.TABLETYPE.CategoryTable, out var dtCategory))
            {
                MessageBox.Show("データが読み込めませんでした。ログファイルを確認してください。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                //comboBoxCategoryのアイテムの設定
                for (var i = 0; i < dtCategory.Rows.Count; i++)
                {
                    cbxCategory.Items.Add(dtCategory.Rows[i]["category_name"]);
                }

                cbxCategory.SelectedIndex = 0;

                //新規か編集化で表示を変更
                switch (Mode)
                {
                    case DETAILMODE.New:
                        //新規で登録する番号は常に最新
                        lbQuizIdNum.Text = $"{(int)dtQuiz.Rows[dtQuiz.Rows.Count -1][0] + 1}";
                        break;
                    case DETAILMODE.Edit:
                        //EnumerableRowCollection<DataRow> dataRows = dtQuizList.AsEnumerable();

                        //クリックした問題の詳細を表示
                        cbxCategory.SelectedIndex = (int)dtQuiz.Select($"quiz_id = {SelectedQuizId}")[0]["category_id"] - 1;
                        lbQuizIdNum.Text = $"{SelectedQuizId}";
                        txbQuestion.Text = $"{dtQuiz.Select($"quiz_id = {SelectedQuizId}")[0]["question"]}";
                        txbChoice1.Text = $"{dtChoices.Select($"quiz_id = {SelectedQuizId}")[0]["disp_value"]}";
                        txbChoice2.Text = $"{dtChoices.Select($"quiz_id = {SelectedQuizId}")[1]["disp_value"]}";
                        txbChoice3.Text = $"{dtChoices.Select($"quiz_id = {SelectedQuizId}")[2]["disp_value"]}";
                        txbChoice4.Text = $"{dtChoices.Select($"quiz_id = {SelectedQuizId}")[3]["disp_value"]}";
                        txbComment.Text = $"{dtQuiz.Select($"quiz_id = {SelectedQuizId}")[0]["comment"]}";

                        //正解の選択肢にチェックを入れる
                        var answer = dtQuiz.Select($"quiz_id = {SelectedQuizId}")[0]["answer"];
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
                if (!(rdbttnChoice1.Checked || rdbttnChoice2.Checked || rdbttnChoice3.Checked || rdbttnChoice4.Checked))
                {
                    MessageBox.Show("正解の選択肢にチェックを入れてください。", "更新", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    //選択されている選択肢の番号を取得
                    var selectedAnswerNum =0;
                    foreach (RadioButton rdo in groupBox1.Controls.OfType<RadioButton>())
                    {
                        if (rdo.Checked)
                        {
                            var tempStr = rdo.Name.Substring(rdo.Name.Length - 1, 1);
                            selectedAnswerNum = int.Parse(tempStr);
                            break;
                        }
                    }

                    switch (Mode)
                    {
                        case DETAILMODE.New:
                            var nRowQuiz = dtQuiz.NewRow();
                            nRowQuiz[0] = (int)dtQuiz.Rows[dtQuiz.Rows.Count - 1][0] + 1;
                            nRowQuiz[1] = cbxCategory.SelectedIndex + 1;
                            nRowQuiz[2] = txbQuestion.Text;
                            nRowQuiz[3] = selectedAnswerNum;
                            nRowQuiz[4] = string.IsNullOrWhiteSpace(txbComment.Text) ? null : txbComment.Text;
                            nRowQuiz[5] = $@"{Environment.MachineName}\{Environment.UserName}";
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
                                    nRowChoices[0] = (int)dtQuiz.Rows[dtQuiz.Rows.Count - 1][0];
                                    nRowChoices[1] = i;
                                    nRowChoices[2] = tempTxb.Text;

                                    if (i == 1)
                                    {
                                        dtChoices.Rows.Add(nRowChoices);
                                    }

                                    if (!ManagerDb.TryInsertData(ManagerDb.TABLETYPE.ChoicesTable, nRowChoices))
                                    {
                                        ManagerDb.TryDelteData(ManagerDb.TABLETYPE.QuizTable, (int)dtQuiz.Rows[dtQuiz.Rows.Count - 1][0]);
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
                            break;
                        case DETAILMODE.Edit:
                            dtQuiz.Select($"quiz_id = {SelectedQuizId}")[0][1] = cbxCategory.SelectedIndex + 1;
                            dtQuiz.Select($"quiz_id = {SelectedQuizId}")[0][2] = txbQuestion.Text;
                            dtQuiz.Select($"quiz_id = {SelectedQuizId}")[0][3] = selectedAnswerNum;
                            dtQuiz.Select($"quiz_id = {SelectedQuizId}")[0][4] = string.IsNullOrWhiteSpace(txbComment.Text) ? null : txbComment.Text;
                            dtQuiz.Select($"quiz_id = {SelectedQuizId}")[0][5] = $@"{Environment.MachineName}\{Environment.UserName}";
                            dtQuiz.Select($"quiz_id = {SelectedQuizId}")[0][6] = DateTime.Now;
                            dtChoices.Select($"quiz_id = {SelectedQuizId} AND choices_id = 1")[0][2] = txbChoice1.Text;
                            dtChoices.Select($"quiz_id = {SelectedQuizId} AND choices_id = 2")[0][2] = txbChoice2.Text;
                            dtChoices.Select($"quiz_id = {SelectedQuizId} AND choices_id = 3")[0][2] = txbChoice3.Text;
                            dtChoices.Select($"quiz_id = {SelectedQuizId} AND choices_id = 4")[0][2] = txbChoice4.Text;

                            if (!ManagerDb.TryUpdateData(ManagerDb.TABLETYPE.QuizTable, dtQuiz))
                            {
                                MessageBox.Show("quizテーブルの更新ができませんでした。ログファイルを確認してください。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                break;
                            }
                            else if(!ManagerDb.TryUpdateData(ManagerDb.TABLETYPE.ChoicesTable, dtChoices))
                            {
                                MessageBox.Show("choicesテーブルの更新ができませんでした。ログファイルを確認してください。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                break;
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
                                    MessageBox.Show("更新が完了しました。", "更新", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    this.Close();
                                }
                            }
                            break;
                    }
                }

            }
        }
    }
}
