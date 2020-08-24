using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Quiz
{
    [Serializable]
    public class Question
    {
        public int No { get; set; } = 0;    // 1から
        public string Statement { get; set; }
        public string Answer { get; set; }
        public string Ruby { get; set; }
        public int TrialCount { get; set; } = 0;
        public int CorrectCount { get; set; } = 0;
        public int Rate => TrialCount == 0 ? 0 : (int)(CorrectCount * 100.0 / TrialCount);
        public int LearnCount { get; set; } = 0;
        public DateTime FinalDate { get; set; } = DateTime.MinValue;
        public bool Favorite { get; set; } = false;

        // 問題文、答え、読みに含まれるか
        public bool Contains(string lowStr) {
            return Statement.ToLower().Contains(lowStr) ||
                Answer.ToLower().Contains(lowStr) ||
                Ruby.ToLower().Contains(lowStr);
        }

        public Question()
        {
            Statement = Answer = Ruby = "";
        }

        public Question(string statement, string answer, string ruby)
        {
            Statement = statement;
            Answer = answer;
            Ruby = ruby;
        }

        public Question(string statement, string answer, string ruby, int trial, int correct, int learn, DateTime date, bool favorite)
        {
            Statement = statement;
            Answer = answer;
            Ruby = ruby;
            TrialCount = trial;
            CorrectCount = trial != 0 ? correct : 0;
            LearnCount = learn;
            FinalDate = date;
            Favorite = favorite;
        }

        public bool IsOK => Statement != "" && Answer != "" && Ruby != "";
        public bool IsEmpty => Statement == "" && Answer == "" && Ruby == "";
    }

    [Serializable]
    public class QuestionProg
    {
        public Question Question { get; set; }
        // When seeing this question for the first time, this value is -1
        public int Progress { get; set; }
        public int IncorrectCount { get; set; } = 0;

        public QuestionProg(Question q, int pro)
        {
            Question = q;
            Progress = pro;
        }

        public QuestionProg(Question q)
        {
            Question = q;
            Progress = -1;
        }
    }
}
