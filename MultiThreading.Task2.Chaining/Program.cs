/*
 * 2.	Write a program, which creates a chain of four Tasks.
 * First Task – creates an array of 10 random integer.
 * Second Task – multiplies this array with another random integer.
 * Third Task – sorts this array by ascending.
 * Fourth Task – calculates the average value. All this tasks should print the values to console.
 */
using System;
using System.Linq;
using System.Threading.Tasks;

namespace MultiThreading.Task2.Chaining
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(".Net Mentoring Program. MultiThreading V1 ");
            Console.WriteLine("2.	Write a program, which creates a chain of four Tasks.");
            Console.WriteLine("First Task – creates an array of 10 random integer.");
            Console.WriteLine("Second Task – multiplies this array with another random integer.");
            Console.WriteLine("Third Task – sorts this array by ascending.");
            Console.WriteLine("Fourth Task – calculates the average value. All this tasks should print the values to console");
            Console.WriteLine();

            ChainTasks();

            Console.ReadLine();
        }

        private static void ChainTasks()
        {
            var random = new Random();

            var firstTask = Task.Run(() =>
            {
                int[] numbers = new int[10];
                for (int i = 0; i < numbers.Length; i++)
                {
                    numbers[i] = random.Next(1, 100);
                }
                Console.WriteLine("First Task Array: " + string.Join(", ", numbers));
                return numbers;
            });

            var secondTask = firstTask.ContinueWith(previousTask =>
            {
                int[] numbers = previousTask.Result;
                int randomMultiplier = random.Next(1, 10);
                for (int i = 0; i < numbers.Length; i++)
                {
                    numbers[i] *= randomMultiplier;
                }
                Console.WriteLine("Second Task Array: " + string.Join(", ", numbers));
                return numbers;
            });

            var thirdTask = secondTask.ContinueWith(previousTask =>
            {
                int[] numbers = previousTask.Result;
                Array.Sort(numbers);
                Console.WriteLine("Third Task Array: " + string.Join(", ", numbers));
                return numbers;
            });

            var fourthTask = thirdTask.ContinueWith(previousTask =>
            {
                int[] numbers = previousTask.Result;
                double average = numbers.Average();
                Console.WriteLine("Fourth Task Average: " + average);
            });

            fourthTask.Wait();
        }
    }
}
