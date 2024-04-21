using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace MultiThreading.Task5.Threads.SharedCollection
{
    class Program
    {
        static void Main(string[] args)
        {
            List<int> collection = new List<int>();
            object locker = new object();

            Console.WriteLine("5. Write a program which creates two threads and a shared collection:");
            Console.WriteLine("the first one should add 10 elements into the collection and the second should print all elements in the collection after each adding.");
            Console.WriteLine("Use Thread, ThreadPool or Task classes for thread creation and any kind of synchronization constructions.");
            Console.WriteLine();

            Task firstTask = Task.Run(() =>
            {
                Monitor.Enter(locker);
                try
                {
                    for (int i = 1; i <= 10; i++)
                    {
                        collection.Add(i);
                        Monitor.Pulse(locker);
                    }
                }
                finally
                {
                    Monitor.Exit(locker);
                }
            });

            Task secondTask = Task.Run(() =>
            {
                Monitor.Enter(locker);
                try
                {
                    while (collection.Count < 10)
                    {
                        Monitor.Wait(locker);
                    }

                    for (int i = 1; i <= 10; i++)
                    {
                        List<int> elementsToPrint = collection.GetRange(0, i);
                        Console.WriteLine(string.Join(", ", elementsToPrint));
                    }
                }
                finally
                {
                    Monitor.Exit(locker);
                }
            });

            Task.WaitAll(firstTask, secondTask);

            Console.ReadLine();
        }
    }
}