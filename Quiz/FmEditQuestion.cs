using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Quiz
{
    public partial class FmEditQuestion : Form
    {
        bool flgCancel = true;
        bool flgRubyEditted = false;    // 読みが一度でも変更されたか
        bool flgOnMacro = false;
        List<Question> answer;    // Editの場合に変更前の問題を入れておく
        int current;            // Editで、編集中の問題のインデックス

        private bool IsAdding { get; set; } = false;

        public FmEditQuestion()
        {
            InitializeComponent();
        }

        public FmEditQuestion(bool isAdd) : this()
        {
            if (isAdd)
            {
                Text = "問題を追加";
                answer = new List<Question>();
                IsAdding = true;
                BtnOK.Text = "追加";
            }
        }

        public static void AddShow()
        {
            Startup.Fm_AddQuestion.Show();
        }

        public static DialogResult EditShow(List<Question> question)
        {
            var fm = new FmEditQuestion() {
                flgRubyEditted = (question.First().Ruby ?? "") != "",
                answer = question,
                current = 0,
            };
            fm.LblAdd.Text = question.Count == 1 ? "" : fm.LblAdd.Text.Replace("追加", "編集");
            fm.TxbStatement.Text = question.First().Statement;
            fm.TxbAnswer.Text = question.First().Answer;
            fm.TxbRuby.Text = question.First().Ruby;
            fm.ShowDialog();

            if (!fm.flgCancel && fm.current < question.Count /* Ctrl+Enterで終えた時は更新済み */)
            {
                // 最後の編集を適用する
                question[fm.current].Statement = fm.TxbStatement.Text;
                question[fm.current].Answer = fm.TxbAnswer.Text;
                question[fm.current].Ruby = fm.TxbRuby.Text;
            }
            else if (fm.current == 0)
            {
                fm.Dispose();
                return DialogResult.Cancel;
            }
            fm.Dispose();
            return DialogResult.OK;
        }

        private void BtnOK_Click(object sender, EventArgs e)
        {
            if (IsAdding)
                Add();

            flgCancel = false;
            Close();
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void FmEditQuestion_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                if (IsAdding)
                {
                    e.Cancel = true;
                    if (!(flgCancel && (TxbStatement.Text != "" || TxbAnswer.Text != "" || TxbRuby.Text != "") &&
                        MessageBox.Show("変更内容を破棄してもよろしいですか？", "確認", MessageBoxButtons.OKCancel) == DialogResult.Cancel))
                    {
                        Clear(); Hide();
                    }
                }
                else
                {
                    if (flgCancel && 
                        (TxbStatement.Text != answer[0].Statement || TxbAnswer.Text != answer[0].Answer || TxbRuby.Text != answer[0].Ruby) &&
                        MessageBox.Show("変更内容を破棄してもよろしいですか？", "確認", MessageBoxButtons.OKCancel) == DialogResult.Cancel)
                        e.Cancel = true;
                }
            }
        }

        private void TxbStatement_TextChanged(object sender, EventArgs e)
        {
            BtnOK.Enabled = TxbStatement.Text != "" && TxbAnswer.Text != "" && TxbRuby.Text != "";
        }

        private void TxbAnswer_TextChanged(object sender, EventArgs e)
        {
            BtnOK.Enabled = TxbStatement.Text != "" && TxbAnswer.Text != "" && TxbRuby.Text != "";
            if (!flgRubyEditted)
            {
                Task task = Task.Run(() =>
                {
                    InputRuby();
                });
            }
        }

        private void InputRuby()
        {
            // 50ms待っても変わらなければ実行する（2文字以上を確定した際に1文字ずつイベントが走るので）
            string str = TxbAnswer.Text;
            Thread.Sleep(50);
            if (str != TxbAnswer.Text) return;

            // 問題文に「何効果」のような表記があるならば、「ワイセンベルク効果」の読みは「ワイセンベルク」とする
            string state = TxbStatement.Text;

            state = state.Replace("いくつ", "何つ");
            if (state.Contains('何'))
            {
                int ind = state.LastIndexOf('何');
                // 前
                int offset = 1;
                while (ind - offset >= 0 && str.Contains(state.Substring(ind - offset, offset))) ++offset;
                if (--offset != 0 && str.Substring(0, offset) == state.Substring(ind - offset, offset))
                    str = str.Substring(offset);
                // 後ろ
                offset = 1;
                while (ind + offset < state.Length && str.Contains(state.Substring(ind + 1, offset))) ++offset;
                if (--offset != 0 && str.Substring(str.Length - offset, offset) == state.Substring(ind + 1, offset))
                    str = str.Substring(0, str.Length - offset);
            }
            else if (state.Contains('誰') && str.Contains('・'))
            {
                // ・以下を取り出す
                str = str.Substring(str.LastIndexOf('・') + 1);
            }

            // 漢字なしなら記号と、括弧内の文字列を抜いたものを入力する
            const string RemoveChar = @"・！？!?、。,. /\￥「」『』$-=＝~～：；:;%％";
            foreach (char c in RemoveChar) str = str.Replace(c.ToString(), "");
            if (str.Contains('(')) str = str.Substring(0, str.IndexOf('('));
            if (str.Contains('（')) str = str.Substring(0, str.IndexOf('（'));

            //if (Regex.IsMatch(str, @"^[\p{IsHiragana}\p{IsKatakana}A-zＡ-ｚ]*$"))
            str = str.GetRuby();
            {
                flgOnMacro = true;
                if (TxbRuby.InvokeRequired)
                {
                    TxbRuby.Invoke((MethodInvoker)(() => {
                        TxbRuby.Text = str;
                    }));
                }
                else
                    TxbRuby.Text = str;
                flgOnMacro = false;
            }
        }

        private void TxbRuby_TextChanged(object sender, EventArgs e)
        {
            if (!flgOnMacro)
                flgRubyEditted = true;
            BtnOK.Enabled = TxbStatement.Text != "" && TxbAnswer.Text != "" && TxbRuby.Text != "";
            if (TxbRuby.Text == "") flgRubyEditted = false;
        }

        private void Control_KeyDown(object sender, KeyEventArgs e)
        {
            if (BtnOK.Enabled && e.KeyData == (Keys.Control | Keys.Enter))
            {
                if (IsAdding)
                {
                    Add(); Clear();
                }
                else
                {
                    answer[current].Statement = TxbStatement.Text;
                    answer[current].Answer = TxbAnswer.Text;
                    answer[current].Ruby = TxbRuby.Text;
                    if (++current == answer.Count)
                    {
                        flgCancel = false; Close(); return;
                    }
                    flgRubyEditted = (answer[current].Ruby ?? "") != "";

                    TxbStatement.Text = answer[current].Statement;
                    TxbAnswer.Text = answer[current].Answer;
                    TxbRuby.Text = answer[current].Ruby;

                    // 空欄のテキストボックスにフォーカス
                    if (TxbStatement.Text == "") TxbStatement.Focus();
                    else if (TxbAnswer.Text == "") TxbAnswer.Focus();
                    else if (TxbRuby.Text == "") TxbRuby.Focus();
                    else TxbStatement.Focus();
                }
                e.SuppressKeyPress = true;
            }
        }

        private void Add()
        {
            Startup.Fm_Main.AddQuestion(new Question(TxbStatement.Text, TxbAnswer.Text, TxbRuby.Text));
        }

        private void Clear()
        {
            TxbStatement.Text = TxbAnswer.Text = TxbRuby.Text = "";
            flgRubyEditted = false; flgCancel = true;
            TxbStatement.Focus();
        }
    }
}
