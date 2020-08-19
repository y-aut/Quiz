using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Quiz
{
    public partial class FmQuiz : Form
    {
        int i1, i2; // QLists[i1][i2]を学習中

        const int Q_COUNT = 10; // 一度に学習する問題の最大数
        List<List<QuestionProg>> QLists;    // 学習を完了したものは削除していく

        // 問題数
        int[] progress_cnt = { 0, 0, 0 };        

        public FmQuiz()
        {
            InitializeComponent();
        }

        public static void Show(IEnumerable<Question> qlist)
        {
            FmQuiz fm = new FmQuiz();
            var lists = new List<List<QuestionProg>>();
            int cnt = 0;

            // Shuffle
            int[] ind = new int[qlist.Count()];
            for (int i = 0; i < ind.Length; ++i) ind[i] = i;
            ind = ind.OrderBy(i => Guid.NewGuid()).ToArray();

            for (int i = 0; i < ind.Length; ++i)
            {
                if (qlist.ElementAt(ind[i]).IsOK)
                {
                    if (cnt == 0) lists.Add(new List<QuestionProg>());
                    lists.Last().Add(new QuestionProg(qlist.ElementAt(ind[i])));
                    if (++cnt == Q_COUNT) cnt = 0;
                    ++fm.progress_cnt[0];
                }
            }
            if (fm.progress_cnt[0] == 0)
            {
                MessageBox.Show("問題がありません。", "中止", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            fm.QLists = lists;
            fm.Show();
        }

        private QuestionProg CurrentQ() => QLists[i1][i2];

        // If there are no questions to learn, return false
        private bool NextIndex()
        {
            if (i2 == -1)
            {
                // First state
                i2 = 0; return true;
            }
            if (CurrentQ().Progress == 2)
            {
                ++CurrentQ().Question.LearnCount;
                QLists[i1].RemoveAt(i2);
                if (i2 < QLists[i1].Count) return true;
            }
            else if (i1 + 1 < QLists.Count && CurrentQ().Progress == 1)
            {
                // 1回正解したら先送り
                QLists[i1 + 1].Add(CurrentQ());
                QLists[i1].RemoveAt(i2);
                if (i2 < QLists[i1].Count) return true;
                else if (QLists[i1].Count == 0) { ++i1; i2 = 0; return true; }
                else
                {
                    // Shuffle
                    QLists[i1] = QLists[i1].OrderBy(i => Guid.NewGuid()).ToList();
                    i2 = 0;
                    return true;
                }
            }
            else if (i1 + 1 < QLists.Count && QLists[i1].Count == 1 && CurrentQ().Progress == 0)
            {
                // 連続で同じ問題になるのを避ける
                QLists[i1 + 1].Add(CurrentQ());
                QLists[i1].RemoveAt(i2);
                ++i1; i2 = 0;
                // Shuffle
                QLists[i1] = QLists[i1].OrderBy(i => Guid.NewGuid()).ToList();
                return true;
            }
            else if (++i2 < QLists[i1].Count) return true;

            if (QLists[i1].Count != 0)
            {
                i2 = 0;
                // Shuffle
                QLists[i1] = QLists[i1].OrderBy(i => Guid.NewGuid()).ToList();
                return true;
            }
            else if (++i1 < QLists.Count)
            {
                i2 = 0;
                return true;
            }
            else
                return false;
        }

        private void LoadNext()
        {
            // Load the next question
            if (!NextIndex()) { Close(); return; }

            var q = CurrentQ();
            TxbQuestion.Text = q.Question.Statement;
            TxbAnswer.Text = "";
            // Update the description
            TxbDesc.Text = (q.Progress == 1 ? "学習中" : "未知　") +
                $"\t\t{progress_cnt[0]} → {progress_cnt[1]} → {progress_cnt[2]}";
            BtnShowAnswer.Visible = true;
            BtnCorrect.Visible = BtnIncorrect.Visible = false;
            BtnShowAnswer.Focus();
        }

        private void FmQuiz_Load(object sender, EventArgs e)
        {
            i1 = 0; i2 = -1;
            LoadNext();
        }

        private void BtnShowAnswer_Click(object sender, EventArgs e)
        {
            TxbAnswer.Text = CurrentQ().Question.Answer + "\n" + CurrentQ().Question.Ruby;
            TxbAnswer.Select(0, CurrentQ().Question.Answer.Length);
            TxbAnswer.SelectionColor = Color.Red;
            TxbAnswer.SelectionFont = new Font(TxbAnswer.Font.FontFamily, 20);
            BtnShowAnswer.Visible = false;
            BtnCorrect.Visible = BtnIncorrect.Visible = true;
            BtnCorrect.Focus();
        }

        private void ShowAnswer(int length)
        {
            length = Math.Min(TxbAnswer.Text.Length + length, CurrentQ().Question.Answer.Length);
            TxbAnswer.Text = CurrentQ().Question.Answer.Substring(0, length);
            TxbAnswer.Select(0, TxbAnswer.Text.Length);
            TxbAnswer.SelectionColor = Color.Red;
            TxbAnswer.SelectionFont = new Font(TxbAnswer.Font.FontFamily, 20);
        }

        private void BtnCorrect_Click(object sender, EventArgs e)
        {
            if (CurrentQ().Progress == -1)
            {
                CurrentQ().Progress = 2;
                --progress_cnt[0]; ++progress_cnt[2];
            }
            else
            {
                --progress_cnt[CurrentQ().Progress];
                ++progress_cnt[++CurrentQ().Progress];
            }
            ++CurrentQ().Question.TrialCount;
            ++CurrentQ().Question.CorrectCount;
            LoadNext();
        }

        private void Control_KeyDown(object sender, KeyEventArgs e)
        {
            if (BtnCorrect.Visible)
            {
                if (e.KeyCode == Keys.Y) BtnCorrect_Click(BtnCorrect, e);
                else if (e.KeyCode == Keys.N) BtnIncorrect_Click(BtnIncorrect, e);
            }
            else
            {
                if (e.KeyCode == Keys.Y || e.KeyCode == Keys.N) BtnShowAnswer_Click(BtnShowAnswer, e);
                // n文字出現
                else
                {
                    if (Keys.D1 <= e.KeyCode && e.KeyCode <= Keys.D9)
                        ShowAnswer(e.KeyCode - Keys.D0);
                }
            }
        }

        private void FmQuiz_FormClosed(object sender, FormClosedEventArgs e)
        {
            Startup.Fm_Main.UpdateList();
        }

        private void BtnIncorrect_Click(object sender, EventArgs e)
        {
            --progress_cnt[Math.Max(0, CurrentQ().Progress)];
            ++progress_cnt[CurrentQ().Progress = 0];

            ++CurrentQ().Question.TrialCount;
            ++CurrentQ().IncorrectCount;

            LoadNext();
        }
    }
}
