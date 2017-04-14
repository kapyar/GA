namespace GeneticAlgorithms.Statistics
{
    public class Stats
    {
        public float PreviousMean        { get; set; } = 0f;
        public int   MeanCounter         { get; set; } = 0;
        public int   NFE                 { get; set; }//
        public int   NumberOfPeaks       { get; set; }//
        public int   KnownNumberOfPeaks  { get; set; } = -1;//

        public float PeakRatio
        {
            get
            {
                if (KnownNumberOfPeaks != -1)
                {
                    return (float)NumberOfPeaks / KnownNumberOfPeaks;
                }

                return 1;
            }
        }

        public float PeakAcuracy         { get; set; }//
        public float DistanceAcuracy     { get; set; }//


        public void IncMeanCounter() {
            MeanCounter++;
        }

        public void IncNfe()
        {
            NFE++;
        }


        public override string ToString ()
        {
            string str = $"NFE: {NFE}\n" +
                         $"NOF: {NumberOfPeaks}\n" +
                         $"PR:  {PeakRatio}\n" +
                         $"PA:  {PeakAcuracy}\n" +
                         $"DA:  {DistanceAcuracy}";

            return str;
        }
    }
}