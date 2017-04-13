using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using GeneticAlgorithms.Crossover;
using GeneticAlgorithms.Functions;
using GeneticAlgorithms.Method;
using GeneticAlgorithms.Selection;
using GeneticAlgorithms.Statistics;

namespace GeneticAlgorithms.Core
{
    public class Genetic
    {

        public bool DEBUG = true;
        public List<PopulationItem> Entities = new List<PopulationItem>();

        private readonly GlobalConfigItem _globalConfig;
        private readonly IFunction        _function;
        private readonly ICrossover       _crossover;
        private readonly ISelection       _selection;
        private readonly IMethod          _method;
        private readonly UserData        _userData;

        private readonly Stats  _stats;
        private readonly Random _random;



        public Genetic (GlobalConfigItem globalConfigItem, UserData userData, IFunction func,
                        ICrossover cross,  ISelection selection, IMethod method
        )
        {
            _globalConfig = globalConfigItem;
            _function     = func;
            _crossover    = cross;
            _selection    = selection;
            _userData     = userData;
            _method       = method;

            _stats        = new Stats();
            _random       = new Random();

            Log();
        }



        #region Interface

        public void Start ()
        {
            Train(CreateFirstGeneration());
        }

        public List<PopulationItem> CreateFirstGeneration ()
        {
            List<PopulationItem> items = new List<PopulationItem>();

            for (var i = 0; i < _globalConfig.size; i++)
            {
                items.Add(Seed());
            }

            return items;
        }

        #endregion



        #region Function

        private float Fitness(PopulationItem entity)
        {
            List<float> x_params = Utils.Deserialize(_userData.Dimension, entity.Genom);
            var fitness = _function.run(x_params);
            return fitness;
        }

        #endregion



        private void Train(List<PopulationItem> startPopulation)
        {
            Entities = startPopulation;

            for (var i = 0; i < _globalConfig.iterations; ++i)
            {
                List<PopulationItem> popRepresentation = new List<PopulationItem>();

                foreach (var entity in Entities)
                {
                    _stats.IncNfe();
                    popRepresentation.Add(new PopulationItem(entity.Genom, Fitness(entity)));
                }
                //optimiz criterial  + - o.Fitness
                List<PopulationItem> popSorted =
                    new List<PopulationItem>(popRepresentation.OrderByDescending(o => o.Fitness));


//                Console.WriteLine("Desceding: ");
//                best.ForEach(s => Console.WriteLine(s));

                var max = popSorted.Max(o => o.Fitness);
                var mean = popSorted.Average(o => o.Fitness);
                var stdev = Math.Sqrt(popSorted.Average(v => Math.Pow(v.Fitness - mean, 2)));

                Console.WriteLine("Max   :" + max);
                Console.WriteLine("Mean  :" + mean);
                Console.WriteLine("Stdev :" + stdev);

                var res = CheckGeneration(popSorted,
                                          new GenerationStatistics(
                                                                   popSorted [0].Fitness,
                                                                   popSorted [popSorted.Count - 1].Fitness,
                                                                   mean,
                                                                   (float) stdev));


                var isFinished = !res || i == _globalConfig.iterations - 1;

                if (isFinished)
                {
                    List<PopulationItem> seeds = GetSeeds(popSorted);

                    foreach (var seed in seeds)
                    {
                        List<float> floats = Utils.Deserialize(_userData.Dimension, seed.Genom);

                        Console.Write(string.Format("[seed :  fitness : {0}", seed.Fitness));

                        foreach (var t in floats)
                        {
                            Console.Write(string.Format("{0} ", t));
                        }

                        Console.Write("]");
                        Console.WriteLine();
                    }


                    Console.WriteLine("NFE: {0}", _stats.Nfe);
                    Console.WriteLine($"Peaks amount: {seeds.Count}");

                    if (_userData.Config.Peaks != 0)
                    {
                        Console.WriteLine(
                                          "Effective number: " +
                                          seeds.Count * 1.0f / Math.Pow(_userData.Config.Peaks, _userData.Dimension));
                    }
                    else
                    {
                        Console.WriteLine("Effective number: 0");
                    }

                    CalculatePeakAndDistanceAccuracy(seeds);
                    break;
                }

                var GapIndex = (int) (_globalConfig.size * _globalConfig.GG);

                Console.WriteLine($"GapIndex :{GapIndex}");


                // todo check whether copy is really needed
                List<PopulationItem> newPop = popSorted.Select(item => new PopulationItem(item.Genom, item.Fitness))
                                                      .ToList();


                List<PopulationItem> BestPart  = newPop.GetRange(0, GapIndex);
                List<PopulationItem> Offspring = new List<PopulationItem>();


                foreach (var item in BestPart)
                {
                    if ((float) _random.NextDouble() < _globalConfig.crossover)
                    {

                        List<PopulationItem> parents = _selection.SelectTwo(BestPart);
                        List<PopulationItem> crossover;

                        do {
                            crossover = _crossover.Crossover(parents[0], parents[1]);
                        } while (!CheckInRange(crossover[0].Genom) || !CheckInRange(crossover[1].Genom));


                        foreach (var cross in crossover)
                        {
                            Offspring.Add(MutateOrNot(cross));
                        }

                    } else {
                        Offspring.Add(MutateOrNot(_selection.SelectOne(BestPart)));
                    }
                }
                    /*  TODO    : my algorithm goes here
                        @author : (kaplya)
                        @date   : 4/13/17
                    */
                foreach (var t in Offspring)
                {
                    var toChangePos    = _method.GetPositions(_globalConfig.CF, newPop.Count);
                    var maxSimilarity  = HammingDistance(newPop[toChangePos[0]].Genom, t.Genom);
                    var mostSimilarPos = toChangePos[0];

                    for (var j = 0; j < _globalConfig.CF; j++)
                    {
                        if (HammingDistance(newPop[toChangePos[j]].Genom, t.Genom) < maxSimilarity) {
                            mostSimilarPos = toChangePos[j];
                        }
                    }
                    newPop.Insert(mostSimilarPos, t);
                }

                Entities = newPop;
            }
        }



        #region Utils

        private bool CheckGeneration(List<PopulationItem> pop, GenerationStatistics stats) {
            // stop running once we've reached the solution
            if (Math.Abs(_stats.PreviousMean - stats.Mean) < 0.0001) {
                _stats.IncMeanCounter();
            } else {
                _stats.MeanCounter = 0;
            }

            _stats.PreviousMean = stats.Mean;
            return _stats.MeanCounter <= 5;
        }

        private PopulationItem Seed() {
            return new PopulationItem(Utils.Serialize(_userData.Dimension, _userData.Config.Min, _userData.Config.Max));
        }

        private List<PopulationItem> GetSeeds(List<PopulationItem> pop) {

            List<PopulationItem> seeds = new List<PopulationItem>();


            foreach (var item in pop)
            {
                var found = seeds.Select(seed => GetEuklidianDistance(item, seed)).Any(distance => distance <= 0.01f);

                if (!found)
                {
                    seeds.Add(item);
                }

                return seeds;

            }

            return seeds;
        }


        private float GetEuklidianDistance(PopulationItem i1, PopulationItem i2) {
            List<float> p1 = Utils.Deserialize(_userData.Dimension, i1.Genom);
            List<float> p2 = Utils.Deserialize(_userData.Dimension, i2.Genom);

            double sum = p1.Select((t, i) => t - p2 [i]).Aggregate<float, double>(0.0f, (current, dp) => current + dp * dp);
            return (float) Math.Sqrt(sum);
        }

        private float GetEuklidianDistance(List<float> p1, List<float> p2)
        {
            double sum = p1.Select((t, i) => t - p2 [i]).Aggregate<float, double>(0.0f, (current, dp) => current + dp * dp);

            return (float) Math.Sqrt(sum);
        }

        private int HammingDistance(string left, string right) {

            if (left == null || right == null)
            {
                Console.WriteLine("strings cant be null");
            }

            if (left.Length != right.Length) {
                Console.WriteLine("Must be the same Lenghts");
            }

            return left.Where((t, i) => t != right [i]).Count();

        }

        private bool CheckInRange(string genom)
        {
            List<float> floats = Utils.Deserialize(_userData.Dimension, genom);
            return floats.All(t => !(t < _userData.Config.Min) && !(t > _userData.Config.Max));
        }

        private PopulationItem MutateOrNot(PopulationItem entity)
        {
            // applies mutation based on mutation probability
            return _random.NextDouble() <= _globalConfig.mutation ? Mutate(entity) : entity;
        }


        private PopulationItem Mutate(PopulationItem entity)
        {
            var genom = entity.Genom;
            string res;

            do {
                var index    = _random.Next(genom.Length);
                var mutation = genom[index] == '1' ? '0' : '1';
                res = genom.Substring(0, index) + mutation + genom.Substring(index + 1);
            } while (!CheckInRange(res));

            return new PopulationItem(res);
        }

        // todo create args for dimensions
        private void CalculatePeakAndDistanceAccuracy(List<PopulationItem> seeds)
        {
            if (_userData.Config.Peaks != 0) {
                float peakAccuracy = 0f;
                float distAccuracy = 0f;
                var args = GenerateArgs(_userData.Config.Optimus, _userData.Dimension);


                foreach (var opt in args)
                {
                    PopulationItem nearestSeed = getNearestSeed(opt, seeds);

                    List<float> nearest = Utils.Deserialize(_userData.Dimension, nearestSeed.Genom);
                    peakAccuracy += Math.Abs(_function.run(opt) - nearestSeed.Fitness);
                    distAccuracy += GetEuklidianDistance(opt, nearest);
                if (DEBUG)
                {
                    Console.Write("nearest: ");
                    foreach (var v in opt)
                    {
                        Console.Write(v + " ");
                    }
                    Console.Write(" : ");

                    foreach (var v in nearest)
                    {
                        Console.Write(v + " ");
                    }
                    Console.WriteLine();
                }
                }
                Console.WriteLine("Peak peakAccuracy: " + peakAccuracy);
                Console.WriteLine("Dist peakAccuracy: " + distAccuracy);
            }
        }

        private List<List<float>> GenerateArgs(List<float> args, int maxLength)
        {
            List<List<float>> result = new List<List<float>>();
            generateArgs(new List<float>(), result, args, maxLength);
            return result;
        }

        private void generateArgs(List<float> prefix, List<List<float>> result, List<float> args, int maxLength) {
            if (prefix.Count == maxLength) result.Add(prefix);
            else
            {
                foreach (var arg in args)
                {
                    List<float> newPrefix = new List<float>(prefix) {arg};
                    generateArgs(newPrefix, result, args, maxLength);
                }
            }
        }

        private PopulationItem getNearestSeed(List<float> arg, List<PopulationItem> seeds)
        {
            PopulationItem nearest = seeds[0];
            float distance = GetEuklidianDistance(arg, Utils.Deserialize(_userData.Dimension, seeds[0].Genom));

            foreach (PopulationItem seed in seeds)
            {
                float tempDist = GetEuklidianDistance(arg, Utils.Deserialize(_userData.Dimension, seed.Genom));

                if (tempDist < distance) {
                    nearest = seed;
                    distance = tempDist;
                }
            }
            return nearest;
        }

        #endregion


        #region Debug

        [Conditional("DEBUG")]
        private void Log ()
        {
            Console.WriteLine(_function);
        }

        #endregion

    }
}