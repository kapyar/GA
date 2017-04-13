using System.Collections.Generic;

namespace GeneticAlgorithms.Functions
{
    public class FunctionConfig
    {
        public float Min;
        public float Max;

        public List<float> Optimus;
        public int Peaks;

        public FunctionConfig (float min, float max, List<float> optimus = null, int peaks = 0)
        {
            Min = min;
            Max = max;
            Optimus = optimus;
            Peaks = peaks;
        }
    }
}