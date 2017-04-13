using System;
using System.Collections.Generic;
using System.Text;
using static System.Random;

namespace GeneticAlgorithms
{
    public class Utils
    {
        private const int bitSize = 30;
        private static readonly Random _rand = new Random();

        private static string ToBinary(float f)
        {
            byte[] b = BitConverter.GetBytes(f);
            var i    = BitConverter.ToInt32(b, 0);

            return Convert.ToString(i, 2);
        }

        private static float ToFloat(string s)
        {
            var i    = Convert.ToInt32(s, 2);
            byte[] b = BitConverter.GetBytes(i);

            return BitConverter.ToSingle(b, 0);
        }

        public static List<float> Deserialize(int dimension, string entity)
        {
            List<float> res = new List<float>();

            for (var i = 0; i < dimension; i++)
            {
               res.Add(ToFloat(entity.Substring(i * bitSize, bitSize)));
            }

            return res;
        }

        public static string Serialize(int dimension, float min, float max)
        {
            var builder = new StringBuilder();

            for (var i = 0; i < dimension; i++)
            {
                builder.Append(ToBinary(RandomFloat(min, max)));
            }

            return builder.ToString();
        }

        private static float RandomFloat(float min, float max)
        {
            var val = (float)_rand.NextDouble() * (max - min) + min;
            return val;
        }

    }
}