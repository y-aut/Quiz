using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quiz
{
    // DataGridViewの各列に対応するデータ
    [Serializable]
    public class ColumnData<T>
    {
        private readonly T[] Vals;

        public ColumnData() { Vals = new T[(int)ColumnEnum.NB]; }
        public ColumnData(params T[] vals)
        {
            Vals = new T[(int)ColumnEnum.NB];
            for (int i = 0; i < vals.Length; ++i)
                Vals[i] = vals[i];
        }

        public T this[ColumnEnum clm]
        {
            get => Vals[(int)clm];
            set => Vals[(int)clm] = value;
        }
    }

    public enum ColumnEnum
    {
        ClmStatement,
        ClmAnswer,
        ClmRuby,
        ClmRate,
        ClmLearn,
        ClmFinalDate,
        ClmFavorite,
        NB
    }
}
