namespace Quiz
{
    partial class FmSetting
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FmSetting));
            this.BtnCancel = new System.Windows.Forms.Button();
            this.BtnOK = new System.Windows.Forms.Button();
            this.GrpEncoding = new System.Windows.Forms.GroupBox();
            this.RdbSJIS = new System.Windows.Forms.RadioButton();
            this.RdbUTF_8 = new System.Windows.Forms.RadioButton();
            this.GrpImport = new System.Windows.Forms.GroupBox();
            this.RdbImportReplace = new System.Windows.Forms.RadioButton();
            this.RdbImportAdd = new System.Windows.Forms.RadioButton();
            this.GrpStudyStyle = new System.Windows.Forms.GroupBox();
            this.RdbStudyFmStudy = new System.Windows.Forms.RadioButton();
            this.RdbStudyFmQuiz = new System.Windows.Forms.RadioButton();
            this.GrpEncoding.SuspendLayout();
            this.GrpImport.SuspendLayout();
            this.GrpStudyStyle.SuspendLayout();
            this.SuspendLayout();
            // 
            // BtnCancel
            // 
            this.BtnCancel.Location = new System.Drawing.Point(676, 403);
            this.BtnCancel.Name = "BtnCancel";
            this.BtnCancel.Size = new System.Drawing.Size(112, 35);
            this.BtnCancel.TabIndex = 0;
            this.BtnCancel.Text = "キャンセル";
            this.BtnCancel.UseVisualStyleBackColor = true;
            this.BtnCancel.Click += new System.EventHandler(this.BtnCancel_Click);
            // 
            // BtnOK
            // 
            this.BtnOK.Location = new System.Drawing.Point(558, 403);
            this.BtnOK.Name = "BtnOK";
            this.BtnOK.Size = new System.Drawing.Size(112, 35);
            this.BtnOK.TabIndex = 0;
            this.BtnOK.Text = "OK";
            this.BtnOK.UseVisualStyleBackColor = true;
            this.BtnOK.Click += new System.EventHandler(this.BtnOK_Click);
            // 
            // GrpEncoding
            // 
            this.GrpEncoding.Controls.Add(this.RdbSJIS);
            this.GrpEncoding.Controls.Add(this.RdbUTF_8);
            this.GrpEncoding.Location = new System.Drawing.Point(12, 12);
            this.GrpEncoding.Name = "GrpEncoding";
            this.GrpEncoding.Size = new System.Drawing.Size(175, 116);
            this.GrpEncoding.TabIndex = 1;
            this.GrpEncoding.TabStop = false;
            this.GrpEncoding.Text = "エンコード";
            // 
            // RdbSJIS
            // 
            this.RdbSJIS.AutoSize = true;
            this.RdbSJIS.Font = new System.Drawing.Font("游ゴシック", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.RdbSJIS.Location = new System.Drawing.Point(24, 71);
            this.RdbSJIS.Name = "RdbSJIS";
            this.RdbSJIS.Size = new System.Drawing.Size(118, 30);
            this.RdbSJIS.TabIndex = 0;
            this.RdbSJIS.TabStop = true;
            this.RdbSJIS.Text = "Shift_JIS";
            this.RdbSJIS.UseVisualStyleBackColor = true;
            // 
            // RdbUTF_8
            // 
            this.RdbUTF_8.AutoSize = true;
            this.RdbUTF_8.Font = new System.Drawing.Font("游ゴシック", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.RdbUTF_8.Location = new System.Drawing.Point(24, 35);
            this.RdbUTF_8.Name = "RdbUTF_8";
            this.RdbUTF_8.Size = new System.Drawing.Size(96, 30);
            this.RdbUTF_8.TabIndex = 0;
            this.RdbUTF_8.TabStop = true;
            this.RdbUTF_8.Text = "UTF-8";
            this.RdbUTF_8.UseVisualStyleBackColor = true;
            // 
            // GrpImport
            // 
            this.GrpImport.Controls.Add(this.RdbImportReplace);
            this.GrpImport.Controls.Add(this.RdbImportAdd);
            this.GrpImport.Location = new System.Drawing.Point(205, 12);
            this.GrpImport.Name = "GrpImport";
            this.GrpImport.Size = new System.Drawing.Size(199, 116);
            this.GrpImport.TabIndex = 2;
            this.GrpImport.TabStop = false;
            this.GrpImport.Text = "インポート時の動作";
            // 
            // RdbImportReplace
            // 
            this.RdbImportReplace.AutoSize = true;
            this.RdbImportReplace.Font = new System.Drawing.Font("游ゴシック", 10F);
            this.RdbImportReplace.Location = new System.Drawing.Point(25, 71);
            this.RdbImportReplace.Name = "RdbImportReplace";
            this.RdbImportReplace.Size = new System.Drawing.Size(117, 30);
            this.RdbImportReplace.TabIndex = 0;
            this.RdbImportReplace.TabStop = true;
            this.RdbImportReplace.Text = "置き換え";
            this.RdbImportReplace.UseVisualStyleBackColor = true;
            // 
            // RdbImportAdd
            // 
            this.RdbImportAdd.AutoSize = true;
            this.RdbImportAdd.Font = new System.Drawing.Font("游ゴシック", 10F);
            this.RdbImportAdd.Location = new System.Drawing.Point(25, 35);
            this.RdbImportAdd.Name = "RdbImportAdd";
            this.RdbImportAdd.Size = new System.Drawing.Size(77, 30);
            this.RdbImportAdd.TabIndex = 0;
            this.RdbImportAdd.TabStop = true;
            this.RdbImportAdd.Text = "追加";
            this.RdbImportAdd.UseVisualStyleBackColor = true;
            // 
            // GrpStudyStyle
            // 
            this.GrpStudyStyle.Controls.Add(this.RdbStudyFmQuiz);
            this.GrpStudyStyle.Controls.Add(this.RdbStudyFmStudy);
            this.GrpStudyStyle.Location = new System.Drawing.Point(12, 149);
            this.GrpStudyStyle.Name = "GrpStudyStyle";
            this.GrpStudyStyle.Size = new System.Drawing.Size(199, 116);
            this.GrpStudyStyle.TabIndex = 3;
            this.GrpStudyStyle.TabStop = false;
            this.GrpStudyStyle.Text = "学習方法";
            // 
            // RdbStudyFmStudy
            // 
            this.RdbStudyFmStudy.AutoSize = true;
            this.RdbStudyFmStudy.Font = new System.Drawing.Font("游ゴシック", 10F);
            this.RdbStudyFmStudy.Location = new System.Drawing.Point(24, 34);
            this.RdbStudyFmStudy.Name = "RdbStudyFmStudy";
            this.RdbStudyFmStudy.Size = new System.Drawing.Size(117, 30);
            this.RdbStudyFmStudy.TabIndex = 1;
            this.RdbStudyFmStudy.TabStop = true;
            this.RdbStudyFmStudy.Text = "演習形式";
            this.RdbStudyFmStudy.UseVisualStyleBackColor = true;
            // 
            // RdbStudyFmQuiz
            // 
            this.RdbStudyFmQuiz.AutoSize = true;
            this.RdbStudyFmQuiz.Font = new System.Drawing.Font("游ゴシック", 10F);
            this.RdbStudyFmQuiz.Location = new System.Drawing.Point(24, 70);
            this.RdbStudyFmQuiz.Name = "RdbStudyFmQuiz";
            this.RdbStudyFmQuiz.Size = new System.Drawing.Size(117, 30);
            this.RdbStudyFmQuiz.TabIndex = 1;
            this.RdbStudyFmQuiz.TabStop = true;
            this.RdbStudyFmQuiz.Text = "実践形式";
            this.RdbStudyFmQuiz.UseVisualStyleBackColor = true;
            // 
            // FmSetting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.GrpStudyStyle);
            this.Controls.Add(this.GrpImport);
            this.Controls.Add(this.GrpEncoding);
            this.Controls.Add(this.BtnOK);
            this.Controls.Add(this.BtnCancel);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FmSetting";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "設定";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FmSetting_FormClosed);
            this.Shown += new System.EventHandler(this.FmSetting_Shown);
            this.GrpEncoding.ResumeLayout(false);
            this.GrpEncoding.PerformLayout();
            this.GrpImport.ResumeLayout(false);
            this.GrpImport.PerformLayout();
            this.GrpStudyStyle.ResumeLayout(false);
            this.GrpStudyStyle.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button BtnCancel;
        private System.Windows.Forms.Button BtnOK;
        private System.Windows.Forms.GroupBox GrpEncoding;
        private System.Windows.Forms.RadioButton RdbSJIS;
        private System.Windows.Forms.RadioButton RdbUTF_8;
        private System.Windows.Forms.GroupBox GrpImport;
        private System.Windows.Forms.RadioButton RdbImportReplace;
        private System.Windows.Forms.RadioButton RdbImportAdd;
        private System.Windows.Forms.GroupBox GrpStudyStyle;
        private System.Windows.Forms.RadioButton RdbStudyFmQuiz;
        private System.Windows.Forms.RadioButton RdbStudyFmStudy;
    }
}