using GeneticAlgorithms.Functions;

namespace GeneticAlgorithms
{
    public class UserData
    {
        public int   Dimension       { get; set; }
        public float PreviousMean    { get; set; }
        public int   MeanCounter     { get; set; }
        public int   Nfe             { get; set; }//number function evaluation

        public FunctionConfig Config { get; set; }

        public UserData (FunctionConfig config, int dimension)
        {
            this.Dimension = dimension;
            this.Config    = config;
        }

    }
}