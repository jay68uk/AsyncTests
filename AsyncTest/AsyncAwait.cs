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
            var taskId = 1;
            var start = DateTime.Now.Ticks;
            Console.WriteLine($"Start: {start}");
            for (long i = 0; i < 20000; i += 2000)
            {
                _ = await LongTask(i, taskId);
                taskId++;
            }

            var end = DateTime.Now.Ticks;
            Console.WriteLine($"End: {end}");
            Console.WriteLine($"Difference: {end - start}");
        }

        private static async Task<int> LongTask(long sleepTime, int taskId)
        {
            var start = DateTime.Now.Ticks;

            Console.WriteLine($"STARTING -- Current thread: {Thread.CurrentThread.ManagedThreadId} Background: {Thread.CurrentThread.IsBackground} Sleep time: {sleepTime} Moniker: LT-{taskId}");

            var st = await ShortTask(sleepTime, taskId);

            Console.WriteLine($"ENDING -- Current thread: {Thread.CurrentThread.ManagedThreadId} Background: {Thread.CurrentThread.IsBackground} Sleep time: {sleepTime} Moniker: LT-{taskId} Duration: {DateTime.Now.Ticks-start}");
            return taskId;
        }

        private static async Task<string> ShortTask(long inString, int taskId)
        {
            Console.WriteLine($"Current thread: {Thread.CurrentThread.ManagedThreadId} Moniker: ST-{taskId}");
            return await Task.Run(() => inString.ToString() + inString.ToString().Length);
        }
    }
}
