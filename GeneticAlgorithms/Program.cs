using System;
using System.Collections.Generic;
using GeneticAlgorithms.Core;
using GeneticAlgorithms.Crossover.Implementation;
using GeneticAlgorithms.Functions;
using GeneticAlgorithms.Selection.Implementation;
using GeneticAlgorithms.Statistics;

namespace GeneticAlgorithms
{
    internal class Program
    {
        private const bool TEST = false;

        private static void Main(string[] args)
        {

//            List<Action> functions = new List<Action>{F15, F16};

            if (TEST)
            {
                var globalStats = new GlobalStatistics();

                var config = Config.GetConfigByName("first");
                var dim    = 1;

                var genetic = F17(config, dim);
                var startPopulation = genetic.CreateFirstGeneration();

                for (int i = 0; i < 10; i++)
                {
                    globalStats.Statses.Add(genetic.Start(startPopulation));
                }

                globalStats.GenerateReport();

                return;
            }

            List<int> dimensions = new List<int>{1,2,3,5};

            foreach (var dim in dimensions)
            {
                Console.WriteLine($"Dimension : {dim}");

                foreach (var config in Config.Configs)
                {
                    var globalStats = new GlobalStatistics();

                    var genetic = F15(config, dim);
                    var startPopulation = genetic.CreateFirstGeneration();

                    for (var i = 0; i < 10; i++)
                    {
                        var localStats = genetic.Start(startPopulation);

                        Console.WriteLine($"===========Local Stats ({i})===========");
                        Console.WriteLine(localStats);
                        Console.WriteLine($"============END local stats {i}========");

                        globalStats.Statses.Add(localStats);
                    }

                    Console.WriteLine($"Report for  {config.key} dim {dim}");
                    globalStats.GenerateReport();
                    Console.WriteLine("-------------------------------------------------------------------------------");
                }
            }
        }



        private static Genetic F15(GlobalConfigItem globalConfigItem, int dimentsion)
        {
            var funcConfig       = new FunctionConfig(0, 1, new List<float> {0.1f, 0.3f, 0.5f, 0.7f, 0.9f});
            var userData         = new UserData(funcConfig, dimentsion);
            var selection        = new Tournament2Selection();
            var crossover        = new TwoPointCrossover();
            var function         = new F15();


            var gen = new Genetic(globalConfigItem, userData, function,
                                  crossover,        selection);
            return gen;
        }

        private static Genetic F16(GlobalConfigItem globalConfigItem, int dimention)
        {
            var funcConfig       = new FunctionConfig(0, 1, new List<float> {0.1f, 0.3f, 0.5f, 0.7f, 0.9f});
            var userData         = new UserData(funcConfig, dimention);
            var selection        = new Tournament2Selection();
            var crossover        = new TwoPointCrossover();
            var function         = new F16();


            var gen = new Genetic(globalConfigItem, userData, function,
                                  crossover,        selection);
            return gen;
        }
        private static Genetic F17(GlobalConfigItem globalConfigItem, int dimention)
        {
            var funcConfig       = new FunctionConfig(0, 1, new List<float> {0.08f,0.247f,0.451f,0.681f,0.934f});
            var userData         = new UserData(funcConfig, dimention);
            var selection        = new Tournament2Selection();
            var crossover        = new TwoPointCrossover();
            var function         = new F17();


            var gen = new Genetic(globalConfigItem, userData, function,
                                  crossover,        selection);
            return gen;
        }
    }
}