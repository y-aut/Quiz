using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Quiz
{
    [Serializable]
    public class QuestionList : List<Question>
    {
        public QuestionList() { }

        public QuestionList(QuestionList src) : base(src) { }

        public QuestionList(IEnumerable<Question> list) : base(list) { }

        //// Save questions to an xml file
        //public void SaveTo(string path)
        //{
        //    XmlSerializer se = new XmlSerializer(typeof(QuestionList));
        //    StreamWriter sw = new StreamWriter(path, false, new System.Text.UTF8Encoding(false));
        //    se.Serialize(sw, this);
        //    sw.Close();
        //}

        //// Load questions from an xml file
        //public static QuestionList LoadFrom(string path)
        //{
        //    XmlSerializer se = new XmlSerializer(typeof(QuestionList));
        //    StreamReader sr = new StreamReader(path, new System.Text.UTF8Encoding(false));
        //    QuestionList ans = (QuestionList)se.Deserialize(sr);
        //    sr.Close();
        //    return ans;
        //}
    }
}
