namespace Quiz
{
    partial class FmQuiz
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
            this.BtnShowAnswer = new System.Windows.Forms.Button();
            this.BtnIncorrect = new System.Windows.Forms.Button();
            this.BtnCorrect = new System.Windows.Forms.Button();
            this.TxbQuestion = new System.Windows.Forms.RichTextBox();
            this.TxbAnswer = new System.Windows.Forms.RichTextBox();
            this.TxbDesc = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // BtnShowAnswer
            // 
            this.BtnShowAnswer.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnShowAnswer.Location = new System.Drawing.Point(405, 365);
            this.BtnShowAnswer.Name = "BtnShowAnswer";
            this.BtnShowAnswer.Size = new System.Drawing.Size(383, 42);
            this.BtnShowAnswer.TabIndex = 0;
            this.BtnShowAnswer.Text = "解答を表示";
            this.BtnShowAnswer.UseVisualStyleBackColor = true;
            this.BtnShowAnswer.Click += new System.EventHandler(this.BtnShowAnswer_Click);
            this.BtnShowAnswer.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Control_KeyDown);
            // 
            // BtnIncorrect
            // 
            this.BtnIncorrect.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnIncorrect.Location = new System.Drawing.Point(591, 365);
            this.BtnIncorrect.Name = "BtnIncorrect";
            this.BtnIncorrect.Size = new System.Drawing.Size(197, 42);
            this.BtnIncorrect.TabIndex = 2;
            this.BtnIncorrect.Text = "もう一度学習する (N)";
            this.BtnIncorrect.UseVisualStyleBackColor = true;
            this.BtnIncorrect.Visible = false;
            this.BtnIncorrect.Click += new System.EventHandler(this.BtnIncorrect_Click);
            this.BtnIncorrect.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Control_KeyDown);
            // 
            // BtnCorrect
            // 
            this.BtnCorrect.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnCorrect.Location = new System.Drawing.Point(405, 365);
            this.BtnCorrect.Name = "BtnCorrect";
            this.BtnCorrect.Size = new System.Drawing.Size(180, 42);
            this.BtnCorrect.TabIndex = 1;
            this.BtnCorrect.Text = "分かりました (Y)";
            this.BtnCorrect.UseVisualStyleBackColor = true;
            this.BtnCorrect.Visible = false;
            this.BtnCorrect.Click += new System.EventHandler(this.BtnCorrect_Click);
            this.BtnCorrect.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Control_KeyDown);
            // 
            // TxbQuestion
            // 
            this.TxbQuestion.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TxbQuestion.BackColor = System.Drawing.Color.White;
            this.TxbQuestion.Font = new System.Drawing.Font("HGSｺﾞｼｯｸM", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.TxbQuestion.Location = new System.Drawing.Point(16, 62);
            this.TxbQuestion.Name = "TxbQuestion";
            this.TxbQuestion.ReadOnly = true;
            this.TxbQuestion.Size = new System.Drawing.Size(772, 168);
            this.TxbQuestion.TabIndex = 3;
            this.TxbQuestion.Text = "問題";
            this.TxbQuestion.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Control_KeyDown);
            // 
            // TxbAnswer
            // 
            this.TxbAnswer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TxbAnswer.BackColor = System.Drawing.Color.White;
            this.TxbAnswer.Font = new System.Drawing.Font("HGSｺﾞｼｯｸM", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.TxbAnswer.Location = new System.Drawing.Point(16, 236);
            this.TxbAnswer.Name = "TxbAnswer";
            this.TxbAnswer.ReadOnly = true;
            this.TxbAnswer.Size = new System.Drawing.Size(772, 111);
            this.TxbAnswer.TabIndex = 4;
            this.TxbAnswer.Text = "解答\n読み";
            this.TxbAnswer.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Control_KeyDown);
            // 
            // TxbDesc
            // 
            this.TxbDesc.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TxbDesc.Font = new System.Drawing.Font("HGSｺﾞｼｯｸM", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.TxbDesc.Location = new System.Drawing.Point(16, 13);
            this.TxbDesc.Name = "TxbDesc";
            this.TxbDesc.Size = new System.Drawing.Size(772, 43);
            this.TxbDesc.TabIndex = 5;
            this.TxbDesc.Text = "説明";
            this.TxbDesc.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Control_KeyDown);
            // 
            // FmQuiz
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 419);
            this.Controls.Add(this.TxbDesc);
            this.Controls.Add(this.TxbAnswer);
            this.Controls.Add(this.TxbQuestion);
            this.Controls.Add(this.BtnCorrect);
            this.Controls.Add(this.BtnIncorrect);
            this.Controls.Add(this.BtnShowAnswer);
            this.Name = "FmQuiz";
            this.Text = "クイズ";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FmQuiz_FormClosed);
            this.Load += new System.EventHandler(this.FmQuiz_Load);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button BtnShowAnswer;
        private System.Windows.Forms.Button BtnIncorrect;
        private System.Windows.Forms.Button BtnCorrect;
        private System.Windows.Forms.RichTextBox TxbQuestion;
        private System.Windows.Forms.RichTextBox TxbAnswer;
        private System.Windows.Forms.RichTextBox TxbDesc;
    }
}