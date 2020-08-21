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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.RdbImportReplace = new System.Windows.Forms.RadioButton();
            this.RdbImportAdd = new System.Windows.Forms.RadioButton();
            this.GrpEncoding.SuspendLayout();
            this.groupBox1.SuspendLayout();
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
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.RdbImportReplace);
            this.groupBox1.Controls.Add(this.RdbImportAdd);
            this.groupBox1.Location = new System.Drawing.Point(205, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(199, 116);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "インポート時の動作";
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
            // FmSetting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.groupBox1);
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
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button BtnCancel;
        private System.Windows.Forms.Button BtnOK;
        private System.Windows.Forms.GroupBox GrpEncoding;
        private System.Windows.Forms.RadioButton RdbSJIS;
        private System.Windows.Forms.RadioButton RdbUTF_8;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton RdbImportReplace;
        private System.Windows.Forms.RadioButton RdbImportAdd;
    }
}