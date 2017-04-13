using System.Collections.Generic;
using GeneticAlgorithms.Core;

namespace GeneticAlgorithms.Crossover
{
    public interface ICrossover
    {
        List<PopulationItem> Crossover(PopulationItem mother, PopulationItem father);
    }
}