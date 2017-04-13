using System.Collections.Generic;
using System.Linq;
using static System.Math;

namespace GeneticAlgorithms.Functions
{
    public class EqualMaxima : IFunction
    {
        public override float run(List<float> gens)
        {
            var result = gens.Sum(gen => (float) Pow(Sin(5 * PI * gen), 6));

            return 1f/gens.Count * result;
        }
    }
}