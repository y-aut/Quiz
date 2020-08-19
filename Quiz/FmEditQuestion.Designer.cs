namespace Quiz
{
    partial class FmEditQuestion
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
            this.TxbStatement = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.TxbAnswer = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.TxbRuby = new System.Windows.Forms.TextBox();
            this.BtnOK = new System.Windows.Forms.Button();
            this.BtnCancel = new System.Windows.Forms.Button();
            this.LblAdd = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("HGSｺﾞｼｯｸM", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label1.Location = new System.Drawing.Point(9, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(69, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "問題文";
            // 
            // TxbStatement
            // 
            this.TxbStatement.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TxbStatement.Font = new System.Drawing.Font("HGSｺﾞｼｯｸM", 10F);
            this.TxbStatement.Location = new System.Drawing.Point(12, 48);
            this.TxbStatement.Multiline = true;
            this.TxbStatement.Name = "TxbStatement";
            this.TxbStatement.Size = new System.Drawing.Size(667, 116);
            this.TxbStatement.TabIndex = 1;
            this.TxbStatement.TextChanged += new System.EventHandler(this.TxbStatement_TextChanged);
            this.TxbStatement.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Control_KeyDown);
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("HGSｺﾞｼｯｸM", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label2.Location = new System.Drawing.Point(9, 179);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(49, 20);
            this.label2.TabIndex = 2;
            this.label2.Text = "解答";
            // 
            // TxbAnswer
            // 
            this.TxbAnswer.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TxbAnswer.Font = new System.Drawing.Font("HGSｺﾞｼｯｸM", 10F);
            this.TxbAnswer.Location = new System.Drawing.Point(12, 207);
            this.TxbAnswer.Name = "TxbAnswer";
            this.TxbAnswer.Size = new System.Drawing.Size(667, 27);
            this.TxbAnswer.TabIndex = 3;
            this.TxbAnswer.TextChanged += new System.EventHandler(this.TxbAnswer_TextChanged);
            this.TxbAnswer.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Control_KeyDown);
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("HGSｺﾞｼｯｸM", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label3.Location = new System.Drawing.Point(9, 249);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(49, 20);
            this.label3.TabIndex = 4;
            this.label3.Text = "読み";
            // 
            // TxbRuby
            // 
            this.TxbRuby.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TxbRuby.Font = new System.Drawing.Font("HGSｺﾞｼｯｸM", 10F);
            this.TxbRuby.Location = new System.Drawing.Point(13, 278);
            this.TxbRuby.Name = "TxbRuby";
            this.TxbRuby.Size = new System.Drawing.Size(667, 27);
            this.TxbRuby.TabIndex = 5;
            this.TxbRuby.TextChanged += new System.EventHandler(this.TxbRuby_TextChanged);
            this.TxbRuby.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Control_KeyDown);
            // 
            // BtnOK
            // 
            this.BtnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnOK.Enabled = false;
            this.BtnOK.Location = new System.Drawing.Point(452, 335);
            this.BtnOK.Name = "BtnOK";
            this.BtnOK.Size = new System.Drawing.Size(109, 34);
            this.BtnOK.TabIndex = 6;
            this.BtnOK.Text = "決定";
            this.BtnOK.UseVisualStyleBackColor = true;
            this.BtnOK.Click += new System.EventHandler(this.BtnOK_Click);
            this.BtnOK.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Control_KeyDown);
            // 
            // BtnCancel
            // 
            this.BtnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnCancel.Location = new System.Drawing.Point(567, 335);
            this.BtnCancel.Name = "BtnCancel";
            this.BtnCancel.Size = new System.Drawing.Size(109, 34);
            this.BtnCancel.TabIndex = 7;
            this.BtnCancel.Text = "キャンセル";
            this.BtnCancel.UseVisualStyleBackColor = true;
            this.BtnCancel.Click += new System.EventHandler(this.BtnCancel_Click);
            this.BtnCancel.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Control_KeyDown);
            // 
            // LblAdd
            // 
            this.LblAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.LblAdd.AutoSize = true;
            this.LblAdd.Font = new System.Drawing.Font("HGSｺﾞｼｯｸM", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.LblAdd.Location = new System.Drawing.Point(13, 343);
            this.LblAdd.Name = "LblAdd";
            this.LblAdd.Size = new System.Drawing.Size(331, 20);
            this.LblAdd.TabIndex = 8;
            this.LblAdd.Text = "※ [Ctrl] + [Enter] で続けて追加する";
            // 
            // FmEditQuestion
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(691, 381);
            this.Controls.Add(this.LblAdd);
            this.Controls.Add(this.BtnCancel);
            this.Controls.Add(this.BtnOK);
            this.Controls.Add(this.TxbRuby);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.TxbAnswer);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.TxbStatement);
            this.Controls.Add(this.label1);
            this.Name = "FmEditQuestion";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "問題を編集";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FmEditQuestion_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox TxbStatement;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox TxbAnswer;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox TxbRuby;
        private System.Windows.Forms.Button BtnOK;
        private System.Windows.Forms.Button BtnCancel;
        private System.Windows.Forms.Label LblAdd;
    }
}