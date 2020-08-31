using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Quiz
{
    public partial class FmStudy : Form
    {
        private StudyingList QList;
        private bool finished = false;

        public FmStudy()
        {
            InitializeComponent();
        }

        public static void Show(IEnumerable<Question> qlist)
        {
            FmStudy fm = new FmStudy()
            {
                QList = new StudyingList(qlist),
            };
            if (fm.QList.IsEmpty)
            {
                MessageBox.Show("問題がありません。", "中止", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            fm.Show();
        }

        public static void ShowResume()
        {
            Startup.Fm_Main.Resumable = false;
            FmStudy fm = new FmStudy()
            {
                QList = new StudyingList(Startup.Fm_Main.QList, (StudyingList)Setting.GetData(Setting.DataType.StudyingList)),
            };
            if (fm.QList.IsEmpty)
            {
                MessageBox.Show("問題がありません。", "中止", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            fm.Show();
        }

        private void LoadNext()
        {
            // Load the next question
            if (!QList.NextIndex()) { finished = true; Close(); return; }

            var q = QList.Current;
            TxbQuestion.Text = q.Question.Statement;
            TxbAnswer.Text = "";
            TxbAnswer.ContextMenuStrip = null;
            // Update the description
            TxbDesc.Text = (q.Progress == 1 ? "学習中" : "未知　") +
                $"\t\t{QList.ProgressCount(0)} → {QList.ProgressCount(1)} → {QList.ProgressCount(2)}";
            BtnShowAnswer.Visible = true;
            BtnCorrect.Visible = BtnIncorrect.Visible = false;
            BtnShowAnswer.Focus();
        }

        private void FmQuiz_Load(object sender, EventArgs e)
        {
            var rect = (Rectangle)Setting.GetData(Setting.DataType.FmStudy_Rectangle);
            Location = rect.Location;
            Size = rect.Size;

            // bug fix
            TxbDesc.AutoWordSelection = true;
            TxbDesc.AutoWordSelection = false;
            TxbQuestion.AutoWordSelection = true;
            TxbQuestion.AutoWordSelection = false;
            TxbAnswer.AutoWordSelection = true;
            TxbAnswer.AutoWordSelection = false;

            LoadNext();
        }

        private void BtnShowAnswer_Click(object sender, EventArgs e)
        {
            TxbAnswer.ContextMenuStrip = CmsAnswer;
            TxbAnswer.Text = QList.Current.Question.Answer + "\n" + QList.Current.Question.Ruby;
            TxbAnswer.Select(0, QList.Current.Question.Answer.Length);
            TxbAnswer.SelectionColor = Color.Red;
            TxbAnswer.SelectionFont = new Font(TxbAnswer.Font.FontFamily, 20);
            BtnShowAnswer.Visible = false;
            BtnCorrect.Visible = BtnIncorrect.Visible = true;
            BtnCorrect.Focus();
        }

        private void ShowAnswer(int length)
        {
            length = Math.Min(TxbAnswer.Text.Length + length, QList.Current.Question.Answer.Length);
            TxbAnswer.Text = QList.Current.Question.Answer.Substring(0, length);
            TxbAnswer.Select(0, TxbAnswer.Text.Length);
            TxbAnswer.SelectionColor = Color.Red;
            TxbAnswer.SelectionFont = new Font(TxbAnswer.Font.FontFamily, 20);
        }

        private void Control_KeyDown(object sender, KeyEventArgs e)
        {
            e.SuppressKeyPress = true;

            if (BtnCorrect.Visible)
            {
                if (e.KeyCode == Keys.Y) BtnCorrect_Click(BtnCorrect, e);
                else if (e.KeyCode == Keys.N) BtnIncorrect_Click(BtnIncorrect, e);
            }
            else
            {
                if (e.KeyCode == Keys.Y || e.KeyCode == Keys.N)
                    BtnShowAnswer_Click(BtnShowAnswer, e);
                // n文字出現
                else  if (Keys.D1 <= e.KeyCode && e.KeyCode <= Keys.D9)
                    ShowAnswer(e.KeyCode - Keys.D0);
            }
        }

        private void FmQuiz_FormClosed(object sender, FormClosedEventArgs e)
        {
            Setting.SetData(Setting.DataType.FmStudy_Rectangle, new Rectangle(Location, Size));

            Startup.Fm_Main.UpdateList();
            // 学習中なら再開できるようにしておく
            if (!finished)
            {
                Setting.SetData(Setting.DataType.StudyingList, QList);
                Startup.Fm_Main.Resumable = true;
            }
            else
            {
                Setting.SetData(Setting.DataType.StudyingList, null);
                Startup.Fm_Main.Resumable = false;
            }
        }

        private void BtnCorrect_Click(object sender, EventArgs e)
        {
            QList.Correct();
            LoadNext();
        }

        private void BtnIncorrect_Click(object sender, EventArgs e)
        {
            QList.Incorrect();
            LoadNext();
        }

        private void BtnGoogle_Click(object sender, EventArgs e)
        {
            var txb = (RichTextBox)CmsAnswer.SourceControl;
            if (txb.SelectionLength != 0)
                Process.Start($"https://www.google.com/search?q={txb.SelectedText}");
            else if (txb == TxbAnswer)
                Process.Start($"https://www.google.com/search?q={QList.Current.Question.Answer}");
        }

        private void BtnWiki_Click(object sender, EventArgs e)
        {
            var txb = (RichTextBox)CmsAnswer.SourceControl;
            if (txb.SelectionLength != 0)
                Process.Start($"https://ja.wikipedia.org/wiki/{txb.SelectedText}");
            else if (txb == TxbAnswer)
                Process.Start($"https://ja.wikipedia.org/wiki/{QList.Current.Question.Answer}");
        }

        private void BtnCopy_Click(object sender, EventArgs e)
        {
            var txb = (RichTextBox)CmsAnswer.SourceControl;
            if (txb.SelectionLength != 0)
                Clipboard.SetText(txb.SelectedText);
            else if (txb == TxbAnswer)
                Clipboard.SetText(QList.Current.Question.Answer);
        }

        private void TxbQuestion_SelectionChanged(object sender, EventArgs e)
        {
            if (TxbQuestion.SelectionLength != 0)
                TxbQuestion.ContextMenuStrip = CmsAnswer;
            else
                TxbQuestion.ContextMenuStrip = null;
        }
    }

}
