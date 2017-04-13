using System;
using System.Collections.Generic;
using GeneticAlgorithms.Core;

namespace GeneticAlgorithms.Crossover.Implementation
{
    public class TwoPointCrossover : ICrossover
    {
        private readonly Random _rand = new Random();

        public List<PopulationItem> Crossover(PopulationItem mother, PopulationItem father)
        {
            var len = mother.Genom.Length;

            var ca = _rand.Next(len);
            var cb = _rand.Next(len);

            if (ca > cb) {
                var tmp = cb;
                cb = ca;
                ca = tmp;
            }

            var son      = new PopulationItem(DoCrossover(father.Genom, mother.Genom, ca, cb));
            var daughter = new PopulationItem(DoCrossover(mother.Genom, father.Genom, ca, cb));

            return new List<PopulationItem> {son, daughter};
        }

        private string DoCrossover(string p1, string p2, int ca, int cb)
        {
            return p1.Substring(0, ca) + p2.Substring(ca, cb-ca) + p1.Substring(cb, p1.Length - cb);
        }
    }
}