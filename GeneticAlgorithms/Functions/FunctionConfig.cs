using System.Collections.Generic;

namespace GeneticAlgorithms.Functions
{
    public class FunctionConfig
    {
        public float Min;
        public float Max;

        public List<float> Optimus;
        public int Peaks => Optimus?.Count ?? 0;

        public FunctionConfig (float min, float max, List<float> optimus = null)
        {
            Min = min;
            Max = max;
            Optimus = optimus;
        }
    }
}