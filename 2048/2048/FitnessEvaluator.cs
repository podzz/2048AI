using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace _2048
{
    public class FitnessEvaluator
    {
        private float[] costs_;
        private Feature[] features_;

        public FitnessEvaluator()
        {
            costs_ = new float[6];
            features_ = new Feature[6];

            //maximize value of pieces
            costs_[0] = 0.0f;
            //minimize number of pieces
            costs_[1] = 1.0f;
            costs_[2] = 1.0f; // OK maxvalue
            costs_[3] = 1.0f; // OK mono2
            costs_[4] = 0.1f; // OK smooth
            costs_[5] = 2.7f; // OK empty

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
