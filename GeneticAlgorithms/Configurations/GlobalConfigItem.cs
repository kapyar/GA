namespace GeneticAlgorithms
{
    public class GlobalConfigItem
    {
        public string key;
        public int   iterations;
        public int   size;
        public float crossover;
        public float mutation;
        public float GG;
        public int   CF;
        public bool skip;




        public override string ToString ()
        {
            return string.Format("[GlobalConfigItem] key: {7} \n " +
                                 "{{\niterations: {0},\n " +
                                 "size {1},\n crossover {2},\n mutation {3},\n " +
                                 "GC {4},\n CF {5},\n skip {6}\n}}\n",
                                 iterations, size, crossover,mutation,GG,CF,skip,key);
        }
    }
}