using System.Collections.Generic;

namespace GeneticAlgorithms.Functions
{
    public abstract class IFunction
    {
        public abstract float run(List<float> gens);

    }
}