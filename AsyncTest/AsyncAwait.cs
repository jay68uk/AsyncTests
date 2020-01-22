using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AsyncTest
{
    public class AsyncAwait
    {
        public static async Task Call2()
        {
            var start = DateTime.Now.Ticks;
            Console.WriteLine($"Start: {start}");
            for (int i = 0; i < 20000; i += 500)
            {
                await LongTask(i);
            }

            var end = DateTime.Now.Ticks;
            Console.WriteLine($"End: {end}");
            Console.WriteLine($"Difference: {end - start}");
        }

        static async Task<int[]> LongTask(int sleepTime)
        {
            var st = await ShortTask(sleepTime);
            var retVal = new int[3] { Thread.CurrentThread.ManagedThreadId, sleepTime, st.Length };

            Console.WriteLine($"Current thread: {retVal[0]} Sleep time: {retVal[1]} Moniker: {retVal[2]}");
            return retVal;
        }

        static  Task<string> ShortTask(int inString)
        {
            return Task.Run(() => inString.ToString() + inString.ToString().Length);
        }
    }
}
