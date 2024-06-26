﻿/*
 * 4.	Write a program which recursively creates 10 threads.
 * Each thread should be with the same body and receive a state with integer number, decrement it,
 * print and pass as a state into the newly created thread.
 * Use Thread class for this task and Join for waiting threads.
 * 
 * Implement all of the following options:
 * - a) Use Thread class for this task and Join for waiting threads.
 * - b) ThreadPool class for this task and Semaphore for waiting threads.
 */

using System;
using System.Threading;

namespace MultiThreading.Task4.Threads.Join
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("4.	Write a program which recursively creates 10 threads.");
            Console.WriteLine("Each thread should be with the same body and receive a state with integer number, decrement it, print and pass as a state into the newly created thread.");
            Console.WriteLine("Implement all of the following options:");
            Console.WriteLine();
            Console.WriteLine("- a) Use Thread class for this task and Join for waiting threads.");
            Console.WriteLine("- b) ThreadPool class for this task and Semaphore for waiting threads.");

            Console.WriteLine();

            CreateThreadAndJoin(10);

            var semaphore = new Semaphore(0, 10);
            CreateThreadPoolThreadAndUseSemaphore(10, semaphore);
            for (int i = 0; i < 10; i++)
            {
                semaphore.WaitOne();
            }

            Console.ReadLine();
        }

        static void CreateThreadAndJoin(int number)
        {
            if (number <= 0) return;

            var thread = new Thread(() =>
            {
                Console.WriteLine($"Thread: {number}");
                CreateThreadAndJoin(number - 1);
            });

            thread.Start();
            thread.Join();
        }

        static void CreateThreadPoolThreadAndUseSemaphore(int number, Semaphore semaphore)
        {
            if (number <= 0) return;

            ThreadPool.QueueUserWorkItem(_ =>
            {
                Console.WriteLine($"ThreadPool: {number}");
                CreateThreadPoolThreadAndUseSemaphore(number - 1, semaphore);
                semaphore.Release();
            });
        }
    }
}
