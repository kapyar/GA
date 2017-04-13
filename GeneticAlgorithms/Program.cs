using System.Collections.Generic;
using GeneticAlgorithms.Core;
using GeneticAlgorithms.Crossover.Implementation;
using GeneticAlgorithms.Functions;
using GeneticAlgorithms.Method.Implementation;
using GeneticAlgorithms.Selection.Implementation;

namespace GeneticAlgorithms
{
    internal class Program
    {

        private static void Main(string[] args)
        {
            var globalConfigItem = Config.GetConfigByName("first");
            var funcConfig       = new FunctionConfig(0, 1, new List<float> {0.1f, 0.3f, 0.5f, 0.9f});
            var userData         = new UserData(funcConfig, 1);
            var selection        = new Tournament2Selection();
            var crossover        = new TwoPointCrossover();
            var method           = new ClosestOfRandomSelected();
            var function         = new EqualMaxima();

            var gen = new Genetic(globalConfigItem, userData, function,
                                  crossover,        selection, method);

            gen.Start();
        }

        private static void Deba (GlobalConfigItem globalConfigItem)
        {
            var funcConfig       = new FunctionConfig(0, 1, new List<float> {0.1f, 0.3f, 0.5f, 0.9f});
            var userData         = new UserData(funcConfig, 1);
            var selection        = new Tournament2Selection();
            var crossover        = new TwoPointCrossover();
            var method           = new ClosestOfRandomSelected();
            var function         = new EqualMaxima();

            var gen = new Genetic(globalConfigItem, userData, function,
                                  crossover,        selection, method);

            gen.Start();
        }
    }
}