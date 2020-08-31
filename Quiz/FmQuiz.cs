using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Quiz
{
    public partial class FmQuiz : Form
    {
        private StudyingList QList;
        private bool finished = false;  // 学習が完了したか
        // State
        private State _state = State.ST_START;
        // 前のState
        private State PreState { get; set; } = State.ST_START;
        // PcbMain.Image
        private Bitmap bmp;
        // 問題文の表示文字数
        private int charCnt = 0;
        // 残り思考時間[ms]
        private int restThinkingTime = 0;
        // 残り入力猶予時間[s]
        private int restInputTime = 0;
        // 解答中の文字列
        private string answer = "";
        // 文字入力ボタンの文字
        private string inputChars = "";
        // ボタンが押されているか（マウス的に）
        private bool btnPressed = false;

        // Const
        // フレーム[ms]
        private const int FRAME = 1000 / 30;
        // 問題番号の表示時間[ms]
        private const int FR_NO = 1000;
        // 問題文の1文字あたりの表示時間[ms]
        private const int FR_CHAR = 100;
        // 問題文の1文字あたりの表示時間[ms]（早送り時）
        private const int FR_CHAR_FAST = 20;
        // 思考時間[ms]
        private const int FR_THINKING = 2500;
        // 解答表示時間[ms]
        private const int FR_SHOWANSWER = 5000;
        // 1文字あたりの解答猶予時間[s]
        private const int FR_INPUT = 5;
        // 文字入力ボタンの個数
        private const int INPUT_CNT = 4;

        // Drawing Const
        // 問題番号の描画開始位置y座標[%]
        private const int NO_TOP_PER = 10;
        // 問題文の描画開始位置
        private static readonly Point PtStatement = new Point(40, 20);
        // "Q."の描画開始位置
        private static readonly Point PtQ = new Point(20, 20);
        // 右端余白
        private const int RIGHT_MAR = 20;
        // 残り時間バーの高さ
        private const int BAR_HEIGHT = 5;
        // 問題文の下端と解答のy座標間隔
        private const int STAANS_Y_MAR = 20;
        // 解答の下端と読みのy座標間隔
        private const int ANSRUB_Y_MAR = 10;
        // 正誤表示のy座標[%]
        private const int JUDGMENT_Y_PER = 70;
        // 解答中ウィンドウのy座標[%]
        private const int ANSWERING_TOP_PER = 40;
        // 解答中ウィンドウの幅[%]
        private const int ANSWERING_WIDTH_PER = 60;
        // 解答中ウィンドウの高さ[%]
        private const int ANSWERING_HEIGHT_PER = 30;
        // 解答中ウィンドウ中の残り時間表示の上端余白
        private const int ANSWERING_REST_TOP_MAR = 10;
        // 解答中ウィンドウ中の残り時間表示の右端余白
        private const int ANSWERING_REST_RIGHT_MAR = 20;
        // 文字入力ボタンのy座標[%]
        private const int INPUT_TOP_PER = 80;
        // 文字入力ボタンのサイズ
        private static readonly Size INPUT_SIZE = new Size(50, 50);
        // 文字入力ボタンの間隔
        private const int INPUT_MAR = 10;
        // 早押しボタンのy座標[%]
        private const int BUTTON_TOP_PER = 70;
        // 早押しボタンのサイズ
        private static readonly Size BUTTON_SIZE = new Size(120, 120);

        // フォント
        private static readonly Font FtNo = new Font("HGSｺﾞｼｯｸM", 16);
        private static readonly Font FtStatement = new Font("HGSｺﾞｼｯｸM", 12);
        private static readonly Font FtAnswer = FtNo;
        private static readonly Font FtRuby = FtStatement;
        private static readonly Font FtType = FtStatement;
        private static readonly Font FtTypeRestTime = FtStatement;
        private static readonly Font FtInput = new Font("HGSｺﾞｼｯｸM", 14);
        private static readonly Font FtJudgment = new Font("HGSｺﾞｼｯｸM", 50);
        // 左右中央揃え
        private static readonly StringFormat SfTopCenter = new StringFormat()
        {
            Alignment = StringAlignment.Center,
            LineAlignment = StringAlignment.Near,
        };
        // 上下左右中央揃え
        private static readonly StringFormat SfCenter = new StringFormat()
        {
            Alignment = StringAlignment.Center,
            LineAlignment = StringAlignment.Center,
        };
        // 右揃え
        private static readonly StringFormat SfTopRight = new StringFormat()
        {
            Alignment = StringAlignment.Far,
            LineAlignment = StringAlignment.Near,
        };

        // 色
        // 残り時間バーの色
        private static readonly Brush ColBar = Brushes.Red;

        public State State
        {
            get => _state;
            set
            {
                PreState = _state;
                _state = value;
            }
        }

        // 文字入力ボタンの位置
        private Rectangle InputRect(int index /* 0~3 */)
            => new Rectangle((PcbMain.Width - INPUT_MAR * 3) / 2 - INPUT_SIZE.Width * 2 + (INPUT_SIZE.Width + INPUT_MAR) * index,
                INPUT_TOP_PER * PcbMain.Height / 100, INPUT_SIZE.Width, INPUT_SIZE.Height);
        // 文字入力ボタンの文字
        private string InputChar(int index /* 0~3 */) => inputChars[index].ToString();

        // 早押しボタンの位置
        private Rectangle ButtonRect()
            => new Rectangle((PcbMain.Width - BUTTON_SIZE.Width) / 2, BUTTON_TOP_PER * PcbMain.Height / 100,
                BUTTON_SIZE.Width, BUTTON_SIZE.Height);

        public FmQuiz()
        {
            InitializeComponent();
        }

        public static void Show(IEnumerable<Question> qlist)
        {
            FmQuiz fm = new FmQuiz()
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
            FmQuiz fm = new FmQuiz()
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

        private void FmQuiz_Load(object sender, EventArgs e)
        {
            var rect = (Rectangle)Setting.GetData(Setting.DataType.FmQuiz_Rectangle);
            Location = rect.Location;
            Size = rect.Size;

            PcbMain.Size = ClientSize;
            bmp = new Bitmap(PcbMain.Width, PcbMain.Height);
            PcbMain.Image = bmp;

            Proceed();
            Draw();
            DrawTimer.Start();
        }

        private void FmQuiz_FormClosed(object sender, FormClosedEventArgs e)
        {
            Setting.SetData(Setting.DataType.FmQuiz_Rectangle, new Rectangle(Location, Size));

            DrawTimer.Stop();
            bmp.Dispose();
            bmp = null;

            Startup.Fm_Main.UpdateList();
            // 解答表示中の場合は次の問題をロード
            if (!finished && (State & State.ST_DELETE_FLAGS) == State.ST_SHOWANSWER && !QList.NextIndex())
                finished = true;
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

        public void Draw()
        {
            using (Graphics grp = Graphics.FromImage(bmp))
            {
                grp.Clear(Color.Transparent);
                grp.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;

                switch (State & State.ST_DELETE_FLAGS)
                {
                    case State.ST_NO:
                        // 問題番号を描画
                        grp.DrawString($"No. {QList.Current.Question.No}", FtNo, Brushes.Black,
                            new Rectangle(0, PcbMain.Height * NO_TOP_PER / 100, PcbMain.Width, 1000), SfTopCenter);
                        btnPressed = false;
                        // 早押しボタンを表示
                        DrawButton(grp);
                        break;

                    case State.ST_QUESTION:
                        // "Q."を描画
                        grp.DrawString("Q.", FtStatement, Brushes.Black, PtQ);
                        // 問題文を描画
                        grp.DrawString(QList.Current.Question.Statement.Substring(0, charCnt),
                            FtStatement, Brushes.Black, new Rectangle(PtStatement, new Size(PcbMain.Width - RIGHT_MAR - PtStatement.X, 1000)));
                        // 早押しボタンを表示
                        DrawButton(grp);
                        break;

                    case State.ST_WAIT:
                        // "Q."を描画
                        grp.DrawString("Q.", FtStatement, Brushes.Black, PtQ);
                        // 問題文を描画
                        grp.DrawString(QList.Current.Question.Statement,
                            FtStatement, Brushes.Black, new Rectangle(PtStatement, new Size(PcbMain.Width - RIGHT_MAR - PtStatement.X, 1000)));
                        // 残り時間バーを表示
                        grp.FillRectangle(ColBar, new Rectangle(0, 0, PcbMain.Width * restThinkingTime / FR_THINKING, BAR_HEIGHT));
                        // 早押しボタンを表示
                        DrawButton(grp);
                        break;

                    case State.ST_ANSWER:
                        // "Q."を描画
                        grp.DrawString("Q.", FtStatement, Brushes.Black, PtQ);
                        // 問題文を描画
                        if (PreState == State.ST_QUESTION)
                        {
                            grp.DrawString(QList.Current.Question.Statement.Substring(0, charCnt),
                                FtStatement, Brushes.Black, new Rectangle(PtStatement, new Size(PcbMain.Width - RIGHT_MAR - PtStatement.X, 1000)));
                        }
                        else
                        {
                            grp.DrawString(QList.Current.Question.Statement,
                                FtStatement, Brushes.Black, new Rectangle(PtStatement, new Size(PcbMain.Width - RIGHT_MAR - PtStatement.X, 1000)));
                            // 残り時間バーを表示
                            grp.FillRectangle(ColBar, new Rectangle(0, 0, PcbMain.Width * restThinkingTime / FR_THINKING, BAR_HEIGHT));
                        }
                        // 早押しボタンを表示
                        DrawButton(grp);
                        {
                            int width = PcbMain.Width * ANSWERING_WIDTH_PER / 100;
                            var rect = new Rectangle((PcbMain.Width - width) / 2, PcbMain.Height * ANSWERING_TOP_PER / 100,
                                width, PcbMain.Height * ANSWERING_HEIGHT_PER / 100);
                            // 解答ウィンドウを表示
                            grp.FillRectangle(Brushes.White, rect);
                            grp.DrawRectangle(Pens.Black, rect);
                            // 解答文字を表示
                            grp.DrawString(answer, FtType, Brushes.Black, rect, SfCenter);
                            // 残り時間を表示
                            grp.DrawString(restInputTime.ToString(), FtTypeRestTime, Brushes.Black, new Point(rect.Right - ANSWERING_REST_RIGHT_MAR, rect.Top + ANSWERING_REST_TOP_MAR));

                            // 文字入力ボタンを表示
                            for (int i = 0; i < INPUT_CNT; ++i)
                            {
                                var input = InputRect(i);
                                grp.FillRectangle(Brushes.White, input);
                                grp.DrawRectangle(Pens.Black, input);
                                grp.DrawString(InputChar(i), FtInput, Brushes.Black, input, SfCenter);
                            }
                        }
                        break;

                    case State.ST_FASTFORWARD:
                        // "Q."を描画
                        grp.DrawString("Q.", FtStatement, Brushes.Black, PtQ);
                        // 問題文を描画
                        grp.DrawString(QList.Current.Question.Statement.Substring(0, charCnt),
                            FtStatement, Brushes.Black, new Rectangle(PtStatement, new Size(PcbMain.Width - RIGHT_MAR - PtStatement.X, 1000)));
                        break;

                    case State.ST_SHOWANSWER:
                        // "Q."を描画
                        grp.DrawString("Q.", FtStatement, Brushes.Black, PtQ);
                        // 問題文を描画
                        grp.DrawString(QList.Current.Question.Statement,
                            FtStatement, Brushes.Black, new Rectangle(PtStatement, new Size(PcbMain.Width - RIGHT_MAR - PtStatement.X, 1000)));
                        {
                            // 問題文の高さを計測
                            int height = (int)grp.MeasureString(QList.Current.Question.Statement, FtStatement, PcbMain.Width - RIGHT_MAR - PtStatement.X).Height;
                            // 解答を表示
                            grp.DrawString(QList.Current.Question.Answer, FtAnswer, Brushes.Red,
                                new Rectangle(0, PtStatement.Y + height + STAANS_Y_MAR, PcbMain.Width - RIGHT_MAR, 1000), SfTopRight);
                            // 解答の高さを計測
                            int ans_height = (int)grp.MeasureString(QList.Current.Question.Answer, FtAnswer, PcbMain.Width - RIGHT_MAR).Height;
                            // 読みを表示
                            grp.DrawString(QList.Current.Question.Ruby, FtRuby, Brushes.Black,
                                new Rectangle(0, PtStatement.Y + height + STAANS_Y_MAR + ans_height + ANSRUB_Y_MAR, PcbMain.Width - RIGHT_MAR, 1000), SfTopRight);
                        }
                        break;
                }

                // 正誤を表示
                if ((State & State.ST_FLG_CORRECT) != 0)
                {
                    grp.DrawString("○", FtJudgment, Brushes.Red,
                        new Rectangle(0, PcbMain.Height * JUDGMENT_Y_PER / 100, PcbMain.Width, 1000), SfTopCenter);
                }
                else if ((State & State.ST_FLG_INCORRECT) != 0)
                {
                    grp.DrawString("×", FtJudgment, Brushes.Blue,
                        new Rectangle(0, PcbMain.Height * JUDGMENT_Y_PER / 100, PcbMain.Width, 1000), SfTopCenter);
                }
            }
            PcbMain.Refresh();
        }

        private void DrawButton(Graphics grp)
        {
            // 早押しボタンを描画
            grp.DrawImage(btnPressed ? Properties.Resources.button2 : Properties.Resources.button,
                ButtonRect());
        }

        public void Proceed()
        {
            // Stateを進める
            switch (State & State.ST_DELETE_FLAGS)
            {
                case State.ST_START:
                case State.ST_SHOWANSWER:
                    State = State.ST_NO;
                    DrawTimer.Interval = FR_NO;
                    // 次の問題をロード
                    if (!QList.NextIndex()) { finished = true; Close(); return; }
                    break;

                case State.ST_NO:
                    State = State.ST_QUESTION;
                    DrawTimer.Interval = FR_CHAR;
                    charCnt = 0;
                    break;

                case State.ST_QUESTION:
                    // 1文字ずつ表示
                    if (++charCnt > QList.Current.Question.Statement.Length)
                    {
                        State = State.ST_WAIT;
                        DrawTimer.Interval = FRAME;
                        restThinkingTime = FR_THINKING;
                    }
                    break;

                case State.ST_WAIT:
                    // 思考時間を減らす
                    restThinkingTime -= FRAME;
                    if (restThinkingTime <= 0)
                    {
                        QList.Incorrect();
                        State = State.ST_SHOWANSWER;
                        DrawTimer.Interval = FR_SHOWANSWER;
                    }
                    break;

                case State.ST_ANSWER:
                    // 猶予時間を減らす
                    if (--restInputTime == 0)
                    {
                        QList.Incorrect();
                        if (PreState == State.ST_QUESTION)
                        {
                            State = State.ST_FASTFORWARD | State.ST_FLG_INCORRECT;
                            DrawTimer.Interval = FR_CHAR_FAST;
                        }
                        else
                        {
                            State = State.ST_SHOWANSWER | State.ST_FLG_INCORRECT;
                            DrawTimer.Interval = FR_SHOWANSWER;
                        }
                    }
                    break;

                case State.ST_FASTFORWARD:
                    // 1文字ずつ表示
                    if (++charCnt > QList.Current.Question.Statement.Length)
                    {
                        State = State.ST_SHOWANSWER | (State & State.ST_FLAGS);
                        DrawTimer.Interval = FR_SHOWANSWER;
                    }
                    break;
            }
        }

        // 早押しボタンのクリックイベント
        private void Button_Click()
        {
            DrawTimer.Stop();

            answer = "";
            SetInputChars();
            State = State.ST_ANSWER;
            DrawTimer.Interval = 1000;
            restInputTime = FR_INPUT;

            Draw();
            DrawTimer.Start();
        }

        // 文字入力ボタンのクリックイベント
        private void Input_Click(int index)
        {
            DrawTimer.Stop();

            restInputTime = FR_INPUT;
            answer += InputChar(index);
            if (QList.Current.Question.Ruby[answer.Length - 1] == answer[answer.Length - 1])
            {
                if (QList.Current.Question.Ruby.Length == answer.Length)
                {
                    QList.Correct();
                    if (PreState == State.ST_QUESTION)
                    {
                        State = State.ST_FASTFORWARD | State.ST_FLG_CORRECT;
                        DrawTimer.Interval = FR_CHAR_FAST;
                    }
                    else
                    {
                        State = State.ST_SHOWANSWER | State.ST_FLG_CORRECT;
                        DrawTimer.Interval = FR_SHOWANSWER;
                    }
                }
                else
                    SetInputChars();
            }
            else
            {
                QList.Incorrect();
                if (PreState == State.ST_QUESTION)
                {
                    State = State.ST_FASTFORWARD | State.ST_FLG_INCORRECT;
                    DrawTimer.Interval = FR_CHAR_FAST;
                }
                else
                {
                    State = State.ST_SHOWANSWER | State.ST_FLG_INCORRECT;
                    DrawTimer.Interval = FR_SHOWANSWER;
                }
            }

            Draw();
            DrawTimer.Start();
        }

        private void FmQuiz_Resize(object sender, EventArgs e)
        {
            // 起動直後はInitializeComponentされていないので後で
            if (State == State.ST_START) return;
            PcbMain.Image = new Bitmap(PcbMain.Width, PcbMain.Height);
            bmp.Dispose();
            bmp = (Bitmap)PcbMain.Image;
            Draw();
        }

        private void DrawTimer_Tick(object sender, EventArgs e)
        {
            lock (bmp)
            {
                DrawTimer.Stop();
                Proceed();
                if (bmp == null) return;    // 終了
                Draw();
                DrawTimer.Start();
            }
        }

        private void PcbMain_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) return;
            // ボタンクリック判定
            if (State == State.ST_QUESTION || State == State.ST_WAIT)
            {
                if (ButtonRect().Contains(e.Location))
                {
                    btnPressed = false;
                    Button_Click();
                }
            }
            // 文字入力ボタンクリック判定
            else if (State == State.ST_ANSWER)
            {
                for (int i = 0; i < INPUT_CNT; ++i)
                {
                    if (InputRect(i).Contains(e.Location))
                    {
                        Input_Click(i);
                        return;
                    }
                }
            }
        }

        private void PcbMain_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) return;
            // ボタンプレス判定
            if (State == State.ST_NO || State == State.ST_QUESTION || State == State.ST_WAIT)
            {
                if (ButtonRect().Contains(e.Location))
                {
                    btnPressed = true;
                    Draw();
                }
            }
        }

        private void PcbMain_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) return;
            if (State == State.ST_NO || State == State.ST_QUESTION || State == State.ST_WAIT)
            {
                // ドラッグしたまま外に出る動きを処理
                if (btnPressed && !ButtonRect().Contains(e.Location))
                {
                    btnPressed = false;
                    Draw();
                }
                // ドラッグしたまま中に入る動きを処理
                else if (!btnPressed && ButtonRect().Contains(e.Location))
                {
                    btnPressed = true;
                    Draw();
                }
            }
        }

        // ひらがな・カタカナ・アルファベットのリスト
        private static readonly string Hiragana =
            "あいうえおかきくけこさしすせそたちつてとなにぬねのはひふへほまみむめもやゆよらりるれろ" +
            "わをんがぎぐげござじずぜぞだぢづでどばびぶべぼぱぴぷぺぽぁぃぅぇぉっゃゅょ";
        private static readonly string Katakana =
            "アイウエオカキクケコサシスセソタチツテトナニヌネノハヒフヘホマミムメモヤユヨラリルレロ" +
            "ワヲンヴガギグゲゴザジズゼゾダヂヅデドバビブベボパピプペポァィゥェォッャュョー";
        private static readonly string ALPHA = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        private static readonly string alpha = "abcdefghijklmnopqrstuvwxyz";
        private static readonly string WIDE_ALPHA = "ＡＢＣＤＥＦＧＨＩＪＫＬＭＮＯＰＱＲＳＴＵＶＷＸＹＺ";
        private static readonly string wide_alpha = "ａｂｃｄｅｆｇｈｉｊｋｌｍｎｏｐｑｒｓｔｕｖｗｘｙｚ";
        private static readonly string Numbers = "1234567890";
        private static readonly string WideNumbers = "１２３４５６７８９０";        

        // 現在のanswerをもとに、次の文字入力ボタンの文字を決める
        private void SetInputChars()
        {
            var c = QList.Current.Question.Ruby[answer.Length];
            inputChars = c.ToString();
            // 同じ文字種の中からランダムで選ぶ
            string str = Hiragana;
            foreach (var i in new string[] { Katakana, ALPHA, alpha, WIDE_ALPHA, wide_alpha, Numbers, WideNumbers, })
            {
                if (i.Contains(c))
                {
                    str = i;
                    break;
                }
            }
            // 一文字目に来られない文字
            if (answer == "")
                str = Regex.Replace(str, "[をぁぃぅぇぉっゃゅょヲァィゥェォッャュョー]", "");
            else
            {
                // 前の文字により候補を制限
                var pre = QList.Current.Question.Ruby[answer.Length - 1];
                // 長音記号の連続
                if (pre == 'ー') str = str.Replace("ー", "");
                // 同じ母音の連続
                if ("あいうえおアイウエオ".Contains(pre)) str = str.Replace(pre.ToString(), "");
                // 小文字の連続/長音記号+小文字
                if ("ぁぃぅぇぉっゃゅょァィゥェォッャュョー".Contains(pre))
                    str = Regex.Replace(str, "[ぁぃぅぇぉゃゅょァィゥェォャュョ]", "");
                // 促音の連続
                if ("っッ".Contains(pre)) str = Regex.Replace(str, "[っッ]", "");
                // イ段+[ゃゅょ]
                if (!"きしちにひみりキシチニヒミリ".Contains(pre))
                    str = Regex.Replace(str, "[ゃゅょャュョ]", "");
            }
            str = str.Replace(c.ToString(), "");

            Random r = new Random();
            for (int i = 1; i < INPUT_CNT; ++i)
            {
                c = str[r.Next(0, str.Length)];
                inputChars += c;
                str = str.Replace(c.ToString(), "");
            }

            // inputCharsをシャッフル
            var cs = inputChars.ToCharArray();
            cs = cs.OrderBy(i => Guid.NewGuid()).ToArray();
            inputChars = new string(cs);
        }
    }

    [Flags]
    public enum State
    {
        ST_START,       // 起動直後
        ST_NO,          // 問題番号表示中
        ST_QUESTION,    // 問題文表示中
        ST_WAIT,        // 問題文表示終了後の待機中
        ST_ANSWER,      // 解答中
        ST_FASTFORWARD, // 早送りで問題文表示中
        ST_SHOWANSWER,  // 解答表示中

        ST_DELETE_FLAGS = (1 << 5) - 1, // フラグ削除用マスク
        ST_FLAGS = ~ST_DELETE_FLAGS,    // フラグ取り出し用マスク

        ST_FLG_CORRECT = 1 << 5,    // 正解
        ST_FLG_INCORRECT = 1 << 6,  // 不正解
    }
}
