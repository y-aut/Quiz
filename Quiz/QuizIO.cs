using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Quiz
{
    public static class QuizIO
    {
        public static List<Question> ImportCsv(string path)
        {
            List<Question> ans = new List<Question>();

            // 読み込みたいCSVファイルを指定して開く
            try
            {
                StreamReader sr = new StreamReader(path, (Encoding)Setting.GetData(Setting.DataType.Encoding));
                {
                    // 各行がどの項目に対応するか
                    int[] cor = { -1, -1, -1, -1, -1, -1, -1, -1 };
                    // cor[0]:問題文, cor[1]:解答, cor[2]:読み, cor[3]:出題回数, cor[4]:正解回数, cor[5]:学習回数, cor[6]:学習日時, cor[7]:お気に入り

                    // 見出しがあるか
                    bool headExists = false;

                    // Headline
                    string head = sr.ReadLine();
                    {
                        var lists = head.Split(',').ToList();
                        for (int i = 0; i < lists.Count; ++i)
                        {
                            // 英語でないかつ文字数が10文字以上なら、見出しではない
                            if (lists[i].Length >= 10 && Regex.IsMatch(lists[i], @"^[^A-z]$")) continue;

                            if (Regex.IsMatch(lists[i], @"(日|時|date|time)", RegexOptions.IgnoreCase))
                                cor[6] = i;
                            else if (Regex.IsMatch(lists[i], @"(出題|質問|挑戦|tr).*(数|co)", RegexOptions.IgnoreCase))
                                cor[3] = i;
                            else if (Regex.IsMatch(lists[i], @"(正解|正答|cor|suc).*(数|co)", RegexOptions.IgnoreCase))
                                cor[4] = i;
                            else if (Regex.IsMatch(lists[i], @"(学習|習得|lea|mas|cle).*(数|co)", RegexOptions.IgnoreCase))
                                cor[5] = i;
                            else if (Regex.IsMatch(lists[i], @"(読み|よみ|ふり|かな|仮名|るび|ルビ|ruby|入力|inp)", RegexOptions.IgnoreCase))
                                cor[2] = i;
                            else if (Regex.IsMatch(lists[i], @"(解答|ans|a\.)", RegexOptions.IgnoreCase))
                                cor[1] = i;
                            else if (Regex.IsMatch(lists[i], @"(問題|sen|q)", RegexOptions.IgnoreCase))
                                cor[0] = i;
                            else if (Regex.IsMatch(lists[i], @"(お気に|星|fav|star)", RegexOptions.IgnoreCase))
                                cor[7] = i;
                        }

                        if (cor[0] != -1 || cor[1] != -1 || cor[2] != -1)
                        {
                            headExists = true;
                        }
                        for (int i = 0; i < cor.Length; ++i)
                            if (cor[i] == -1) cor[i] = i;
                    }
                
                    // 末尾まで繰り返す
                    while (!sr.EndOfStream)
                    {
                        string line;
                        if (!headExists)
                        {
                            line = head;
                            headExists = true;
                        }
                        else line = sr.ReadLine();

                        List<string> lists = line.Split(',').ToList();

                        for (int i = 0; i < lists.Count; ++i)
                        {
                            string trimst = lists[i].TrimStart();

                            // 先頭のダブルクォーテーションが奇数個なら括弧、偶数個ならダブルクォーテーション
                            int cnt = 0;
                            foreach (var c in trimst)
                            {
                                if (c == '"') ++cnt;
                                else break;
                            }
                            if (cnt % 2 == 0)
                            {
                                lists[i] = lists[i].Replace("\"\"", "\"");
                            }
                            else
                            {
                                lists[i] = trimst.Substring(1).Replace("\"\"", "\"");

                                string trimend = lists[i].TrimEnd();

                                // もう一回ダブルクォーテーションが出てくるまで要素を結合
                                while (trimend[trimend.Length - 1] != '"' && lists.Count > i + 1)
                                {
                                    lists[i] += "," + lists[i + 1].Replace("\"\"", "\"");
                                    trimend = lists[i].TrimEnd();

                                    // 結合したら要素を削除する
                                    lists.RemoveAt(i + 1);
                                }
                                lists[i] = trimend.Substring(0, trimend.Length - 1);
                            }
                        }

                        {
                            string statement = "", answer = "", ruby = "";
                            int trial = 0, correct = 0, learn = 0;
                            bool favorite = false;
                            DateTime time = DateTime.MinValue;
                            if (lists.Count > cor[0]) statement = lists[cor[0]];
                            if (lists.Count > cor[1]) answer = lists[cor[1]];
                            if (lists.Count > cor[2]) ruby = lists[cor[2]];
                            if (lists.Count > cor[3]) int.TryParse(lists[cor[3]], out trial);
                            if (lists.Count > cor[4]) int.TryParse(lists[cor[4]], out correct);
                            if (lists.Count > cor[5]) int.TryParse(lists[cor[5]], out learn);
                            if (lists.Count > cor[6]) DateTime.TryParse(lists[cor[6]], out time);
                            if (lists.Count > cor[7]) bool.TryParse(lists[cor[7]], out favorite);
                            var q = new Question(statement, answer, ruby, trial, correct, learn, time, favorite);
                            if (!q.IsEmpty) ans.Add(q);
                        }
                    }

                    sr.Close();
                    sr.Dispose();
                }
            }
            catch (Exception e)
            {
                e.ShowErr();
                return new List<Question>();
            }

            return ans;
        }

        public static void ExportCsv(string path, IReadOnlyCollection<Question> qlist)
        {
            try
            {
                StreamWriter sw = new StreamWriter(path, false, (Encoding)Setting.GetData(Setting.DataType.Encoding));
                {
                    // Headline
                    sw.WriteLine("問題,解答,読み,出題回数,正解回数,学習回数,最終学習日時,お気に入り");

                    foreach (var i in qlist)
                        sw.WriteLine($"{i.Statement.ToCSV()},{i.Answer.ToCSV()},{i.Ruby.ToCSV()},{i.TrialCount},{i.CorrectCount},{i.LearnCount},{i.FinalDate},{i.Favorite}");

                    sw.Close();
                    sw.Dispose();
                }
            }
            catch (Exception e)
            {
                e.ShowErr();
            }
        }

        private static string ToCSV(this string str)
        {
            str = str.Replace("\"", "\"\"");
            if (str.Contains(',')) return $"\"{str}\"";
            else return str;
        }

        public static void ToQLists(string str, out List<List<Question>> lists)
        {
            lists = new List<List<Question>>();

            // splitしたとき、後ろに\rが残っていると面倒くさい
            str = str.Trim().Replace('\r', '\n');
            str = Regex.Replace(str, "\n[\n]+", "\n");

            // LINE (10:10 sender Statement \n 10:10 sender Answer)
            {
                List<Question> buf = new List<Question>();
                bool isState = true;

                Question q = null;
                foreach (var line in str.Split('\n'))
                {
                    var cmds = line.Split(' ');
                    if (cmds.Length >= 3)
                    {
                        var val = line.Substring(line.IndexOf(' ') + 1);
                        val = val.Substring(val.IndexOf(' ') + 1);
                        if (isState)
                            q = new Question() { Statement = val };
                        else
                        {
                            q.Answer = val;
                            buf.Add(q);
                        }
                        isState = !isState;
                    }
                }

                if (buf.Count != 0)
                    lists.Add(buf);
            }

            // Normal (Statement \n Answer)
            {
                List<Question> buf = new List<Question>();
                bool isState = true;

                Question q = null;
                foreach (var line in str.Split('\n'))
                {
                    if (isState)
                        q = new Question() { Statement = line };
                    else
                    {
                        q.Answer = line;
                        buf.Add(q);
                    }
                    isState = !isState;
                }

                if (buf.Count != 0)
                    lists.Add(buf);
            }

        }
    }
}
