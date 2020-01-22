using System;
using System.Threading.Tasks;

namespace AsyncTest
{
    class Program
    {

        static void Main(string[] args)
        {
            Task.Run(async () => await Tasks.Call1());
            Console.ReadLine();
            Task.Run(async () => await AsyncAwait.Call2());
            Console.ReadLine();
        }

    }
}
