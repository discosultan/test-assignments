using System;
using System.Collections.Generic;

namespace Museum
{
    interface ISolver
    {
        Tuple<List<Tuple<TimeSpan, TimeSpan>>, int> Solve(Tuple<TimeSpan, TimeSpan>[] visitingRanges);
    }

    class Solver : ISolver // Fast when dealing with LARGE number of ranges.
    {
        private const int MinutesInHour = 60;
        private const int MinutesInDay = 24 * MinutesInHour;

        public Tuple<List<Tuple<TimeSpan, TimeSpan>>, int> Solve(Tuple<TimeSpan, TimeSpan>[] visitingRanges)
        {
            // Flatten all the entering and leaving times into a single list with additional marker to determine
            // whether we are dealing with an enterer or leaver.
            var times = new KeyValuePair<int, bool>[visitingRanges.Length*2];
            for (int i = 0; i < visitingRanges.Length; i++)
            {
                var range = visitingRanges[i];
                times[i] = new KeyValuePair<int, bool>(TimeToMinutes(range.Item1), true);
                times[times.Length - 1 - i] = new KeyValuePair<int, bool>(TimeToMinutes(range.Item2), false);
            }


            // Order the list by time asc, then by marker value desc.
            Array.Sort(times, (x, y) =>
            {
                int result = x.Key.CompareTo(y.Key);
                if (result == 0)
                    result = y.Value.CompareTo(x.Value);
                return result;
            });

            // Prep the variables we need to keep count of concurrent visitors and date ranges.
            int concurrentVisitors = 0;
            int maxConcurrentVisitors = 0;
            var maxDateRanges = new List<KeyValuePair<int, int>>();
            int enterTime = 0;
            bool isMaxActive = false;

            // Let's go!
            foreach (var time in times)
            {
                if (time.Value)
                {
                    concurrentVisitors++;
                    if (concurrentVisitors == maxConcurrentVisitors)
                    {
                        isMaxActive = true;
                        enterTime = time.Key;
                    }
                    else if (concurrentVisitors > maxConcurrentVisitors)
                    {
                        maxDateRanges.Clear();
                        maxConcurrentVisitors = concurrentVisitors;
                        isMaxActive = true;
                        enterTime = time.Key;
                    }
                }
                else
                {
                    concurrentVisitors--;
                    if (isMaxActive)
                    {
                        isMaxActive = false;
                        maxDateRanges.Add(new KeyValuePair<int, int>(enterTime, time.Key));
                    }
                }
            }

            return Tuple.Create(MinuteRangesToTimeRanges(maxDateRanges), maxConcurrentVisitors);
        }

        private static int TimeToMinutes(TimeSpan date)
        {
            return date.Days * MinutesInDay + date.Hours * MinutesInHour + date.Minutes;
        }

        private static List<Tuple<TimeSpan, TimeSpan>> MinuteRangesToTimeRanges(
            List<KeyValuePair<int, int>> minuteRanges)
        {
            var result = new List<Tuple<TimeSpan, TimeSpan>>(minuteRanges.Count);
            int rangeCnt = minuteRanges.Count;
            for (int i = 0; i < rangeCnt; i++)
            {
                result.Add(Tuple.Create(MinutesToTime(minuteRanges[i].Key), MinutesToTime(minuteRanges[i].Value)));
            }
            return result;
        }

        private static TimeSpan MinutesToTime(int minutes)
        {
            return TimeSpan.FromMinutes(minutes);
        }
    }
}