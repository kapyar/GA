using System;
using System.Collections.Generic;
using System.Linq;

namespace GeneticAlgorithms.Functions
{
    public class F16 : IFunction
    {
        public override float run(List<float> gens)
        {

            return (float) gens.Sum(gen => Math.Pow(Math.E,
                                                    -2 * Math.Log(2, Math.E) * ((gen - 0.1f) / 0.8f) *
                                                    Math.Pow(Math.Sin(5 * Math.PI * gen), 6)));
        }
    }
}