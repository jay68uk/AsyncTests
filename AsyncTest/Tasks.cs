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
        public static async Task Call1()
        {

            var start = DateTime.Now.Ticks;
            var taskId = 1;

            Console.WriteLine($"Start: {start}");
            for (long i = 0; i < 20000; i += 2000)
            {
                var id = taskId;
                var i1 = i;
                var i2 = i;
                var id1 = taskId;
                _ = await Task.Run(async () =>
                    {
                        await LongTask(i1, id);
                        Console.WriteLine(
                            $"ENDING -- Current thread: {Thread.CurrentThread.ManagedThreadId} Background: {Thread.CurrentThread.IsBackground} Sleep time: {i1} Moniker: LT-{id}");
                    })
                    .ContinueWith(task => ShortTask(i2, id1))
                    .ConfigureAwait(false);
                taskId++;
            }

        }

        public static async Task<int> LongTask(long sleepTime, int taskId)
        {
            Console.WriteLine(
                $"STARING -- Current thread: {Thread.CurrentThread.ManagedThreadId} Background: {Thread.CurrentThread.IsBackground} Sleep time: {sleepTime} Moniker: LT-{taskId}");
            var st = string.Empty;
            await Task.Delay(new TimeSpan(sleepTime));
            return taskId;

        }

        public static async Task<string> ShortTask(long inString, int taskId)
        {
            Console.WriteLine($"Current thread: {Thread.CurrentThread.ManagedThreadId} Moniker: ST-{taskId}");
            return await Task.Run(() => inString.ToString() + inString.ToString().Length);
        }
    }
}
