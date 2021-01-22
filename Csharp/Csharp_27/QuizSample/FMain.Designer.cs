
namespace QuizSample
{
    partial class FMain
    {
        /// <summary>
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージド リソースを破棄する場合は true を指定し、その他の場合は false を指定します。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナーで生成されたコード

        /// <summary>
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.btnPlayStart = new System.Windows.Forms.Button();
            this.btnQuizList = new System.Windows.Forms.Button();
            this.btnEnd = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Meiryo UI", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label1.Location = new System.Drawing.Point(223, 34);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(219, 50);
            this.label1.TabIndex = 0;
            this.label1.Text = "メニュー画面";
            // 
            // btnPlayStart
            // 
            this.btnPlayStart.Location = new System.Drawing.Point(134, 128);
            this.btnPlayStart.Name = "btnPlayStart";
            this.btnPlayStart.Size = new System.Drawing.Size(400, 50);
            this.btnPlayStart.TabIndex = 1;
            this.btnPlayStart.Text = "遊ぶ";
            this.btnPlayStart.UseVisualStyleBackColor = true;
            this.btnPlayStart.Click += new System.EventHandler(this.btnPlayStart_Click);
            // 
            // btnQuizList
            // 
            this.btnQuizList.Location = new System.Drawing.Point(134, 242);
            this.btnQuizList.Name = "btnQuizList";
            this.btnQuizList.Size = new System.Drawing.Size(400, 50);
            this.btnQuizList.TabIndex = 2;
            this.btnQuizList.Text = "クイズ管理";
            this.btnQuizList.UseVisualStyleBackColor = true;
            this.btnQuizList.Click += new System.EventHandler(this.btnQuizList_Click);
            // 
            // btnEnd
            // 
            this.btnEnd.Location = new System.Drawing.Point(550, 360);
            this.btnEnd.Name = "btnEnd";
            this.btnEnd.Size = new System.Drawing.Size(100, 30);
            this.btnEnd.TabIndex = 3;
            this.btnEnd.Text = "終了";
            this.btnEnd.UseVisualStyleBackColor = true;
            this.btnEnd.Click += new System.EventHandler(this.btnEnd_Click);
            // 
            // FMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 23F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(678, 404);
            this.Controls.Add(this.btnEnd);
            this.Controls.Add(this.btnQuizList);
            this.Controls.Add(this.btnPlayStart);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "FMain";
            this.Text = "メニュー";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnPlayStart;
        private System.Windows.Forms.Button btnQuizList;
        private System.Windows.Forms.Button btnEnd;
    }
}

