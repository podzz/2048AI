using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2048
{
    public class FitnessEvaluator
    {
        private float[] costs_;
        private Feature[] features_;

        public FitnessEvaluator()
        {
            costs_ = new float[2];
            features_ = new Feature[2];

            //maximize value of pieces
            costs_[0] = 1.0f;
            //minimize number of pieces
            costs_[1] = 0.0f;

            features_[0] = new Feature1();
            features_[1] = new Feature2();
        }

        public float compute(int[,] board)
        {
            float res = 0.0f;

            for (int i = 0; i < 2; i++)
            {
                res += features_[i].compute(board) * costs_[i];
            }

            return res;
        }
    }
}
