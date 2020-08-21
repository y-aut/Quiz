using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.VisualBasic;
using NMeCab;

namespace Quiz
{
    public static class AddMethod
    {
        // 読み仮名を取得
        public static string GetRuby(this string str)
        {
            var tagger = MeCabTagger.Create();
            var result = tagger.Parse(str);
            tagger.Dispose();

            string ans = "";

            // 棒 名詞,一般,*,*,*,*,棒,ボウ,ボー
            var lines = result.Split('\n');
            for (int i = 0; i < lines.Length - 2; ++i)
            {
                var word = lines[i].Substring(0, lines[i].IndexOf('\t'));
                var words = lines[i].Split(',');
                string ruby;
                // 単語がカタカナでないならひらがなに変換
                if (!Regex.IsMatch(word, @"^\p{IsKatakana}+$"))
                    ruby = Strings.StrConv(words[words.Length - 2], VbStrConv.Hiragana);
                else
                    ruby = word;
                if (ruby == "*") ruby = word;
                ans += ruby;
            }

            return ans;
        }

        // はじめのn文字だけ取り出す
        public static string FirstN(this string str, int length)
        {
            if (str.Length > length) return str.Substring(0, length) + "…";
            else return str;
        }

        // QuestionにNoをふる
        public static void MakeNo(this List<Question> qlist)
        {
            for (int i = 0; i < qlist.Count; ++i) qlist[i].No = i + 1;
        }

        // 検索条件の文字列をフォーマット
        public static string FormatForSearch(this string str)
        {
            str = str.Trim().Replace('　', ' ').Replace('\n', ' ').Replace('\r', ' ');
            str = Regex.Replace(str, " [ ]+", " ");
            return str;
        }
    }
}
