using System;
using System.Collections.Generic;
using GeneticAlgorithms.Core;

namespace GeneticAlgorithms.Selection.Implementation
{
    public class Tournament2Selection : ISelection
    {
        private readonly Random _rand = new Random();

        public PopulationItem SelectOne(List<PopulationItem> lst)
        {
            var a = lst[_rand.Next(lst.Count)];
            var b = lst[_rand.Next(lst.Count)];

            return Optimize(a.Fitness, b.Fitness) ? a : b;
        }

        /// <summary>
        /// Use this method when you want to to use crossover
        /// </summary>
        /// <param name="lst"></param>
        /// <returns></returns>
        public List<PopulationItem> SelectTwo(List<PopulationItem> lst) {

            var ToReturn = new List<PopulationItem> {SelectOne(lst), SelectOne(lst)};

            return ToReturn;
        }

        private bool Optimize(float a, float b) {
            return a >= b;
        }
    }
}