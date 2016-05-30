using System;
using System.Linq;
using static System.Console;

namespace Alphabet
{
    class Program
    {
        static void Main(string[] args)
        {
            char[] alphabet = "qwertyuiopasdfghjklzxcvbnm".ToCharArray();
            string[] input = { "aas", "aasta", "year", "jahr", "god" };

            string[] inputCopy = input.ToArray();

            SortByAlphabet(input, alphabet);

            WriteLine(alphabet);
            for (int i = 0; i < input.Length; i++)
                Write($"{inputCopy[i]}\t\t\t{input[i]}\n");
            ReadKey();
        }

        private static void SortByAlphabet(string[] input, char[] alphabet)
        {
            Array.Sort(input, (x, y) =>
            {
                if (x.Length == 0 && y.Length == 0) return 0; // We dont sort empty strings.
                if (x.Length == 0) return -1; // Empty string is before non-empty string.
                if (y.Length == 0) return +1; // -.-

                for (int i = 0; i < x.Length; i++)
                {                    
                    if (i > y.Length - 1) return +1; // Shorter string comes first.
                    int a = Array.IndexOf(alphabet, x[i]);
                    int b = Array.IndexOf(alphabet, y[i]);
                    if (a < b) return -1;
                    if (b < a) return +1;
                }

                if (x.Length == y.Length) return 0; // We dont sort equal strings.
                return -1; // Shorter string comes first.                                
            });
        }
    }
}
