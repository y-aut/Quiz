namespace Quiz
{
    partial class FmPreview
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
            this.components = new System.ComponentModel.Container();
            this.BtnOK = new System.Windows.Forms.Button();
            this.BtnCancel = new System.Windows.Forms.Button();
            this.NudPattern = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.LblRange = new System.Windows.Forms.Label();
            this.DgvQuestions = new System.Windows.Forms.DataGridView();
            this.ClmRate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ClmLearn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ClmFinalDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ClmFavorite = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.ClmStatement = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ClmAnswer = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ClmRuby = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.questionBindingSource = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.NudPattern)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DgvQuestions)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.questionBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // BtnOK
            // 
            this.BtnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnOK.Location = new System.Drawing.Point(542, 404);
            this.BtnOK.Name = "BtnOK";
            this.BtnOK.Size = new System.Drawing.Size(120, 34);
            this.BtnOK.TabIndex = 2;
            this.BtnOK.Text = "決定";
            this.BtnOK.UseVisualStyleBackColor = true;
            this.BtnOK.Click += new System.EventHandler(this.BtnOK_Click);
            // 
            // BtnCancel
            // 
            this.BtnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnCancel.Location = new System.Drawing.Point(668, 404);
            this.BtnCancel.Name = "BtnCancel";
            this.BtnCancel.Size = new System.Drawing.Size(120, 34);
            this.BtnCancel.TabIndex = 2;
            this.BtnCancel.Text = "キャンセル";
            this.BtnCancel.UseVisualStyleBackColor = true;
            this.BtnCancel.Click += new System.EventHandler(this.BtnCancel_Click);
            // 
            // NudPattern
            // 
            this.NudPattern.Location = new System.Drawing.Point(138, 12);
            this.NudPattern.Maximum = new decimal(new int[] {
            4,
            0,
            0,
            0});
            this.NudPattern.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.NudPattern.Name = "NudPattern";
            this.NudPattern.Size = new System.Drawing.Size(120, 25);
            this.NudPattern.TabIndex = 3;
            this.NudPattern.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.NudPattern.ValueChanged += new System.EventHandler(this.NudPattern_ValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("HGSｺﾞｼｯｸM", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label1.Location = new System.Drawing.Point(28, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(89, 20);
            this.label1.TabIndex = 4;
            this.label1.Text = "パターン";
            // 
            // LblRange
            // 
            this.LblRange.AutoSize = true;
            this.LblRange.Font = new System.Drawing.Font("HGSｺﾞｼｯｸM", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.LblRange.Location = new System.Drawing.Point(284, 14);
            this.LblRange.Name = "LblRange";
            this.LblRange.Size = new System.Drawing.Size(93, 20);
            this.LblRange.TabIndex = 5;
            this.LblRange.Text = "（1～4）";
            // 
            // DgvQuestions
            // 
            this.DgvQuestions.AllowUserToAddRows = false;
            this.DgvQuestions.AllowUserToDeleteRows = false;
            this.DgvQuestions.AllowUserToOrderColumns = true;
            this.DgvQuestions.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.DgvQuestions.AutoGenerateColumns = false;
            this.DgvQuestions.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DgvQuestions.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ClmStatement,
            this.ClmAnswer,
            this.ClmRuby,
            this.ClmRate,
            this.ClmLearn,
            this.ClmFinalDate,
            this.ClmFavorite});
            this.DgvQuestions.DataSource = this.questionBindingSource;
            this.DgvQuestions.Location = new System.Drawing.Point(12, 43);
            this.DgvQuestions.Name = "DgvQuestions";
            this.DgvQuestions.ReadOnly = true;
            this.DgvQuestions.RowHeadersWidth = 62;
            this.DgvQuestions.RowTemplate.Height = 27;
            this.DgvQuestions.Size = new System.Drawing.Size(776, 355);
            this.DgvQuestions.TabIndex = 6;
            this.DgvQuestions.CellPainting += new System.Windows.Forms.DataGridViewCellPaintingEventHandler(this.DgvQuestions_CellPainting);
            // 
            // ClmRate
            // 
            this.ClmRate.DataPropertyName = "Rate";
            this.ClmRate.HeaderText = "正解率（％）";
            this.ClmRate.MinimumWidth = 8;
            this.ClmRate.Name = "ClmRate";
            this.ClmRate.ReadOnly = true;
            this.ClmRate.Width = 150;
            // 
            // ClmLearn
            // 
            this.ClmLearn.DataPropertyName = "LearnCount";
            this.ClmLearn.HeaderText = "学習回数";
            this.ClmLearn.MinimumWidth = 8;
            this.ClmLearn.Name = "ClmLearn";
            this.ClmLearn.ReadOnly = true;
            this.ClmLearn.Width = 150;
            // 
            // ClmFinalDate
            // 
            this.ClmFinalDate.DataPropertyName = "FinalDate";
            this.ClmFinalDate.HeaderText = "最終学習日時";
            this.ClmFinalDate.MinimumWidth = 8;
            this.ClmFinalDate.Name = "ClmFinalDate";
            this.ClmFinalDate.ReadOnly = true;
            this.ClmFinalDate.Width = 150;
            // 
            // ClmFavorite
            // 
            this.ClmFavorite.DataPropertyName = "Favorite";
            this.ClmFavorite.HeaderText = "お気に";
            this.ClmFavorite.MinimumWidth = 8;
            this.ClmFavorite.Name = "ClmFavorite";
            this.ClmFavorite.ReadOnly = true;
            this.ClmFavorite.Width = 150;
            // 
            // ClmStatement
            // 
            this.ClmStatement.DataPropertyName = "Statement";
            this.ClmStatement.HeaderText = "問題";
            this.ClmStatement.MinimumWidth = 8;
            this.ClmStatement.Name = "ClmStatement";
            this.ClmStatement.ReadOnly = true;
            this.ClmStatement.Width = 150;
            // 
            // ClmAnswer
            // 
            this.ClmAnswer.DataPropertyName = "Answer";
            this.ClmAnswer.HeaderText = "解答";
            this.ClmAnswer.MinimumWidth = 8;
            this.ClmAnswer.Name = "ClmAnswer";
            this.ClmAnswer.ReadOnly = true;
            this.ClmAnswer.Width = 150;
            // 
            // ClmRuby
            // 
            this.ClmRuby.DataPropertyName = "Ruby";
            this.ClmRuby.HeaderText = "読み";
            this.ClmRuby.MinimumWidth = 8;
            this.ClmRuby.Name = "ClmRuby";
            this.ClmRuby.ReadOnly = true;
            this.ClmRuby.Width = 150;
            // 
            // questionBindingSource
            // 
            this.questionBindingSource.DataSource = typeof(Quiz.Question);
            // 
            // FmPreview
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.DgvQuestions);
            this.Controls.Add(this.LblRange);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.NudPattern);
            this.Controls.Add(this.BtnCancel);
            this.Controls.Add(this.BtnOK);
            this.Name = "FmPreview";
            this.Text = "プレビュー";
            this.Load += new System.EventHandler(this.FmPreview_Load);
            ((System.ComponentModel.ISupportInitialize)(this.NudPattern)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DgvQuestions)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.questionBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button BtnOK;
        private System.Windows.Forms.Button BtnCancel;
        private System.Windows.Forms.NumericUpDown NudPattern;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label LblRange;
        private System.Windows.Forms.BindingSource questionBindingSource;
        private System.Windows.Forms.DataGridView DgvQuestions;
        private System.Windows.Forms.DataGridViewTextBoxColumn ClmStatement;
        private System.Windows.Forms.DataGridViewTextBoxColumn ClmAnswer;
        private System.Windows.Forms.DataGridViewTextBoxColumn ClmRuby;
        private System.Windows.Forms.DataGridViewTextBoxColumn ClmRate;
        private System.Windows.Forms.DataGridViewTextBoxColumn ClmLearn;
        private System.Windows.Forms.DataGridViewTextBoxColumn ClmFinalDate;
        private System.Windows.Forms.DataGridViewCheckBoxColumn ClmFavorite;
    }
}