using System;
using System.Threading.Tasks;

namespace AsyncTest
{
    class Program
    {

        static async Task Main(string[] args)
        {
            await Task.Run(async () =>
            {
                await Tasks.Call1();
                Console.ReadLine();

            });

            await AsyncAwait.Call2();
            Console.ReadLine();
        }

    }
}
