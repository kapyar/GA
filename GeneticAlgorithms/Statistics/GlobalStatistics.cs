using System;
using System.Collections.Generic;
using System.Linq;

namespace GeneticAlgorithms.Statistics
{
    public class GlobalStatistics
    {
        public List<Stats> Statses;

        public GlobalStatistics()
        {
            Statses = new List<Stats>();
        }

        public void GenerateReport ()
        {
            string str = $"All peaksFound: someValue\n" +
                         $"Avg peaks found: {Statses.Average(s=>s.NumberOfPeaks)}\n" +
                         $"Avg NFE: {Statses.Average(s=>s.NFE)}\n" +
                         $"Avg PR:  {Statses.Average(s=>s.PeakRatio)}\n" +
                         $"Avg PA:  {Statses.Average(s=>s.PeakAcuracy)}\n" +
                         $"Avg DA:  {Statses.Average(s=>s.DistanceAcuracy)}\n"+

                         $"Best NFE: {Statses.Max(s=>s.NFE)}\n" +
                         $"Best PR:  {Statses.Max(s=>s.PeakRatio)}\n" +
                         $"Best PA:  {Statses.Max(s=>s.PeakAcuracy)}\n" +
                         $"Best DA:  {Statses.Max(s=>s.DistanceAcuracy)}\n";

            Console.WriteLine(str);
            Console.WriteLine();
            Console.WriteLine();
        }
    }
}