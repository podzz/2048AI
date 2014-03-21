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
        private float[] results_;
        private Thread[] threads_;

        private void compute_thread(Feature feature, int[,] board, ref float res, float cost)
        {
            res = feature.compute(board) * cost;
        }

        public FitnessEvaluator()
        {
            costs_ = new float[6];
            features_ = new Feature[6];
            results_ = new float[6] { 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f };

            //maximize value of pieces
            costs_[0] = 1.0f;
            //minimize number of pieces
            costs_[1] = 0.0f;
            costs_[2] = 1.0f;
            costs_[3] = 1.0f;
            costs_[4] = 0.1f;
            costs_[5] = 2.7f;

            features_[0] = new Feature1();
            features_[1] = new Feature2();
            features_[2] = new Feature3();
            features_[3] = new Feature4();
            features_[4] = new Feature5();
            features_[5] = new Feature6();
        }

        public float compute(int[,] board)
        {
            float res = 0.0f;

            for (int i = 1; i < 6; i++)
            {
                res += features_[i].compute(board) * costs_[i];
            }

            return res;
        }
    }
}
