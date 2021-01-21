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
        private DataTable _DtQuiz;
        private DataTable _DtCategory;
        private DataTable _DtChoices;

        public FPlay(int selectedCategoryNum)
        {
            InitializeComponent();

            if (!ManagerDb.TryGetData(ManagerDb.TABLETYPE.QuizTable, out _DtQuiz) || !ManagerDb.TryGetData(ManagerDb.TABLETYPE.CategoryTable, out _DtCategory) || !ManagerDb.TryGetData(ManagerDb.TABLETYPE.ChoicesTable, out _DtChoices))
            {
                MessageBox.Show("データの読み込み失敗しました。ログファイルを確認してくだい。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                lbCategoryName.Text = _DtCategory.Select($"category_id = {selectedCategoryNum}")[0]["category_name"].ToString();
                lbQuestionNumber.Text = $"- 1 / {_DtQuiz.Select($"category_id = {selectedCategoryNum}").Count()}";
                tbxQuestion.Text = _DtQuiz.Select($"category_id = {selectedCategoryNum}")[0]["Question"].ToString();
                rb1.Text = _DtChoices.Select($"quiz_id = {_DtQuiz.Select($"category_id = {selectedCategoryNum}")[0]["quiz_id"]} AND choices_id = 1")[0]["disp_value"].ToString();
                rb2.Text = _DtChoices.Select($"quiz_id = {_DtQuiz.Select($"category_id = {selectedCategoryNum}")[0]["quiz_id"]} AND choices_id = 2")[0]["disp_value"].ToString();
                rb3.Text = _DtChoices.Select($"quiz_id = {_DtQuiz.Select($"category_id = {selectedCategoryNum}")[0]["quiz_id"]} AND choices_id = 3")[0]["disp_value"].ToString();
                rb4.Text = _DtChoices.Select($"quiz_id = {_DtQuiz.Select($"category_id = {selectedCategoryNum}")[0]["quiz_id"]} AND choices_id = 4")[0]["disp_value"].ToString();
            }
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

    }
}
