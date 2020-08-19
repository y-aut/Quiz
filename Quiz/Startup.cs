using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Quiz
{
    public static class Startup
    {
        public static FmMain Fm_Main;
        public static FmSetting Fm_Setting;
        public static FmEditQuestion Fm_AddQuestion;   // 編集時はShowDialog

        /// <summary>
        /// アプリケーションのメイン エントリ ポイントです。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            SetInstance();
            Application.Run(Fm_Main);
        }

        static void SetInstance()
        {
            Fm_Main = new FmMain();
            Fm_Setting = new FmSetting();
            Fm_AddQuestion = new FmEditQuestion(true);
        }

        public static void LoadSettings()
        {
            Setting.Load();
            Fm_Main.LoadSettings();
        }

        public static void SaveSettings()
        {
            Fm_Main.SaveSettings();

            Setting.Save();
        }
    }
}
