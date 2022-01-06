using System;
using System.Linq;

namespace IBank.UnitTests.Utils
{
    public static class RandomGeneratorUtils
    {
        public static string GenerateString(
            int length,
            string pattern = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ"
        )
        {
            var rand = new Random();
            return new string(Enumerable.Repeat(pattern, length)
                .Select(s => s[rand.Next(s.Length)]).ToArray());
        }

        public static string GenerateValidNumber(int length)
        {
            return RandomGeneratorUtils.GenerateString(
                length,
                "0123456789"
            );
        }

        public static String GenerateCpf()
        {
            int sum = 0, rest = 0;
            int[] multiplier1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplier2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };

            Random rnd = new Random();
            string seed = rnd.Next(100000000, 999999999).ToString();

            for (int i = 0; i < 9; i++)
                sum += int.Parse(seed[i].ToString()) * multiplier1[i];

            rest = sum % 11;
            if (rest < 2)
                rest = 0;
            else
                rest = 11 - rest;

            seed = seed + rest;
            sum = 0;

            for (int i = 0; i < 10; i++)
                sum += int.Parse(seed[i].ToString()) * multiplier2[i];

            rest = sum % 11;

            if (rest < 2)
                rest = 0;
            else
                rest = 11 - rest;

            seed = seed + rest;
            return seed;
        }
    }
}