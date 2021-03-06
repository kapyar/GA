﻿using System.Collections.Generic;
using System.Linq;

namespace GeneticAlgorithms
{
    public class Config
    {
        private static ConfigDataHolder _data;

        static Config ()
        {
            _data = new ConfigDataHolder();
        }


        public static List<GlobalConfigItem> Configs { get { return _data.items; } }

        public static GlobalConfigItem GetConfigByName(string key)
        {
            return _data.items.FirstOrDefault(s => s.key == key);
        }
    }

}