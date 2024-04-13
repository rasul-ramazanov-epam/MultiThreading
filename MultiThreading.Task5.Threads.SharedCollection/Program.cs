/*
 * 5. Write a program which creates two threads and a shared collection:
 * the first one should add 10 elements into the collection and the second should print all elements
 * in the collection after each adding.
 * Use Thread, ThreadPool or Task classes for thread creation and any kind of synchronization constructions.
 */
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

            Task.Run(() =>
            {
                Monitor.Enter(locker);
                for (int i = 0; i < 10; i++)
                {
                    collection.Add(i);
                }
                Monitor.Exit(locker);
            });

            Task.Run(() =>
            {
                Monitor.Enter(locker);
                foreach (int i in collection)
                {
                    Console.WriteLine("element`s value: " + i);
                }
                Monitor.Exit(locker);
            });

            Console.ReadLine();
        }
    }
}
