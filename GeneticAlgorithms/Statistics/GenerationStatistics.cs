namespace GeneticAlgorithms.Statistics
{
    /// <summary>
    /// Use for internal usage to know when we need to stop changing our
    /// algorithm to modify populations
    /// </summary>
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