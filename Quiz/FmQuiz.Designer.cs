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
            this.components = new System.ComponentModel.Container();
            this.PcbMain = new System.Windows.Forms.PictureBox();
            this.DrawTimer = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.PcbMain)).BeginInit();
            this.SuspendLayout();
            // 
            // PcbMain
            // 
            this.PcbMain.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.PcbMain.BackColor = System.Drawing.Color.White;
            this.PcbMain.Location = new System.Drawing.Point(0, 0);
            this.PcbMain.Name = "PcbMain";
            this.PcbMain.Size = new System.Drawing.Size(260, 287);
            this.PcbMain.TabIndex = 0;
            this.PcbMain.TabStop = false;
            this.PcbMain.MouseDown += new System.Windows.Forms.MouseEventHandler(this.PcbMain_MouseDown);
            this.PcbMain.MouseMove += new System.Windows.Forms.MouseEventHandler(this.PcbMain_MouseMove);
            this.PcbMain.MouseUp += new System.Windows.Forms.MouseEventHandler(this.PcbMain_MouseUp);
            // 
            // DrawTimer
            // 
            this.DrawTimer.Tick += new System.EventHandler(this.DrawTimer_Tick);
            // 
            // FmQuiz
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(517, 583);
            this.Controls.Add(this.PcbMain);
            this.Name = "FmQuiz";
            this.Text = "クイズ";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FmQuiz_FormClosed);
            this.Load += new System.EventHandler(this.FmQuiz_Load);
            this.Resize += new System.EventHandler(this.FmQuiz_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.PcbMain)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox PcbMain;
        private System.Windows.Forms.Timer DrawTimer;
    }
}