namespace Quiz
{
    partial class FmSearch
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FmSearch));
            this.TxbSearch = new System.Windows.Forms.TextBox();
            this.BtnClose = new System.Windows.Forms.Button();
            this.ChbTopMost = new System.Windows.Forms.CheckBox();
            this.BtnSearch = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // TxbSearch
            // 
            this.TxbSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TxbSearch.Font = new System.Drawing.Font("HGSｺﾞｼｯｸM", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.TxbSearch.Location = new System.Drawing.Point(13, 13);
            this.TxbSearch.Multiline = true;
            this.TxbSearch.Name = "TxbSearch";
            this.TxbSearch.Size = new System.Drawing.Size(499, 181);
            this.TxbSearch.TabIndex = 0;
            this.TxbSearch.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxbSearch_KeyDown);
            // 
            // BtnClose
            // 
            this.BtnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnClose.Location = new System.Drawing.Point(387, 207);
            this.BtnClose.Name = "BtnClose";
            this.BtnClose.Size = new System.Drawing.Size(125, 35);
            this.BtnClose.TabIndex = 3;
            this.BtnClose.Text = "閉じる";
            this.BtnClose.UseVisualStyleBackColor = true;
            this.BtnClose.Click += new System.EventHandler(this.BtnClose_Click);
            // 
            // ChbTopMost
            // 
            this.ChbTopMost.AutoSize = true;
            this.ChbTopMost.Checked = true;
            this.ChbTopMost.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ChbTopMost.Location = new System.Drawing.Point(12, 220);
            this.ChbTopMost.Name = "ChbTopMost";
            this.ChbTopMost.Size = new System.Drawing.Size(138, 22);
            this.ChbTopMost.TabIndex = 2;
            this.ChbTopMost.Text = "最前面に表示";
            this.ChbTopMost.UseVisualStyleBackColor = true;
            this.ChbTopMost.CheckedChanged += new System.EventHandler(this.ChbTopMost_CheckedChanged);
            // 
            // BtnSearch
            // 
            this.BtnSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnSearch.Location = new System.Drawing.Point(256, 207);
            this.BtnSearch.Name = "BtnSearch";
            this.BtnSearch.Size = new System.Drawing.Size(125, 35);
            this.BtnSearch.TabIndex = 3;
            this.BtnSearch.Text = "検索";
            this.BtnSearch.UseVisualStyleBackColor = true;
            this.BtnSearch.Click += new System.EventHandler(this.BtnSearch_Click);
            // 
            // FmSearch
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(524, 254);
            this.Controls.Add(this.ChbTopMost);
            this.Controls.Add(this.BtnSearch);
            this.Controls.Add(this.BtnClose);
            this.Controls.Add(this.TxbSearch);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FmSearch";
            this.Text = "検索";
            this.TopMost = true;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FmSearch_FormClosing);
            this.VisibleChanged += new System.EventHandler(this.FmSearch_VisibleChanged);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox TxbSearch;
        private System.Windows.Forms.Button BtnClose;
        private System.Windows.Forms.CheckBox ChbTopMost;
        private System.Windows.Forms.Button BtnSearch;
    }
}