namespace GeneticAlgorithms.Statistics
{
    public class GenerationStatistics {

        public float Min   { get; }
        public float Max   { get; }
        public float Mean  { get; }
        public float Stdev { get; }

        public GenerationStatistics(float max, float min, float mean, float stdev)
        {
            this.Max   = max;
            this.Min   = min;
            this.Mean  = mean;
            this.Stdev = stdev;
        }
    }
}