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
    public partial class FPlay : Form
    {
        private DataTable _dtQuiz;
        private DataTable _dtCategory;
        private DataTable _dtChoices;
        private int _nowQuestionIndex;
        private int _selectedCategoryNum;
        private int _sumCorrectAnswer;

        public FPlay(int selectedCategoryNum)
        {
            InitializeComponent();

            _selectedCategoryNum = selectedCategoryNum;
        }

        /// <summary>
        /// 問題を読み込む
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FPlay_Shown(object sender, EventArgs e)
        {
            if (!ManagerDb.TryGetWhere(ManagerDb.TABLETYPE.QuizTable, ManagerDb.WHERETYPE.CategoryId, _selectedCategoryNum, out _dtQuiz) || !ManagerDb.TryGetWhere(ManagerDb.TABLETYPE.CategoryTable, ManagerDb.WHERETYPE.CategoryId, _selectedCategoryNum, out _dtCategory) || !ManagerDb.TryGetData(ManagerDb.TABLETYPE.ChoicesTable, out _dtChoices))
            {
                MessageBox.Show("データの読み込み失敗しました。ログファイルを確認してくだい。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (_dtQuiz.Rows.Count == 0)
            {
                this.Visible = false;
                btnStop.PerformClick();
                MessageBox.Show("そのカテゴリーには問題が登録されていません。問題を登録して遊んでね。", "遊ぶ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                _nowQuestionIndex = 0;
                lbCategoryName.Text = _dtCategory.Select($"category_id = {_dtQuiz.Rows[_nowQuestionIndex]["category_id"]}")[0]["category_name"].ToString();
                lbQuestionNumber.Text = $"- {_nowQuestionIndex + 1} / {_dtQuiz.Rows.Count}";
                tbxQuestion.Text = _dtQuiz.Rows[_nowQuestionIndex]["Question"].ToString();
                rb1.Text = _dtChoices.Select($"quiz_id = {_dtQuiz.Rows[_nowQuestionIndex]["quiz_id"]} AND choices_id = 1")[0]["disp_value"].ToString();
                rb2.Text = _dtChoices.Select($"quiz_id = {_dtQuiz.Rows[_nowQuestionIndex]["quiz_id"]} AND choices_id = 2")[0]["disp_value"].ToString();
                rb3.Text = _dtChoices.Select($"quiz_id = {_dtQuiz.Rows[_nowQuestionIndex]["quiz_id"]} AND choices_id = 3")[0]["disp_value"].ToString();
                rb4.Text = _dtChoices.Select($"quiz_id = {_dtQuiz.Rows[_nowQuestionIndex]["quiz_id"]} AND choices_id = 4")[0]["disp_value"].ToString();
            }
        }

        private void btnAnswer_Click(object sender, EventArgs e)
        {
            if (!(rb1.Checked || rb2.Checked || rb3.Checked || rb4.Checked))
            {
                MessageBox.Show("回答を選択してください。", "回答", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                //選択されている選択肢の番号を取得
                var selectedAnswerNum = 0;
                foreach (RadioButton rdo in groupBox1.Controls.OfType<RadioButton>())
                {
                    if (rdo.Checked)
                    {
                        var tempStr = rdo.Name.Substring(rdo.Name.Length - 1, 1);
                        selectedAnswerNum = int.Parse(tempStr);
                        break;
                    }
                }

                if ((int)_dtQuiz.Rows[_nowQuestionIndex]["answer"] != selectedAnswerNum)
                {
                    MessageBox.Show("不正解", "判定", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("正解", "判定", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    _sumCorrectAnswer += 1;
                }

                //次の問題をセットする。最後の問題であれば、正解数を伝えて、フォームを閉じる。
                if (_nowQuestionIndex < _dtQuiz.Rows.Count - 1)
                {
                    _nowQuestionIndex += 1;
                    lbCategoryName.Text = _dtCategory.Select($"category_id = {_dtQuiz.Rows[_nowQuestionIndex]["category_id"]}")[0]["category_name"].ToString();
                    lbQuestionNumber.Text = $"- {_nowQuestionIndex + 1} / {_dtQuiz.Rows.Count}";
                    tbxQuestion.Text = _dtQuiz.Rows[_nowQuestionIndex]["Question"].ToString();
                    rb1.Text = _dtChoices.Select($"quiz_id = {_dtQuiz.Rows[_nowQuestionIndex]["quiz_id"]} AND choices_id = 1")[0]["disp_value"].ToString();
                    rb2.Text = _dtChoices.Select($"quiz_id = {_dtQuiz.Rows[_nowQuestionIndex]["quiz_id"]} AND choices_id = 2")[0]["disp_value"].ToString();
                    rb3.Text = _dtChoices.Select($"quiz_id = {_dtQuiz.Rows[_nowQuestionIndex]["quiz_id"]} AND choices_id = 3")[0]["disp_value"].ToString();
                    rb4.Text = _dtChoices.Select($"quiz_id = {_dtQuiz.Rows[_nowQuestionIndex]["quiz_id"]} AND choices_id = 4")[0]["disp_value"].ToString();
                }
                else
                {
                    MessageBox.Show($"正解数は{_sumCorrectAnswer}です。", "正解数", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    btnStop.PerformClick();
                }
            }
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
