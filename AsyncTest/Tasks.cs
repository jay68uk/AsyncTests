using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

namespace AsyncTest
{
    public class Tasks
    {
        public static ConfiguredTaskAwaitable Call1()
        {
            List<Task> tasks = new List<Task>();
            var start = DateTime.Now.Ticks;
            Console.WriteLine($"Start: {start}");
            for (int i = 0; i < 20000; i += 500)
            {
                tasks.Add(LongTask(i));
            }

            return Task.WhenAll(tasks).ContinueWith(t =>
            {
                var end = DateTime.Now.Ticks;
                Console.WriteLine($"End: {end}");
                Console.WriteLine($"Difference: {end - start}");

            }).ConfigureAwait(false);

        }

        static Task<int[]> LongTask(int sleepTime)
        {
            return Task.Run(() =>
            {
                string st = string.Empty;
                var retTask = Task.Run(() => ShortTask(sleepTime)).ContinueWith(t =>
                {
                    st = t.Result;
                    var retVal = new int[3] { Thread.CurrentThread.ManagedThreadId, sleepTime, st.Length };
                    Console.WriteLine($"Current thread: {retVal[0]} Sleep time: {retVal[1]} Moniker: {retVal[2]}");
                    return retVal;
                });
                return retTask;
            });
        }

        static Task<string> ShortTask(int inString)
        {
            return Task.Run(() => inString.ToString() + inString.ToString().Length);
        }
    }
}
