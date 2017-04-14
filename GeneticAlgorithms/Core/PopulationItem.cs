using static System.String;

namespace GeneticAlgorithms.Core
{
    public class PopulationItem
    {
        public string Genom { get; }
        public float Fitness { get; }

        public PopulationItem(string genom, float fitness) {
            Genom   = genom;
            Fitness = fitness;
        }

        public PopulationItem(string genom) {
            Genom = genom;
            Fitness = 0f;
        }

        public override string ToString ()
        {
            return Format("[Genome : {0} , Fitness: {1}]", Genom, Fitness);
        }
    }
}