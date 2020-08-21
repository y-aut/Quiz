using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Quiz
{
    public partial class FmSearch : Form
    {
        bool SuppressEvent = false;

        public FmSearch()
        {
            InitializeComponent();
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            Startup.Fm_Main.SearchBox = false;
        }

        private void FmSearch_Shown(object sender, EventArgs e)
        {
            TxbSearch.Text = Startup.Fm_Main.SearchText;
        }

        private void TxbSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                Search();
            }
        }

        private void Search()
        {
            SuppressEvent = true;
            Startup.Fm_Main.SearchText = TxbSearch.Text.FormatForSearch();
            SuppressEvent = false;
        }

        public void SetText(string str)
        {
            if (SuppressEvent) return;
            if (InvokeRequired)
                Invoke((MethodInvoker)(() => TxbSearch.Text = str));
            else
                TxbSearch.Text = str;
        }

        private void ChbTopMost_CheckedChanged(object sender, EventArgs e)
        {
            TopMost = ChbTopMost.Checked;
        }

        private void BtnSearch_Click(object sender, EventArgs e)
        {
            Search();
        }

        private void FmSearch_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                Startup.Fm_Main.SearchBox = false;
            }
        }

        private void FmSearch_VisibleChanged(object sender, EventArgs e)
        {
            TxbSearch.Text = Startup.Fm_Main.SearchText;
        }
    }
}
