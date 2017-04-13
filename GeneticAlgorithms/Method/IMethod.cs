using System.Collections.Generic;

namespace GeneticAlgorithms.Method
{
    public interface IMethod
    {
        List<int> GetPositions(int amount, int maxPos);
    }
}