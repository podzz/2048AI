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
            costs_ = new float[3];
            features_ = new Feature[3];

            //maximize value of pieces
            costs_[0] = 10.0f;
            //minimize number of pieces
            costs_[1] = 0.0f;
            costs_[2] = 1.0f;

            features_[0] = new Feature1();
            features_[1] = new Feature2();
            features_[2] = new Feature3();
        }

        public float compute(int[,] board)
        {
            float res = 0.0f;

            for (int i = 0; i < 3; i++)
            {
                res += features_[i].compute(board) * costs_[i];
            }

            return res;
        }
    }
}
