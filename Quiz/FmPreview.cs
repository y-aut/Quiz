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
    public partial class FmPreview : Form
    {
        public List<List<Question>> QListPatterns;
        private bool flgCancel = true;

        public List<Question> CurrentList => QListPatterns[(int)NudPattern.Value - 1];

        public FmPreview()
        {
            InitializeComponent();
        }

        public static bool Show(List<List<Question>> patterns, out List<Question> questions)
        {
            FmPreview fm = new FmPreview()
            {
                QListPatterns = patterns,
            };
            fm.NudPattern.Maximum = patterns.Count;
            fm.ShowDialog();
            questions = fm.CurrentList;
            var ans = !fm.flgCancel;
            fm.Dispose();

            return ans;
        }

        private void BtnOK_Click(object sender, EventArgs e)
        {
            flgCancel = false;
            Close();
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void NudPattern_ValueChanged(object sender, EventArgs e)
        {
            LoadList();
        }

        private void LoadList()
        {
            questionBindingSource.DataSource = CurrentList;
            LblRange.Text = $"（1～{QListPatterns.Count}）";
        }

        private void FmPreview_Load(object sender, EventArgs e)
        {
            LoadList();

            DgvQuestions.RowHeadersWidth = (int)Setting.GetData(Setting.DataType.RowHeader_Width);

            var clmWidths = (ColumnData<int>)Setting.GetData(Setting.DataType.Column_Width);
            var clmIndexes = (ColumnData<int>)Setting.GetData(Setting.DataType.Column_Index);
            foreach (DataGridViewColumn clm in DgvQuestions.Columns)
            {
                clm.Width = clmWidths[(ColumnEnum)Enum.Parse(typeof(ColumnEnum), clm.Name)];
                clm.DisplayIndex = clmIndexes[(ColumnEnum)Enum.Parse(typeof(ColumnEnum), clm.Name)];
            }
        }

        private void DgvQuestions_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
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
    }
}
