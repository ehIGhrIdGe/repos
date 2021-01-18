
namespace QuizSample
{
    partial class FQuizDetail
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lbSubject = new System.Windows.Forms.Label();
            this.lbQuizId = new System.Windows.Forms.Label();
            this.lbQuizIdNum = new System.Windows.Forms.Label();
            this.lbCategory = new System.Windows.Forms.Label();
            this.cbxCategory = new System.Windows.Forms.ComboBox();
            this.lbQuestion = new System.Windows.Forms.Label();
            this.txbQuestion = new System.Windows.Forms.TextBox();
            this.lbChoices = new System.Windows.Forms.Label();
            this.rdbttnChoice1 = new System.Windows.Forms.RadioButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txbChoice4 = new System.Windows.Forms.TextBox();
            this.txbChoice3 = new System.Windows.Forms.TextBox();
            this.txbChoice2 = new System.Windows.Forms.TextBox();
            this.txbChoice1 = new System.Windows.Forms.TextBox();
            this.rdbttnChoice4 = new System.Windows.Forms.RadioButton();
            this.rdbttnChoice3 = new System.Windows.Forms.RadioButton();
            this.rdbttnChoice2 = new System.Windows.Forms.RadioButton();
            this.label6 = new System.Windows.Forms.Label();
            this.txbComment = new System.Windows.Forms.TextBox();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lbSubject
            // 
            this.lbSubject.AutoSize = true;
            this.lbSubject.Font = new System.Drawing.Font("Meiryo UI", 15F);
            this.lbSubject.Location = new System.Drawing.Point(243, 18);
            this.lbSubject.Name = "lbSubject";
            this.lbSubject.Size = new System.Drawing.Size(144, 38);
            this.lbSubject.TabIndex = 0;
            this.lbSubject.Text = "クイズ詳細";
            // 
            // lbQuizId
            // 
            this.lbQuizId.AutoSize = true;
            this.lbQuizId.Location = new System.Drawing.Point(62, 89);
            this.lbQuizId.Name = "lbQuizId";
            this.lbQuizId.Size = new System.Drawing.Size(70, 23);
            this.lbQuizId.TabIndex = 1;
            this.lbQuizId.Text = "クイズID";
            // 
            // lbQuizIdNum
            // 
            this.lbQuizIdNum.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbQuizIdNum.Location = new System.Drawing.Point(163, 88);
            this.lbQuizIdNum.Name = "lbQuizIdNum";
            this.lbQuizIdNum.Size = new System.Drawing.Size(100, 25);
            this.lbQuizIdNum.TabIndex = 2;
            this.lbQuizIdNum.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbCategory
            // 
            this.lbCategory.AutoSize = true;
            this.lbCategory.Location = new System.Drawing.Point(63, 145);
            this.lbCategory.Name = "lbCategory";
            this.lbCategory.Size = new System.Drawing.Size(62, 23);
            this.lbCategory.TabIndex = 3;
            this.lbCategory.Text = "カテゴリ";
            // 
            // cbxCategory
            // 
            this.cbxCategory.FormattingEnabled = true;
            this.cbxCategory.Location = new System.Drawing.Point(163, 142);
            this.cbxCategory.Name = "cbxCategory";
            this.cbxCategory.Size = new System.Drawing.Size(121, 31);
            this.cbxCategory.TabIndex = 4;
            // 
            // lbQuestion
            // 
            this.lbQuestion.AutoSize = true;
            this.lbQuestion.Location = new System.Drawing.Point(63, 220);
            this.lbQuestion.Name = "lbQuestion";
            this.lbQuestion.Size = new System.Drawing.Size(46, 23);
            this.lbQuestion.TabIndex = 5;
            this.lbQuestion.Text = "問題";
            // 
            // txbQuestion
            // 
            this.txbQuestion.AcceptsReturn = true;
            this.txbQuestion.Location = new System.Drawing.Point(163, 217);
            this.txbQuestion.Multiline = true;
            this.txbQuestion.Name = "txbQuestion";
            this.txbQuestion.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txbQuestion.Size = new System.Drawing.Size(375, 159);
            this.txbQuestion.TabIndex = 6;
            // 
            // lbChoices
            // 
            this.lbChoices.AutoSize = true;
            this.lbChoices.Location = new System.Drawing.Point(63, 411);
            this.lbChoices.Name = "lbChoices";
            this.lbChoices.Size = new System.Drawing.Size(197, 23);
            this.lbChoices.TabIndex = 7;
            this.lbChoices.Text = "選択肢（正解にチェック）";
            // 
            // rdbttnChoice1
            // 
            this.rdbttnChoice1.AutoSize = true;
            this.rdbttnChoice1.ImageAlign = System.Drawing.ContentAlignment.BottomLeft;
            this.rdbttnChoice1.Location = new System.Drawing.Point(6, 29);
            this.rdbttnChoice1.Name = "rdbttnChoice1";
            this.rdbttnChoice1.Size = new System.Drawing.Size(107, 27);
            this.rdbttnChoice1.TabIndex = 8;
            this.rdbttnChoice1.TabStop = true;
            this.rdbttnChoice1.Text = "選択肢１";
            this.rdbttnChoice1.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.groupBox1.Controls.Add(this.txbChoice4);
            this.groupBox1.Controls.Add(this.txbChoice3);
            this.groupBox1.Controls.Add(this.txbChoice2);
            this.groupBox1.Controls.Add(this.txbChoice1);
            this.groupBox1.Controls.Add(this.rdbttnChoice4);
            this.groupBox1.Controls.Add(this.rdbttnChoice3);
            this.groupBox1.Controls.Add(this.rdbttnChoice2);
            this.groupBox1.Controls.Add(this.rdbttnChoice1);
            this.groupBox1.Location = new System.Drawing.Point(40, 437);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(531, 198);
            this.groupBox1.TabIndex = 9;
            this.groupBox1.TabStop = false;
            // 
            // txbChoice4
            // 
            this.txbChoice4.Location = new System.Drawing.Point(123, 155);
            this.txbChoice4.Name = "txbChoice4";
            this.txbChoice4.Size = new System.Drawing.Size(375, 30);
            this.txbChoice4.TabIndex = 15;
            // 
            // txbChoice3
            // 
            this.txbChoice3.Location = new System.Drawing.Point(123, 114);
            this.txbChoice3.Name = "txbChoice3";
            this.txbChoice3.Size = new System.Drawing.Size(375, 30);
            this.txbChoice3.TabIndex = 14;
            // 
            // txbChoice2
            // 
            this.txbChoice2.Location = new System.Drawing.Point(123, 71);
            this.txbChoice2.Name = "txbChoice2";
            this.txbChoice2.Size = new System.Drawing.Size(375, 30);
            this.txbChoice2.TabIndex = 13;
            // 
            // txbChoice1
            // 
            this.txbChoice1.Location = new System.Drawing.Point(123, 29);
            this.txbChoice1.Name = "txbChoice1";
            this.txbChoice1.Size = new System.Drawing.Size(375, 30);
            this.txbChoice1.TabIndex = 12;
            // 
            // rdbttnChoice4
            // 
            this.rdbttnChoice4.AutoSize = true;
            this.rdbttnChoice4.Location = new System.Drawing.Point(6, 156);
            this.rdbttnChoice4.Name = "rdbttnChoice4";
            this.rdbttnChoice4.Size = new System.Drawing.Size(107, 27);
            this.rdbttnChoice4.TabIndex = 11;
            this.rdbttnChoice4.TabStop = true;
            this.rdbttnChoice4.Text = "選択肢４";
            this.rdbttnChoice4.UseVisualStyleBackColor = true;
            // 
            // rdbttnChoice3
            // 
            this.rdbttnChoice3.AutoSize = true;
            this.rdbttnChoice3.Location = new System.Drawing.Point(6, 114);
            this.rdbttnChoice3.Name = "rdbttnChoice3";
            this.rdbttnChoice3.Size = new System.Drawing.Size(107, 27);
            this.rdbttnChoice3.TabIndex = 10;
            this.rdbttnChoice3.TabStop = true;
            this.rdbttnChoice3.Text = "選択肢３";
            this.rdbttnChoice3.UseVisualStyleBackColor = true;
            // 
            // rdbttnChoice2
            // 
            this.rdbttnChoice2.AutoSize = true;
            this.rdbttnChoice2.Location = new System.Drawing.Point(6, 71);
            this.rdbttnChoice2.Name = "rdbttnChoice2";
            this.rdbttnChoice2.Size = new System.Drawing.Size(107, 27);
            this.rdbttnChoice2.TabIndex = 9;
            this.rdbttnChoice2.TabStop = true;
            this.rdbttnChoice2.Text = "選択肢２";
            this.rdbttnChoice2.UseVisualStyleBackColor = true;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(63, 666);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(60, 23);
            this.label6.TabIndex = 10;
            this.label6.Text = "コメント";
            // 
            // txbComment
            // 
            this.txbComment.AcceptsReturn = true;
            this.txbComment.Location = new System.Drawing.Point(163, 663);
            this.txbComment.Multiline = true;
            this.txbComment.Name = "txbComment";
            this.txbComment.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txbComment.Size = new System.Drawing.Size(375, 102);
            this.txbComment.TabIndex = 11;
            // 
            // btnUpdate
            // 
            this.btnUpdate.Location = new System.Drawing.Point(408, 791);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(130, 30);
            this.btnUpdate.TabIndex = 13;
            this.btnUpdate.Text = "更新";
            this.btnUpdate.UseVisualStyleBackColor = true;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(257, 791);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(130, 30);
            this.btnClose.TabIndex = 14;
            this.btnClose.Text = "閉じる";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // FQuizDetail
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 23F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(607, 841);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnUpdate);
            this.Controls.Add(this.txbComment);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.lbChoices);
            this.Controls.Add(this.txbQuestion);
            this.Controls.Add(this.lbQuestion);
            this.Controls.Add(this.cbxCategory);
            this.Controls.Add(this.lbCategory);
            this.Controls.Add(this.lbQuizIdNum);
            this.Controls.Add(this.lbQuizId);
            this.Controls.Add(this.lbSubject);
            this.Font = new System.Drawing.Font("Meiryo UI", 9F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "FQuizDetail";
            this.Text = "クイズ詳細";
            this.Load += new System.EventHandler(this.FQuizDetail_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbSubject;
        private System.Windows.Forms.Label lbQuizId;
        private System.Windows.Forms.Label lbQuizIdNum;
        private System.Windows.Forms.Label lbCategory;
        private System.Windows.Forms.ComboBox cbxCategory;
        private System.Windows.Forms.Label lbQuestion;
        private System.Windows.Forms.TextBox txbQuestion;
        private System.Windows.Forms.Label lbChoices;
        private System.Windows.Forms.RadioButton rdbttnChoice1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txbChoice4;
        private System.Windows.Forms.TextBox txbChoice3;
        private System.Windows.Forms.TextBox txbChoice2;
        private System.Windows.Forms.TextBox txbChoice1;
        private System.Windows.Forms.RadioButton rdbttnChoice4;
        private System.Windows.Forms.RadioButton rdbttnChoice3;
        private System.Windows.Forms.RadioButton rdbttnChoice2;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txbComment;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.Button btnClose;
    }
}