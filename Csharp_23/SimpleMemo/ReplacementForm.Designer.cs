
namespace SimpleMemo
{
    partial class ReplacementForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxFindTxt = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.buttonAllRepleace = new System.Windows.Forms.Button();
            this.buttonReplace = new System.Windows.Forms.Button();
            this.buttonNextFind = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.textBoxReplaceTxt = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.chkBox1 = new System.Windows.Forms.CheckBox();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label1.Location = new System.Drawing.Point(-3, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(102, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "検索する文字列(&N):";
            // 
            // textBoxFindTxt
            // 
            this.textBoxFindTxt.Location = new System.Drawing.Point(105, 3);
            this.textBoxFindTxt.Name = "textBoxFindTxt";
            this.textBoxFindTxt.Size = new System.Drawing.Size(230, 19);
            this.textBoxFindTxt.TabIndex = 1;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.buttonCancel);
            this.panel1.Controls.Add(this.buttonAllRepleace);
            this.panel1.Controls.Add(this.buttonReplace);
            this.panel1.Controls.Add(this.buttonNextFind);
            this.panel1.Location = new System.Drawing.Point(377, 6);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(105, 120);
            this.panel1.TabIndex = 2;
            // 
            // buttonCancel
            // 
            this.buttonCancel.Location = new System.Drawing.Point(0, 93);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(100, 25);
            this.buttonCancel.TabIndex = 3;
            this.buttonCancel.Text = "キャンセル(&Q)";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // buttonAllRepleace
            // 
            this.buttonAllRepleace.Location = new System.Drawing.Point(0, 62);
            this.buttonAllRepleace.Name = "buttonAllRepleace";
            this.buttonAllRepleace.Size = new System.Drawing.Size(100, 25);
            this.buttonAllRepleace.TabIndex = 2;
            this.buttonAllRepleace.Text = "すべて置換(&A)";
            this.buttonAllRepleace.UseVisualStyleBackColor = true;
            this.buttonAllRepleace.Click += new System.EventHandler(this.buttonAllRepleace_Click);
            // 
            // buttonReplace
            // 
            this.buttonReplace.Location = new System.Drawing.Point(0, 31);
            this.buttonReplace.Name = "buttonReplace";
            this.buttonReplace.Size = new System.Drawing.Size(100, 25);
            this.buttonReplace.TabIndex = 1;
            this.buttonReplace.Text = "置換して次に(&R)";
            this.buttonReplace.UseVisualStyleBackColor = true;
            this.buttonReplace.Click += new System.EventHandler(this.buttonReplace_Click);
            // 
            // buttonNextFind
            // 
            this.buttonNextFind.Location = new System.Drawing.Point(0, 0);
            this.buttonNextFind.Name = "buttonNextFind";
            this.buttonNextFind.Size = new System.Drawing.Size(100, 25);
            this.buttonNextFind.TabIndex = 0;
            this.buttonNextFind.Text = "次を検索(&F)";
            this.buttonNextFind.UseVisualStyleBackColor = true;
            this.buttonNextFind.Click += new System.EventHandler(this.buttonNextFind_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.textBoxFindTxt);
            this.panel2.Location = new System.Drawing.Point(12, 6);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(345, 25);
            this.panel2.TabIndex = 3;
            // 
            // textBoxReplaceTxt
            // 
            this.textBoxReplaceTxt.Location = new System.Drawing.Point(105, 3);
            this.textBoxReplaceTxt.Name = "textBoxReplaceTxt";
            this.textBoxReplaceTxt.Size = new System.Drawing.Size(230, 19);
            this.textBoxReplaceTxt.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label2.Location = new System.Drawing.Point(-2, 6);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(101, 12);
            this.label2.TabIndex = 0;
            this.label2.Text = "置換する文字列(&P):";
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.textBoxReplaceTxt);
            this.panel3.Controls.Add(this.label2);
            this.panel3.Location = new System.Drawing.Point(12, 37);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(345, 25);
            this.panel3.TabIndex = 4;
            // 
            // chkBox1
            // 
            this.chkBox1.AutoSize = true;
            this.chkBox1.Location = new System.Drawing.Point(11, 85);
            this.chkBox1.Name = "chkBox1";
            this.chkBox1.Size = new System.Drawing.Size(139, 16);
            this.chkBox1.TabIndex = 5;
            this.chkBox1.Text = "文字の大きさを区別する";
            this.chkBox1.UseVisualStyleBackColor = true;
            // 
            // ReplacementForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(484, 131);
            this.Controls.Add(this.chkBox1);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "ReplacementForm";
            this.Text = "置換";
            this.Activated += new System.EventHandler(this.ReplacementForm_Activated);
            this.Load += new System.EventHandler(this.ReplacementForm_Load);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxFindTxt;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Button buttonAllRepleace;
        private System.Windows.Forms.Button buttonReplace;
        private System.Windows.Forms.Button buttonNextFind;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxReplaceTxt;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.CheckBox chkBox1;
    }
}