using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Quiz
{
    // 学習中の問題リストを管理
    [Serializable]
    public class StudyingList
    {
        int i1, i2; // QLists[i1][i2]を学習中

        const int Q_COUNT = 10; // 一度に学習する問題の最大数
        readonly List<List<QuestionProg>> QLists;    // 学習を完了したものは削除していく

        bool addIndex = false;  // NextIndex()でindexを進めるか。初回ロード時はfalseにしておく。

        // 問題数
        private readonly int[] progress_cnt = { 0, 0, 0 };

        public int ProgressCount(int progress) => progress_cnt[progress];

        public QuestionProg Current => QLists[i1][i2];

        public bool IsEmpty => ProgressCount(0) == 0;

        public StudyingList(IEnumerable<Question> qlist)
        {
            QLists = new List<List<QuestionProg>>();
            int cnt = 0;

            // Shuffle
            int[] ind = new int[qlist.Count()];
            for (int i = 0; i < ind.Length; ++i) ind[i] = i;
            ind = ind.OrderBy(i => Guid.NewGuid()).ToArray();

            for (int i = 0; i < ind.Length; ++i)
            {
                if (qlist.ElementAt(ind[i]).IsOK)
                {
                    if (cnt == 0) QLists.Add(new List<QuestionProg>());
                    QLists.Last().Add(new QuestionProg(qlist.ElementAt(ind[i])));
                    if (++cnt == Q_COUNT) cnt = 0;
                    ++progress_cnt[0];
                }
            }

            i1 = 0; i2 = 0;
        }

        public StudyingList(List<Question> qlist, StudyingList src)
        {
            // FmMainのQListへの参照を設定しながら、srcをもとに復元
            int failcnt = 0;
            QLists = new List<List<QuestionProg>>();
            foreach (var list in src.QLists)
            {
                var buf = new List<QuestionProg>();
                foreach (var q in list)
                {
                    if (qlist.Count >= q.Question.No &&
                        qlist[q.Question.No - 1].Statement == q.Question.Statement)
                    {
                        buf.Add(new QuestionProg(qlist[q.Question.No - 1], q.Progress));
                        ++progress_cnt[Math.Max(q.Progress, 0)];
                    }
                    else
                        ++failcnt;
                }
                QLists.Add(buf);
            }

            // 2になったら消去されるのでここだけはコピー
            progress_cnt[2] = src.progress_cnt[2];

            i1 = Math.Min(src.i1, QLists.Count - 1);
            i2 = Math.Min(src.i2, QLists[i1].Count - 1);

            if (failcnt != 0)
                MessageBox.Show($"{failcnt}個の問題の復元に失敗しました。", "再開");
        }

        public void Correct()
        {
            if (Current.Progress == -1)
            {
                Current.Progress = 2;
                --progress_cnt[0]; ++progress_cnt[2];
            }
            else
            {
                --progress_cnt[Current.Progress];
                ++progress_cnt[++Current.Progress];
            }
            ++Current.Question.TrialCount;
            ++Current.Question.CorrectCount;
            Current.Question.FinalDate = DateTime.Now;

        }

        public void Incorrect()
        {
            --progress_cnt[Math.Max(0, Current.Progress)];
            ++progress_cnt[Current.Progress = 0];

            ++Current.Question.TrialCount;
            ++Current.IncorrectCount;
            Current.Question.FinalDate = DateTime.Now;
        }

        // If there are no questions to learn, return false
        public bool NextIndex()
        {
            if (!addIndex)
            {
                // First state
                addIndex = true;
                return true;
            }
            if (Current.Progress == 2)
            {
                ++Current.Question.LearnCount;
                QLists[i1].RemoveAt(i2);
                if (i2 < QLists[i1].Count) return true;
            }
            else if (i1 + 1 < QLists.Count && Current.Progress == 1)
            {
                // 1回正解したら先送り
                QLists[i1 + 1].Add(Current);
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
            else if (i1 + 1 < QLists.Count && QLists[i1].Count == 1 && Current.Progress == 0)
            {
                // 連続で同じ問題になるのを避ける
                QLists[i1 + 1].Add(Current);
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
    }

}
