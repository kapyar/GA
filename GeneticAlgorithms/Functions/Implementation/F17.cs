using System;
using System.Collections.Generic;
using System.Linq;

namespace GeneticAlgorithms.Functions
{
    public class F17 : IFunction
    {
        public override float run(List<float> gens)
        {

            var result = (float)gens.Sum(gen => Math.Pow(Math.Sin(5*Math.PI*(Math.Pow(gen, 075f)-0.05f)), 6));
//            var result = gens.Sum(gen => Math.Pow(Math.Sin(5 * Math.PI * (Math.Pow(gen, 0.75f) - 0.05))), 6);

            return 1f / gens.Count * result;

        }
    }
}