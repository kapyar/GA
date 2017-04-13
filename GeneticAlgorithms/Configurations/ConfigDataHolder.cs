using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace GeneticAlgorithms
{
    public class ConfigDataHolder
    {
        private static readonly string Path = "Configurations/data/global.json";


        public List<GlobalConfigItem> items;

        public ConfigDataHolder ()
        {
            using (var r = File.OpenText(Path))
            {
                var json = r.ReadToEnd();
                items = JsonConvert.DeserializeObject<List<GlobalConfigItem>>(json);
            }
        }

    }
}