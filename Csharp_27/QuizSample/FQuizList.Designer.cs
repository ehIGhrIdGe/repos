
namespace QuizSample
{
    partial class FQuizList
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.label1 = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.btnClose = new System.Windows.Forms.Button();
            this.ColQuizId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColCategoryName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColQuiestion = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColEdit = new System.Windows.Forms.DataGridViewButtonColumn();
            this.ColDelete = new System.Windows.Forms.DataGridViewButtonColumn();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Meiryo UI", 20F);
            this.label1.Location = new System.Drawing.Point(230, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(192, 50);
            this.label1.TabIndex = 0;
            this.label1.Text = "クイズ一覧";
            // 
            // dataGridView1
            // 
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Meiryo UI", 8.25F);
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColQuizId,
            this.ColCategoryName,
            this.ColQuiestion,
            this.ColEdit,
            this.ColDelete});
            this.dataGridView1.Location = new System.Drawing.Point(13, 96);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.RowHeadersWidth = 10;
            this.dataGridView1.RowTemplate.Height = 27;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(653, 245);
            this.dataGridView1.TabIndex = 1;
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(566, 362);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(100, 30);
            this.btnClose.TabIndex = 2;
            this.btnClose.Text = "閉じる";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // ColQuizId
            // 
            this.ColQuizId.DataPropertyName = "quiz_id";
            this.ColQuizId.HeaderText = "ID";
            this.ColQuizId.MinimumWidth = 8;
            this.ColQuizId.Name = "ColQuizId";
            this.ColQuizId.Width = 150;
            // 
            // ColCategoryName
            // 
            this.ColCategoryName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.ColCategoryName.HeaderText = "カテゴリ";
            this.ColCategoryName.MinimumWidth = 8;
            this.ColCategoryName.Name = "ColCategoryName";
            // 
            // ColQuiestion
            // 
            this.ColQuiestion.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.ColQuiestion.HeaderText = "問題";
            this.ColQuiestion.MinimumWidth = 8;
            this.ColQuiestion.Name = "ColQuiestion";
            // 
            // ColEdit
            // 
            this.ColEdit.HeaderText = "";
            this.ColEdit.MinimumWidth = 8;
            this.ColEdit.Name = "ColEdit";
            this.ColEdit.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.ColEdit.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.ColEdit.Text = "編集";
            this.ColEdit.UseColumnTextForButtonValue = true;
            this.ColEdit.Width = 50;
            // 
            // ColDelete
            // 
            this.ColDelete.HeaderText = "";
            this.ColDelete.MinimumWidth = 8;
            this.ColDelete.Name = "ColDelete";
            this.ColDelete.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.ColDelete.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.ColDelete.Text = "削除";
            this.ColDelete.UseColumnTextForButtonValue = true;
            this.ColDelete.Width = 50;
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(13, 44);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(121, 30);
            this.comboBox1.TabIndex = 3;
            // 
            // FQuizList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 22F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(678, 404);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Meiryo UI", 8.25F);
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "FQuizList";
            this.Text = "クイズ一覧";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColQuizId;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColCategoryName;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColQuiestion;
        private System.Windows.Forms.DataGridViewButtonColumn ColEdit;
        private System.Windows.Forms.DataGridViewButtonColumn ColDelete;
        private System.Windows.Forms.ComboBox comboBox1;
    }
}