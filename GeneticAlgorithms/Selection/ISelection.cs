using System.Collections.Generic;
using GeneticAlgorithms.Core;

namespace GeneticAlgorithms.Selection
{
    public interface ISelection
    {
        PopulationItem       SelectOne(List<PopulationItem> lst);
        List<PopulationItem> SelectTwo(List<PopulationItem> lst);
    }
}