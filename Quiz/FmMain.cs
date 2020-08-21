using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Quiz
{
    public partial class FmMain : Form
    {
        // 重複
        private const string STR_DISTINCT = "dup";
        // 解答の重複
        private const string STR_DISTINCT_ANSWER = "dupans";
        // お気に入り
        private const string STR_FAVORITE = "fav";
        // プレイ済み
        private const string STR_TRIED = "tri";
        // 学習済み
        private const string STR_LEARNED = "lea";

        // Question list
        public List<Question> QList { get; private set; }

        // Searched list
        private List<Question> SearchedQList { get; set; }

        private List<Question> CurrentList => IsSearching ? SearchedQList : QList;

        // 削除用リスト
        private List<Question> DeletingList { get; set; } = new List<Question>();

        // Whether to save
        private bool flgSave = true;

        // 絞り込み表示中かどうか
        private bool IsSearching => questionBindingSource.DataSource == SearchedQList;

        // 現在の検索テキスト
        public string SearchText
        {
            get => TxbSearch.Text;
            set => SearchTextSet(value);
        }

        delegate void SearchTextSet_Delegate(string str);
        private void SearchTextSet(string val)
        {
            if (InvokeRequired)
                Invoke(new SearchTextSet_Delegate(SearchTextSet), val);
            else
            {
                int index = TxbSearch.SelectionStart;
                string str = TxbSearch.Text = val.FormatForSearch();
                if (index >= 0) TxbSearch.SelectionStart = index;
                Search();
                if (SearchBox) Startup.Fm_Search.SetText(val);

                BtnDistinct.Checked = Regex.IsMatch(str, $"%{STR_DISTINCT}%", RegexOptions.IgnoreCase);
                BtnFavorite.Checked = Regex.IsMatch(str, $"%{STR_FAVORITE}%", RegexOptions.IgnoreCase);
            }
        }

        public bool SearchBox
        {
            get => Startup.Fm_Search.Visible;
            set
            {
                Startup.Fm_Search.Visible = BtnShowFmSearch.Checked = value;
            }
        }

        // FmQuizから再開可能かを設定する
        public bool Resumable
        {
            get => BtnResume.Enabled;
            set => BtnResume.Enabled = value;
        }

        public FmMain()
        {
            InitializeComponent();
        }

        private void FmMain_Load(object sender, EventArgs e)
        {
            LblInfo.Text = "";
            // Load settings
            Startup.LoadSettings();

            Resumable = Setting.GetData(Setting.DataType.StudyingList) != null;

            questionBindingSource.DataSource = QList;
        }

        public void LoadSettings()
        {
            try
            {
                QList = (List<Question>)Setting.GetData(Setting.DataType.QList);
                QList.MakeNo();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                QList = new List<Question>();
            }

            var rect = (Rectangle)Setting.GetData(Setting.DataType.FmMain_Rectangle);
            Size = rect.Size;
            Location = rect.Location;

            DgvQuestions.RowHeadersWidth = (int)Setting.GetData(Setting.DataType.RowHeader_Width);
            ClmStatement.Width = (int)Setting.GetData(Setting.DataType.ClmStatement_Width);
            ClmAnswer.Width = (int)Setting.GetData(Setting.DataType.ClmAnswer_Width);
            ClmRuby.Width = (int)Setting.GetData(Setting.DataType.ClmRuby_Width);
            ClmRate.Width = (int)Setting.GetData(Setting.DataType.ClmRate_Width);
            ClmLearn.Width = (int)Setting.GetData(Setting.DataType.ClmLearn_Width);
            ClmFinalDate.Width = (int)Setting.GetData(Setting.DataType.ClmFinalDate_Width);
            ClmFavorite.Width = (int)Setting.GetData(Setting.DataType.ClmFavorite_Width);
        }

        public void SaveSettings()
        {
            Setting.SetData(Setting.DataType.QList, QList);
            Setting.SetData(Setting.DataType.FmMain_Rectangle, new Rectangle(Location, Size));
            Setting.SetData(Setting.DataType.RowHeader_Width, DgvQuestions.RowHeadersWidth);
            Setting.SetData(Setting.DataType.ClmStatement_Width, ClmStatement.Width);
            Setting.SetData(Setting.DataType.ClmAnswer_Width, ClmAnswer.Width);
            Setting.SetData(Setting.DataType.ClmRuby_Width, ClmRuby.Width);
            Setting.SetData(Setting.DataType.ClmRate_Width, ClmRate.Width);
            Setting.SetData(Setting.DataType.ClmLearn_Width, ClmLearn.Width);
            Setting.SetData(Setting.DataType.ClmFinalDate_Width, ClmFinalDate.Width);
            Setting.SetData(Setting.DataType.ClmFavorite_Width, ClmFavorite.Width);
        }

        private void FmMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (flgSave)
                Startup.SaveSettings();
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void BtnCloseWithoutSaving_Click(object sender, EventArgs e)
        {
            flgSave = false;
            Close();
        }

        private void BtnStudyAll_Click(object sender, EventArgs e)
        {
            FmQuiz.Show(QList);
        }

        private void BtnStudyCurrent_Click(object sender, EventArgs e)
        {
            FmQuiz.Show(CurrentList);
        }

        private void BtnStudyFavorite_Click(object sender, EventArgs e)
        {
            FmQuiz.Show(QList.Where(i => i.Favorite));
        }

        private void BtnStudySelected_Click(object sender, EventArgs e)
        {
            var list = SelectedQuestions();
            if (list.Count == 0)
                MessageBox.Show("選択中の問題がありません。", "中止", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else
                FmQuiz.Show(list);
        }

        private void BtnImport_Click(object sender, EventArgs e)
        {
            // Select a file
            var ofd = new OpenFileDialog()
            {
                Filter = "CSVファイル (*.csv)|*.csv|すべてのファイル (*.*)|*.*",
                RestoreDirectory = true,
            };
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                // 絞り込みは解除する
                SearchText = "";

                var list = QuizIO.ImportCsv(ofd.FileName);

                // 末尾の空行は削除する
                if (QList.Last().IsEmpty) QList.RemoveAt(QList.Count - 1);

                if ((bool)Setting.GetData(Setting.DataType.ImportReplace))
                {
                    // 置き換え
                    if (QList.Exists(i => !i.IsEmpty))
                    {
                        if (MessageBox.Show("既存のリストに上書きしてもよろしいですか？", "確認", MessageBoxButtons.YesNo) == DialogResult.No)
                            return;
                    }
                    QList.Clear();
                    QList.AddRange(list);
                }
                else
                {
                    // QListに追加
                    QList.AddRange(list);
                }
                QList.MakeNo();
                UpdateList();
                SetInfoText($"{ofd.FileName} から{list.Count}個の問題がインポートされました");
            }
            ofd.Dispose();
        }

        private void BtnExportQList_Click(object sender, EventArgs e)
        {
            // Select a file
            var sfd = new SaveFileDialog()
            {
                Filter = "CSVファイル (*.csv)|*.csv",
                RestoreDirectory = true,
            };
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                QuizIO.ExportCsv(sfd.FileName, QList);
                SetInfoText($"リストが {sfd.FileName} にエクスポートされました");
            }
            sfd.Dispose();
        }

        private void BtnExportCurrentList_Click(object sender, EventArgs e)
        {
            // Select a file
            var sfd = new SaveFileDialog()
            {
                Filter = "CSVファイル (*.csv)|*.csv",
                RestoreDirectory = true,
            };
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                QuizIO.ExportCsv(sfd.FileName, CurrentList);
                SetInfoText($"現在のリストが {sfd.FileName} にエクスポートされました");
            }
            sfd.Dispose();
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            SetInfoText("保存しています…");
            Startup.SaveSettings();
            SetInfoText("保存されました");
        }

        private void BtnAddQuestion_Click(object sender, EventArgs e)
        {
            FmEditQuestion.AddShow();
        }

        delegate void AddQuestion_Delegate(Question q);
        public void AddQuestion(Question q)
        {
            if (InvokeRequired)
            {
                Invoke(new AddQuestion_Delegate(AddQuestion), q);
            }
            else
            {
                q.No = QList.Count + 1;
                QList.Add(q);
                // 絞り込み時は、検索後のデータにも追加する
                if (IsSearching)
                {
                    if (q.Contains(TxbSearch.Text))
                    {
                        SearchedQList.Add(q);
                        UpdateList();
                    }
                }
                else
                    UpdateList();
                FocusLastRow();
                SetInfoText($"問題を追加しました：{q.Statement.FirstN(15)}");
            }
        }

        private void BtnEditQuestion_Click(object sender, EventArgs e)
        {
            var list = SelectedQuestions();
            if (list.Count == 0)
            {
                MessageBox.Show("セルまたは行が選択されていません。", "中止", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (FmEditQuestion.EditShow(list) == DialogResult.OK)
                UpdateList();
        }

        private void BtnSetting_Click(object sender, EventArgs e)
        {
            Startup.Fm_Setting.ShowDialog();
        }

        private void DgvQuestions_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            // https://dobon.net/vb/dotnet/datagridview/drawrownumber.html
            // 列ヘッダーかどうか調べる
            if (e.ColumnIndex < 0 && e.RowIndex >= 0)
            {
                // セルを描画する
                e.Paint(e.ClipBounds, DataGridViewPaintParts.All);

                // 行番号を描画する範囲を決定する
                // e.AdvancedBorderStyleやe.CellStyle.Paddingは無視しています
                Rectangle indexRect = e.CellBounds;
                indexRect.Inflate(-2, -2);
                // 行番号を描画する
                int num = (CurrentList.Count > e.RowIndex && CurrentList[e.RowIndex].No != 0) ?
                    CurrentList[e.RowIndex].No : e.RowIndex + 1;
                TextRenderer.DrawText(e.Graphics,
                    num.ToString(),
                    e.CellStyle.Font,
                    indexRect,
                    e.CellStyle.ForeColor,
                    TextFormatFlags.Right | TextFormatFlags.VerticalCenter);
                // 描画が完了したことを知らせる
                e.Handled = true;
            }
        }

        private void StTimer_Tick(object sender, EventArgs e)
        {
            lock (StTimer)
            {
                StTimer.Stop();
                if (LblInfo.Text == (string)StTimer.Tag)
                    LblInfo.Text = "";
            }
        }

        private void SetInfoText(string str)
        {
            // LblInfoに文字列を表示し、一定時間が経過したら削除する
            LblInfo.Text = str;
            lock (StTimer)
            {
                StTimer.Tag = str;
                StTimer.Stop();
                StTimer.Start();
            }
        }

        private void DeleteInfoText(string pattern)
        {
            // matchstrに一致するなら削除する
            if (Regex.IsMatch(LblInfo.Text, pattern))
            {
                StTimer.Stop();
                LblInfo.Text = "";
            }
        }

        private void BtnStudyIgnorant_Click(object sender, EventArgs e)
        {
            if (QList.Count == 0)
                FmQuiz.Show(QList);
            else
            {
                int min = QList.Min(i => i.LearnCount);
                FmQuiz.Show(QList.Where(i => i.LearnCount == min));
            }
        }

        private void BtnStudyLowRate_Click(object sender, EventArgs e)
        {
            if (QList.Count == 0)
                FmQuiz.Show(QList);
            else
            {
                var list = QList.Where(i => i.Rate != 100).OrderBy(i => i.Rate).ToList();
                if (list.Count > 100) list.RemoveRange(100, list.Count - 100);
                FmQuiz.Show(list);
            }
        }

        delegate void UpdateList_Delegate();
        public void UpdateList()
        {
            if (InvokeRequired)
            {
                Invoke(new UpdateList_Delegate(UpdateList));
            }
            else
            {
                var index = DgvQuestions.FirstDisplayedScrollingRowIndex;
                questionBindingSource.ResetBindings(false);
                try
                {
                    DgvQuestions.FirstDisplayedScrollingRowIndex = index;
                }
                catch (Exception)
                {
                }
            }
        }

        private List<Question> SelectedQuestions()
        {
            // 選択されている問題のリストを取得
            var rowList = new List<int>();
            if (DgvQuestions.SelectedRows.Count != 0)
            {
                foreach (DataGridViewRow i in DgvQuestions.SelectedRows)
                    if (i.Index < CurrentList.Count) rowList.Add(i.Index);
            }
            else if (DgvQuestions.SelectedCells.Count != 0)
            {
                bool[] added = new bool[CurrentList.Count];
                foreach (DataGridViewCell i in DgvQuestions.SelectedCells)
                {
                    if (i.RowIndex < CurrentList.Count && !added[i.RowIndex])
                    {
                        added[i.RowIndex] = true;
                        rowList.Add(i.RowIndex);
                    }
                }
            }
            rowList.Sort();
            // 絞り込み時はSearchedQList上のデータを選択している
            return new List<Question>(rowList.Select(i => CurrentList[i]));
        }

        private void TxbSearch_Validated(object sender, EventArgs e)
        {
            SearchText = TxbSearch.Text;
        }

        private void Search()
        {
            string str = TxbSearch.Text.FormatForSearch();
            if (str == "")
            {
                questionBindingSource.DataSource = QList;

                DgvQuestions.AllowUserToAddRows = true;
            }
            else
            {
                // スペースごとに区切る
                var words = str.ToLower().Split(' ');

                IEnumerable<Question> buf = QList;

                foreach (var word in words)
                {
                    if (Regex.IsMatch(word, @"^%.+%$"))
                    {
                        var cmd = word.Substring(1, word.Length - 2).ToLower();
                        if (cmd == STR_DISTINCT)
                        {
                            // 重複抽出
                            buf = buf.GroupBy(i => i.Statement).Where(i => i.Count() > 1)
                                .SelectMany(i => i);
                        }
                        else if (cmd == "-" + STR_DISTINCT)
                        {
                            buf = buf.GroupBy(i => i.Statement).Where(i => i.Count() == 1)
                                .SelectMany(i => i);
                        }
                        else if (cmd == STR_DISTINCT_ANSWER)
                        {
                            // 解答の重複抽出
                            buf = buf.GroupBy(i => i.Answer).Where(i => i.Count() > 1)
                                .SelectMany(i => i);
                        }
                        else if (cmd == "-" + STR_DISTINCT_ANSWER)
                        {
                            // 解答の重複抽出
                            buf = buf.GroupBy(i => i.Answer).Where(i => i.Count() == 1)
                                .SelectMany(i => i);
                        }
                        else if (cmd == STR_FAVORITE)
                        {
                            // お気に入り抽出
                            buf = buf.Where(i => i.Favorite);
                        }
                        else if (cmd == "-" + STR_FAVORITE)
                        {
                            buf = buf.Where(i => !i.Favorite);
                        }
                        else if (cmd == STR_TRIED)
                        {
                            // プレイ済み
                            buf = buf.Where(i => i.TrialCount != 0);
                        }
                        else if (cmd == "-" + STR_TRIED)
                        {
                            // 未プレイ
                            buf = buf.Where(i => i.TrialCount == 0);
                        }
                        else if (cmd == STR_LEARNED)
                        {
                            // 学習済み
                            buf = buf.Where(i => i.LearnCount != 0);
                        }
                        else if (cmd == "-" + STR_LEARNED)
                        {
                            buf = buf.Where(i => i.LearnCount == 0);
                        }
                        else if (Regex.IsMatch(cmd, @"^(no|rate|count|time)[+-]?$"))
                        {
                            // ソート
                            bool asc = cmd[cmd.Length - 1] != '-';
                            if (cmd[cmd.Length - 1] == '+' || cmd[cmd.Length - 1] == '-')
                                cmd = cmd.Substring(0, cmd.Length - 1);
                            if (cmd == "no")
                                buf = asc ? buf.OrderBy(i => i.No) : buf.OrderByDescending(i => i.No);
                            else if (cmd == "rate")
                                buf = asc ? buf.OrderBy(i => i.Rate) : buf.OrderByDescending(i => i.Rate);
                            else if (cmd == "count")
                                buf = asc ? buf.OrderBy(i => i.LearnCount) : buf.OrderByDescending(i => i.LearnCount);
                            else if (cmd == "time")
                                buf = asc ? buf.OrderBy(i => i.FinalDate) : buf.OrderByDescending(i => i.FinalDate);
                        }
                        else if (Regex.IsMatch(cmd, @"^[1-9][0-9]*n([+-][1-9][0-9]*)?$"))
                        {
                            // mod抽出 (n = 0,1,2,...)
                            int mod = int.Parse(cmd.Substring(0, cmd.IndexOf('n')));
                            int surplus = cmd[cmd.Length - 1] == 'n' ? 0 :
                                int.Parse(cmd.Substring(cmd.IndexOf('n') + 1));
                            if (surplus < 0) surplus = surplus % mod + mod;

                            var buflist = new List<Question>();
                            for (int i = surplus == 0 ? mod - 1 : surplus - 1; i < buf.Count(); i += mod)
                            {
                                buflist.Add(buf.ElementAt(i));
                            }
                            buf = buflist;
                        }
                        else if (Regex.IsMatch(cmd + ",", @"^(([1-9][0-9]*(-([1-9][0-9]*)?)?),)+$"))
                        {
                            // 範囲抽出（10-23,25,35-など）
                            var indlist = new List<int>();
                            foreach (var range in cmd.Split(','))
                            {
                                if (range.Contains('-'))
                                {
                                    int a = int.Parse(range.Substring(0, range.IndexOf('-')));
                                    int b = range[range.Length - 1] == '-' ? buf.Count() :
                                        int.Parse(range.Substring(range.IndexOf('-') + 1));
                                    int min = Math.Max(Math.Min(a, b), 1);
                                    int max = Math.Min(Math.Max(a, b), buf.Count());

                                    for (int i = min - 1; i < max; ++i)
                                        if (!indlist.Contains(i)) indlist.Add(i);
                                }
                                else
                                {
                                    int a = int.Parse(range);
                                    if (1 <= a && a <= buf.Count() && !indlist.Contains(a - 1))
                                        indlist.Add(a - 1);
                                }
                            }
                            var buflist = new List<Question>();
                            foreach (var i in indlist) buflist.Add(buf.ElementAt(i));
                            buf = buflist;
                        }
                        else if (Regex.IsMatch(cmd, @"^[0-9]+d$"))
                        {
                            // n日以内に学んだものを抽出
                            int n = int.Parse(cmd.Substring(0, cmd.Length - 1));
                            DateTime min = DateTime.Today.AddDays(-n).Date;
                            buf = buf.Where(i => i.FinalDate.Date >= min.Date);
                        }
                        else if (Regex.IsMatch(cmd, @"^-[0-9]+d$"))
                        {
                            // n日以内に学んでいないものを抽出
                            int n = int.Parse(cmd.Substring(1, cmd.Length - 2));
                            DateTime min = DateTime.Today.AddDays(-n).Date;
                            buf = buf.Where(i => i.FinalDate.Date < min.Date);
                        }
                    }
                    else
                    {
                        buf = buf.Where(i => i.Contains(word)).ToList();
                    }
                }

                SearchedQList = new List<Question>(buf);
                questionBindingSource.DataSource = SearchedQList;
                DgvQuestions.AllowUserToAddRows = false;

                SetInfoText($"検索条件 \"{str}\" に一致する問題が{SearchedQList.Count}件見つかりました");
            }
        }

        private void TxbSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SearchText = TxbSearch.Text;
                e.SuppressKeyPress = true;
            }
        }

        private void DgvQuestions_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            lock (DeleteTimer)
            {
                // 削除待ちリストに追加
                DeletingList.Add(CurrentList[e.Row.Index]);
                DeleteTimer.Stop();
                DeleteTimer.Start();
                e.Cancel = true;
            }
        }

        private void DeleteTimer_Tick(object sender, EventArgs e)
        {
            DeleteTimer.Stop();
            // まとめて削除
            foreach (var i in DeletingList)
            {
                QList.Remove(i);
                if (IsSearching) SearchedQList.Remove(i);
            }
            QList.MakeNo();
            UpdateList();
        }

        private void FocusLastRow()
        {
            DgvQuestions.FirstDisplayedScrollingRowIndex = DgvQuestions.Rows.Count - 1;
        }

        private void BtnDistinct_Click(object sender, EventArgs e)
        {
            var match = Regex.Matches(SearchText, $"%{STR_DISTINCT}%", RegexOptions.IgnoreCase);
            if (match.Count != 0)
            {
                string str = SearchText;
                foreach (Match i in match)
                    str = str.Replace(i.Value, "");
                SearchText = str;
            }
            else
                SearchText += $" %{STR_DISTINCT}%";
        }

        private void BtnFavorite_Click(object sender, EventArgs e)
        {
            var match = Regex.Matches(SearchText, $"%{STR_FAVORITE}%", RegexOptions.IgnoreCase);
            if (match.Count != 0)
            {
                string str = SearchText;
                foreach (Match i in match)
                    str = str.Replace(i.Value, "");
                SearchText = str;
            }
            else
                SearchText += $" %{STR_FAVORITE}%";
        }

        private void BtnOddLines_Click(object sender, EventArgs e)
        {
            SearchText += " %2n+1%";
        }

        private void BtnEvenLines_Click(object sender, EventArgs e)
        {
            SearchText += " %2n%";
        }

        private void BtnClearSearch_Click(object sender, EventArgs e)
        {
            SearchText = "";
        }

        private void DgvQuestions_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            // 列ヘッダーかどうか調べる
            if (e.RowIndex < 0)
            {
                // ソート
                string sort = "";
                if (e.ColumnIndex < 0) sort = "No";
                else if (e.ColumnIndex == ClmRate.Index) sort = "Rate";
                else if (e.ColumnIndex == ClmLearn.Index) sort = "Count";
                else if (e.ColumnIndex == ClmFinalDate.Index) sort = "Time";

                if (sort != "")
                {
                    var match = Regex.Match(TxbSearch.Text, $"%{sort}[+-]?%$", RegexOptions.IgnoreCase);
                    if (match.Success)
                    {
                        if (match.Value.Contains('-'))
                            SearchText = TxbSearch.Text.Replace(match.Value, $"%{sort}+%");
                        else
                            SearchText = TxbSearch.Text.Replace(match.Value, $"%{sort}-%");
                    }
                    else
                        SearchText += $" %{sort}+%";
                }
            }
        }

        private void BtnShowFmSearch_Click(object sender, EventArgs e)
        {
            SearchBox = !SearchBox;
        }

        private void DgvQuestions_SelectionChanged(object sender, EventArgs e)
        {
            var cnt = SelectedQuestions().Count;
            if (cnt > 1)
            {
                SetInfoText($"{cnt}個の問題が選択されています。");
            }
            else
            {
                DeleteInfoText("個の問題が選択されています。");
            }
        }

        private void BtnResume_Click(object sender, EventArgs e)
        {
            FmQuiz.ShowResume();
        }
    }
}
