using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Quiz
{
    public partial class FmMain : Form
    {
        // Question list
        public QuestionList QList { get; private set; }

        // Searched list
        private QuestionList SearchedQList { get; set; }

        // Whether to save
        private bool flgSave = true;

        // Address of the questions file
        private readonly string savePath = Directory.GetCurrentDirectory() + "\\quiz.xml";

        // 絞り込み表示中かどうか
        private bool IsSearching => questionBindingSource.DataSource == SearchedQList;

        public FmMain()
        {
            InitializeComponent();
        }

        private void FmMain_Load(object sender, EventArgs e)
        {
            LblInfo.Text = "";
            // Load settings
            Startup.LoadSettings();

            questionBindingSource.DataSource = QList;
        }

        public void LoadSettings()
        {
            QList = (QuestionList)Setting.GetData(Setting.DataType.QList);

            var rect = (Rectangle)Setting.GetData(Setting.DataType.FmMain_Rectangle);
            Size = rect.Size;
            Location = rect.Location;

            DgvQuestions.RowHeadersWidth = (int)Setting.GetData(Setting.DataType.RowHeader_Width);
            ClmStatement.Width = (int)Setting.GetData(Setting.DataType.ClmStatement_Width);
            ClmAnswer.Width = (int)Setting.GetData(Setting.DataType.ClmAnswer_Width);
            ClmRuby.Width = (int)Setting.GetData(Setting.DataType.ClmRuby_Width);
            ClmRate.Width = (int)Setting.GetData(Setting.DataType.ClmRate_Width);
            ClmLearn.Width = (int)Setting.GetData(Setting.DataType.ClmLearn_Width);
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

        private void StudySelected_Click(object sender, EventArgs e)
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
                UpdateList();
                SetInfoText($"{ofd.FileName} から{list.Count}個の問題がインポートされました");
            }
            ofd.Dispose();
        }

        private void BtnExport_Click(object sender, EventArgs e)
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
                Invoke(new AddQuestion_Delegate(AddQuestion), new object[] { q });
            }
            else
            {
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
                TextRenderer.DrawText(e.Graphics,
                    (e.RowIndex + 1).ToString(),
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

        private void StudyIgnorant_Click(object sender, EventArgs e)
        {
            if (QList.Count == 0)
                FmQuiz.Show(QList);
            else
            {
                int min = QList.Min(i => i.LearnCount);
                FmQuiz.Show(QList.Where(i => i.LearnCount == min));
            }
        }

        private void StudyLowRate_Click(object sender, EventArgs e)
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

        public void UpdateList()
        {
            if (InvokeRequired)
            {
                Invoke((MethodInvoker)(() =>
                {
                    questionBindingSource.ResetBindings(false);
                }));
            }
            else
                questionBindingSource.ResetBindings(false);
        }

        private QuestionList SelectedQuestions()
        {
            // 選択されている問題のリストを取得
            var rowList = new List<int>();
            if (DgvQuestions.SelectedRows.Count != 0)
            {
                foreach (DataGridViewRow i in DgvQuestions.SelectedRows)
                    rowList.Add(i.Index);
            }
            else if (DgvQuestions.SelectedCells.Count != 0)
            {
                bool[] added = new bool[QList.Count];
                foreach (DataGridViewCell i in DgvQuestions.SelectedCells)
                {
                    if (i.RowIndex < QList.Count && !added[i.RowIndex])
                    {
                        added[i.RowIndex] = true;
                        rowList.Add(i.RowIndex);
                    }
                }
            }
            rowList.Sort();
            // 絞り込み時はSearchedQList上のデータを選択している
            if (IsSearching)
                return new QuestionList(rowList.Select(i => SearchedQList[i]));
            else
                return new QuestionList(rowList.Select(i => QList[i]));
        }

        private void TxbSearch_Validated(object sender, EventArgs e)
        {
            string str = TxbSearch.Text;
            if (str == "")
            {
                questionBindingSource.DataSource = QList;

                DgvQuestions.AllowUserToAddRows = true;
            }
            else
            {
                SearchedQList = new QuestionList(QList.Where(i => i.Contains(str)));
                questionBindingSource.DataSource = SearchedQList;

                DgvQuestions.AllowUserToAddRows = false;

                SetInfoText($"\"{TxbSearch.Text}\" を含む問題が{SearchedQList.Count}件見つかりました");
            }
        }

        private void TxbSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) TxbSearch_Validated(sender, e);
        }

        private void DgvQuestions_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            // 絞り込み時は、元のデータからも削除する
            if (IsSearching)
            {
                QList.Remove(SearchedQList[e.Row.Index]);
            }
        }

    }
}
