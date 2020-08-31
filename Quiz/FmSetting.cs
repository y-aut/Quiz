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
    public partial class FmSetting : Form
    {
        bool flgSave = false;

        public FmSetting()
        {
            InitializeComponent();
        }

        private void FmSetting_Shown(object sender, EventArgs e)
        {
            flgSave = false;
            LoadSettings();
        }

        private void FmSetting_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (flgSave) SaveSettings();
        }

        private void LoadSettings()
        {
            if (Setting.GetData(Setting.DataType.Encoding).GetType() == typeof(UTF8Encoding))
                RdbUTF_8.Checked = true;
            else
                RdbSJIS.Checked = true;
            if ((bool)Setting.GetData(Setting.DataType.ImportReplace))
                RdbImportReplace.Checked = true;
            else
                RdbImportAdd.Checked = true;
            if ((bool)Setting.GetData(Setting.DataType.UseFmQuiz))
                RdbStudyFmQuiz.Checked = true;
            else
                RdbStudyFmStudy.Checked = true;
        }

        private void SaveSettings()
        {
            Setting.SetData(Setting.DataType.Encoding, RdbUTF_8.Checked ? new UTF8Encoding() : Encoding.GetEncoding("Shift_JIS"));
            Setting.SetData(Setting.DataType.ImportReplace, RdbImportReplace.Checked);
            Setting.SetData(Setting.DataType.UseFmQuiz, RdbStudyFmQuiz.Checked);
        }

        private void BtnOK_Click(object sender, EventArgs e)
        {
            flgSave = true;
            Close();
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
