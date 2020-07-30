using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace Museum
{
    class Program
    {
        const string InputFileName = "visits_very_large.txt";

        static void Main(string[] args)
        {
            ISolver solver = new Solver(); // Create our problem solver.
            solver.Solve(new Tuple<TimeSpan, TimeSpan>[0]); // Jit the algorithm aka warm up.

            var visitingRanges = GetVisitingTimesFromFile($"Data\\{InputFileName}").ToArray(); // Read input data from file.

            GC.WaitForPendingFinalizers(); // Ensure GC has finished cleaning up.

            var sw = Stopwatch.StartNew();
            var result = solver.Solve(visitingRanges);
            sw.Stop();

            Console.WriteLine($"Number of entries:{visitingRanges.Length}");
            Console.WriteLine("All time ranges with the most concurrent visitors:\n");
            foreach (var subResult in result.Item1)
            {
                Console.WriteLine($"{FormatTime(subResult.Item1)}-{FormatTime(subResult.Item2)};{result.Item2}");
            }
            Console.WriteLine($"\nTime spent {sw.Elapsed}\n");

            Console.ReadKey();
        }

        static IEnumerable<Tuple<TimeSpan, TimeSpan>> GetVisitingTimesFromFile(string fileName)
        {
            using (var reader = File.OpenText(fileName))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    string[] split = line.Split(',');
                    TimeSpan from = ParseDateTime(split[0]);
                    TimeSpan to = ParseDateTime(split[1]);
                    if (to < from) to = to.Add(TimeSpan.FromDays(1));
                    yield return Tuple.Create(from, to);
                }
            }
        }

        static TimeSpan ParseDateTime(string input)
        {
            string[] split = input.Split(':');
            return new TimeSpan(int.Parse(split[0]), int.Parse(split[1]), 0); // We don't really care about the date part.
        }

        static string FormatTime(TimeSpan time)
        {
            return time.ToString("hh\\:mm");
        }
    }
}