using System;
using System.Collections.Generic;

namespace GeneticAlgorithms.Method.Implementation
{
    public class ClosestOfRandomSelected : IMethod
    {
        private readonly Random _rand = new Random();

        public List<int> GetPositions(int amount, int maxPos)
        {
            List<int> toChangePos = new List<int>(amount);

            while (toChangePos.Count != amount)
            {
                var randomPos = _rand.Next(maxPos);

                if (!toChangePos.Contains(randomPos)) {
                    toChangePos.Add(randomPos);
                }
            }
            return toChangePos;
        }
    }
}