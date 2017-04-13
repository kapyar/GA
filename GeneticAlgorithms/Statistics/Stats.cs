namespace GeneticAlgorithms.Statistics
{
    public class Stats
    {
        public float PreviousMean { get; set; } = 0f;
        public int MeanCounter    { get; set; } = 0;
        public int Nfe            { get; set; }

        public void IncMeanCounter() {
            MeanCounter++;
        }

        public void IncNfe()
        {
            Nfe++;
        }

    }
}