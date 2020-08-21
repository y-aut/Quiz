using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace Quiz
{
    public static class Setting
    {
        public enum DataType
        {
            QList,                  // List<Question>
            StudyingList,           // StudyingList
            RowHeader_Width,        // int
            ClmStatement_Width,     // int
            ClmAnswer_Width,        // int
            ClmRuby_Width,          // int
            ClmRate_Width,          // int
            ClmLearn_Width,         // int
            ClmFinalDate_Width,     // int
            ClmFavorite_Width,      // int
            FmMain_Rectangle,       // Rectangle

            // Setting
            Encoding,               // Encoding
            ImportReplace,          // bool

        }

        // 保存先のファイル名
        static readonly string FileName =
            Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + "\\setting.config";

        public static string GetFileName() => FileName;

        // 設定をハッシュテーブルに保存
        private static Hashtable Datas;

        // 設定を読み書き
        public static void SetData(DataType dt, object value)
        {
            Datas[dt.ToString()] = value;
        }
        public static object GetData(DataType dt) => Datas[dt.ToString()];

        public static void Save()
        {
            // binファイルに書き込む
            // BinaryFormatterオブジェクトを作成
            BinaryFormatter bf = new BinaryFormatter();
            // ファイルを開く
            FileStream fs;
            if (File.Exists(FileName))
                fs = new FileStream(FileName, FileMode.Create);
            else
                fs = File.Create(FileName);
            // Seriarizeし、binファイルに保存する
            bf.Serialize(fs, Datas);
            // 閉じる
            fs.Close();
        }

        public static void Load()
        {
            if (File.Exists(FileName))
            {
                // binファイルから読み込む
                // BinaryFormatterオブジェクトを作成
                BinaryFormatter bf = new BinaryFormatter();
                // ファイルを開く
                FileStream fs = new FileStream(FileName, FileMode.Open);
                // binファイルから読み込み、逆シリアル化する
                try
                {
                    Datas = (Hashtable)bf.Deserialize(fs);
                    Arrange();
                }
                catch (Exception)
                {
                    Initialize();
                }
                // 閉じる
                fs.Close();
            }
            else
                Initialize();
        }

        // 設定を初期化
        private static void Initialize()
        {
            Datas = new Hashtable
            {
                { DataType.QList.ToString(), new List<Question>() },
                { DataType.StudyingList.ToString(), null },
                { DataType.RowHeader_Width.ToString(), 30 },
                { DataType.ClmStatement_Width.ToString(), 400 },
                { DataType.ClmAnswer_Width.ToString(), 200 },
                { DataType.ClmRuby_Width.ToString(), 200 },
                { DataType.ClmRate_Width.ToString(), 100 },
                { DataType.ClmLearn_Width.ToString(), 100 },
                { DataType.ClmFinalDate_Width.ToString(), 100 },
                { DataType.ClmFavorite_Width.ToString(), 50 },
                { DataType.FmMain_Rectangle.ToString(), new Rectangle(0, 0, 500, 300) },

                { DataType.Encoding.ToString(), new UTF8Encoding() },
                { DataType.ImportReplace.ToString(), false },
            };
        }

        // 設定を整理
        private static void Arrange()
        {
            List<string> setMem = new List<string>(Enum.GetNames(typeof(DataType)));
            // 古いデータをコピー
            Hashtable oldDatas = new Hashtable(Datas);
            Initialize();

            foreach (string i in setMem)
                if (oldDatas.ContainsKey(i))
                    Datas[i] = oldDatas[i];
        }

    }
}
