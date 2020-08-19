namespace Quiz
{
    partial class FmMain
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FmMain));
            this.DgvQuestions = new System.Windows.Forms.DataGridView();
            this.ClmRate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ClmLearn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.MnuFile = new System.Windows.Forms.ToolStripDropDownButton();
            this.BtnSave = new System.Windows.Forms.ToolStripMenuItem();
            this.BtnImport = new System.Windows.Forms.ToolStripMenuItem();
            this.BtnExport = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.BtnClose = new System.Windows.Forms.ToolStripMenuItem();
            this.BtnCloseWithoutSaving = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.MnuStudy = new System.Windows.Forms.ToolStripDropDownButton();
            this.BtnStudyAll = new System.Windows.Forms.ToolStripMenuItem();
            this.StudySelected = new System.Windows.Forms.ToolStripMenuItem();
            this.StudyIgnorant = new System.Windows.Forms.ToolStripMenuItem();
            this.StudyLowRate = new System.Windows.Forms.ToolStripMenuItem();
            this.MnuEdit = new System.Windows.Forms.ToolStripDropDownButton();
            this.BtnAddQuestion = new System.Windows.Forms.ToolStripMenuItem();
            this.BtnEditQuestion = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.BtnSetting = new System.Windows.Forms.ToolStripButton();
            this.StStrip = new System.Windows.Forms.StatusStrip();
            this.LblInfo = new System.Windows.Forms.ToolStripStatusLabel();
            this.StTimer = new System.Windows.Forms.Timer(this.components);
            this.LblSearch = new System.Windows.Forms.ToolStripLabel();
            this.TxbSearch = new System.Windows.Forms.ToolStripTextBox();
            this.ClmStatement = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ClmAnswer = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ClmRuby = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.questionBindingSource = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.DgvQuestions)).BeginInit();
            this.toolStrip1.SuspendLayout();
            this.StStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.questionBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // DgvQuestions
            // 
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
            this.ClmLearn});
            this.DgvQuestions.DataSource = this.questionBindingSource;
            this.DgvQuestions.Location = new System.Drawing.Point(12, 41);
            this.DgvQuestions.Name = "DgvQuestions";
            this.DgvQuestions.RowHeadersWidth = 62;
            this.DgvQuestions.RowTemplate.Height = 27;
            this.DgvQuestions.Size = new System.Drawing.Size(776, 335);
            this.DgvQuestions.TabIndex = 0;
            this.DgvQuestions.CellPainting += new System.Windows.Forms.DataGridViewCellPaintingEventHandler(this.DgvQuestions_CellPainting);
            this.DgvQuestions.UserDeletingRow += new System.Windows.Forms.DataGridViewRowCancelEventHandler(this.DgvQuestions_UserDeletingRow);
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
            // toolStrip1
            // 
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MnuFile,
            this.toolStripSeparator3,
            this.MnuStudy,
            this.MnuEdit,
            this.LblSearch,
            this.TxbSearch,
            this.toolStripSeparator1,
            this.BtnSetting});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(800, 38);
            this.toolStrip1.TabIndex = 2;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // MnuFile
            // 
            this.MnuFile.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.MnuFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.BtnSave,
            this.BtnImport,
            this.BtnExport,
            this.toolStripSeparator2,
            this.BtnClose,
            this.BtnCloseWithoutSaving});
            this.MnuFile.Image = ((System.Drawing.Image)(resources.GetObject("MnuFile.Image")));
            this.MnuFile.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.MnuFile.Name = "MnuFile";
            this.MnuFile.Size = new System.Drawing.Size(81, 33);
            this.MnuFile.Text = "ファイル";
            // 
            // BtnSave
            // 
            this.BtnSave.Name = "BtnSave";
            this.BtnSave.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.BtnSave.Size = new System.Drawing.Size(340, 34);
            this.BtnSave.Text = "上書き保存";
            this.BtnSave.Click += new System.EventHandler(this.BtnSave_Click);
            // 
            // BtnImport
            // 
            this.BtnImport.Name = "BtnImport";
            this.BtnImport.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.I)));
            this.BtnImport.Size = new System.Drawing.Size(340, 34);
            this.BtnImport.Text = "インポート";
            this.BtnImport.Click += new System.EventHandler(this.BtnImport_Click);
            // 
            // BtnExport
            // 
            this.BtnExport.Name = "BtnExport";
            this.BtnExport.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.E)));
            this.BtnExport.Size = new System.Drawing.Size(340, 34);
            this.BtnExport.Text = "エクスポート";
            this.BtnExport.Click += new System.EventHandler(this.BtnExport_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(337, 6);
            // 
            // BtnClose
            // 
            this.BtnClose.Name = "BtnClose";
            this.BtnClose.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.X)));
            this.BtnClose.Size = new System.Drawing.Size(340, 34);
            this.BtnClose.Text = "終了";
            this.BtnClose.Click += new System.EventHandler(this.BtnClose_Click);
            // 
            // BtnCloseWithoutSaving
            // 
            this.BtnCloseWithoutSaving.Name = "BtnCloseWithoutSaving";
            this.BtnCloseWithoutSaving.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.X)));
            this.BtnCloseWithoutSaving.Size = new System.Drawing.Size(340, 34);
            this.BtnCloseWithoutSaving.Text = "保存せずに終了";
            this.BtnCloseWithoutSaving.Click += new System.EventHandler(this.BtnCloseWithoutSaving_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 38);
            // 
            // MnuStudy
            // 
            this.MnuStudy.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.MnuStudy.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.BtnStudyAll,
            this.StudySelected,
            this.StudyIgnorant,
            this.StudyLowRate});
            this.MnuStudy.Image = ((System.Drawing.Image)(resources.GetObject("MnuStudy.Image")));
            this.MnuStudy.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.MnuStudy.Name = "MnuStudy";
            this.MnuStudy.Size = new System.Drawing.Size(66, 33);
            this.MnuStudy.Text = "学習";
            // 
            // BtnStudyAll
            // 
            this.BtnStudyAll.Name = "BtnStudyAll";
            this.BtnStudyAll.Size = new System.Drawing.Size(335, 34);
            this.BtnStudyAll.Text = "全てを学習";
            this.BtnStudyAll.Click += new System.EventHandler(this.BtnStudyAll_Click);
            // 
            // StudySelected
            // 
            this.StudySelected.Name = "StudySelected";
            this.StudySelected.Size = new System.Drawing.Size(335, 34);
            this.StudySelected.Text = "選択した問題を学習";
            this.StudySelected.Click += new System.EventHandler(this.StudySelected_Click);
            // 
            // StudyIgnorant
            // 
            this.StudyIgnorant.Name = "StudyIgnorant";
            this.StudyIgnorant.Size = new System.Drawing.Size(335, 34);
            this.StudyIgnorant.Text = "学習回数の少ない問題を学習";
            this.StudyIgnorant.Click += new System.EventHandler(this.StudyIgnorant_Click);
            // 
            // StudyLowRate
            // 
            this.StudyLowRate.Name = "StudyLowRate";
            this.StudyLowRate.Size = new System.Drawing.Size(335, 34);
            this.StudyLowRate.Text = "正解率の低い問題を学習";
            this.StudyLowRate.Click += new System.EventHandler(this.StudyLowRate_Click);
            // 
            // MnuEdit
            // 
            this.MnuEdit.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.MnuEdit.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.BtnAddQuestion,
            this.BtnEditQuestion});
            this.MnuEdit.Image = ((System.Drawing.Image)(resources.GetObject("MnuEdit.Image")));
            this.MnuEdit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.MnuEdit.Name = "MnuEdit";
            this.MnuEdit.Size = new System.Drawing.Size(66, 33);
            this.MnuEdit.Text = "編集";
            // 
            // BtnAddQuestion
            // 
            this.BtnAddQuestion.Name = "BtnAddQuestion";
            this.BtnAddQuestion.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.P)));
            this.BtnAddQuestion.Size = new System.Drawing.Size(261, 34);
            this.BtnAddQuestion.Text = "問題を追加";
            this.BtnAddQuestion.Click += new System.EventHandler(this.BtnAddQuestion_Click);
            // 
            // BtnEditQuestion
            // 
            this.BtnEditQuestion.Name = "BtnEditQuestion";
            this.BtnEditQuestion.Size = new System.Drawing.Size(261, 34);
            this.BtnEditQuestion.Text = "問題を編集";
            this.BtnEditQuestion.Click += new System.EventHandler(this.BtnEditQuestion_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 38);
            // 
            // BtnSetting
            // 
            this.BtnSetting.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.BtnSetting.Image = ((System.Drawing.Image)(resources.GetObject("BtnSetting.Image")));
            this.BtnSetting.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.BtnSetting.Name = "BtnSetting";
            this.BtnSetting.Size = new System.Drawing.Size(52, 33);
            this.BtnSetting.Text = "設定";
            this.BtnSetting.Click += new System.EventHandler(this.BtnSetting_Click);
            // 
            // StStrip
            // 
            this.StStrip.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.StStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.LblInfo});
            this.StStrip.Location = new System.Drawing.Point(0, 379);
            this.StStrip.Name = "StStrip";
            this.StStrip.Size = new System.Drawing.Size(800, 32);
            this.StStrip.TabIndex = 3;
            // 
            // LblInfo
            // 
            this.LblInfo.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.LblInfo.Name = "LblInfo";
            this.LblInfo.Size = new System.Drawing.Size(48, 25);
            this.LblInfo.Text = "情報";
            // 
            // StTimer
            // 
            this.StTimer.Interval = 10000;
            this.StTimer.Tick += new System.EventHandler(this.StTimer_Tick);
            // 
            // LblSearch
            // 
            this.LblSearch.Name = "LblSearch";
            this.LblSearch.Size = new System.Drawing.Size(48, 33);
            this.LblSearch.Text = "検索";
            // 
            // TxbSearch
            // 
            this.TxbSearch.Font = new System.Drawing.Font("Yu Gothic UI", 9F);
            this.TxbSearch.Name = "TxbSearch";
            this.TxbSearch.Size = new System.Drawing.Size(150, 38);
            this.TxbSearch.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxbSearch_KeyDown);
            this.TxbSearch.Validated += new System.EventHandler(this.TxbSearch_Validated);
            // 
            // ClmStatement
            // 
            this.ClmStatement.DataPropertyName = "Statement";
            this.ClmStatement.HeaderText = "問題";
            this.ClmStatement.MinimumWidth = 8;
            this.ClmStatement.Name = "ClmStatement";
            this.ClmStatement.Width = 150;
            // 
            // ClmAnswer
            // 
            this.ClmAnswer.DataPropertyName = "Answer";
            this.ClmAnswer.HeaderText = "解答";
            this.ClmAnswer.MinimumWidth = 8;
            this.ClmAnswer.Name = "ClmAnswer";
            this.ClmAnswer.Width = 150;
            // 
            // ClmRuby
            // 
            this.ClmRuby.DataPropertyName = "Ruby";
            this.ClmRuby.HeaderText = "読み";
            this.ClmRuby.MinimumWidth = 8;
            this.ClmRuby.Name = "ClmRuby";
            this.ClmRuby.Width = 150;
            // 
            // questionBindingSource
            // 
            this.questionBindingSource.DataSource = typeof(Quiz.Question);
            // 
            // FmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 411);
            this.Controls.Add(this.StStrip);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.DgvQuestions);
            this.Name = "FmMain";
            this.Text = "クイズ一覧";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FmMain_FormClosed);
            this.Load += new System.EventHandler(this.FmMain_Load);
            ((System.ComponentModel.ISupportInitialize)(this.DgvQuestions)).EndInit();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.StStrip.ResumeLayout(false);
            this.StStrip.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.questionBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView DgvQuestions;
        private System.Windows.Forms.BindingSource questionBindingSource;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripDropDownButton MnuFile;
        private System.Windows.Forms.ToolStripMenuItem BtnImport;
        private System.Windows.Forms.ToolStripMenuItem BtnExport;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem BtnClose;
        private System.Windows.Forms.ToolStripMenuItem BtnCloseWithoutSaving;
        private System.Windows.Forms.ToolStripDropDownButton MnuStudy;
        private System.Windows.Forms.ToolStripMenuItem BtnStudyAll;
        private System.Windows.Forms.ToolStripDropDownButton MnuEdit;
        private System.Windows.Forms.ToolStripButton BtnSetting;
        private System.Windows.Forms.ToolStripMenuItem BtnAddQuestion;
        private System.Windows.Forms.ToolStripMenuItem BtnSave;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem StudySelected;
        private System.Windows.Forms.ToolStripMenuItem BtnEditQuestion;
        private System.Windows.Forms.DataGridViewTextBoxColumn ClmStatement;
        private System.Windows.Forms.DataGridViewTextBoxColumn ClmAnswer;
        private System.Windows.Forms.DataGridViewTextBoxColumn ClmRuby;
        private System.Windows.Forms.DataGridViewTextBoxColumn ClmRate;
        private System.Windows.Forms.DataGridViewTextBoxColumn ClmLearn;
        private System.Windows.Forms.StatusStrip StStrip;
        private System.Windows.Forms.ToolStripStatusLabel LblInfo;
        private System.Windows.Forms.Timer StTimer;
        private System.Windows.Forms.ToolStripMenuItem StudyIgnorant;
        private System.Windows.Forms.ToolStripMenuItem StudyLowRate;
        private System.Windows.Forms.ToolStripLabel LblSearch;
        private System.Windows.Forms.ToolStripTextBox TxbSearch;
    }
}

