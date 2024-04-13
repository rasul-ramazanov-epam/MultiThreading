/*
*  Create a Task and attach continuations to it according to the following criteria:
   a.    Continuation task should be executed regardless of the result of the parent task.
   b.    Continuation task should be executed when the parent task finished without success.
   c.    Continuation task should be executed when the parent task would be finished with fail and parent task thread should be reused for continuation
   d.    Continuation task should be executed outside of the thread pool when the parent task would be cancelled
   Demonstrate the work of the each case with console utility.
*/
using System;
using System.Threading.Tasks;
using System.Threading;

namespace MultiThreading.Task6.Continuation
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Create a Task and attach continuations to it according to the following criteria:");
            Console.WriteLine("a.    Continuation task should be executed regardless of the result of the parent task.");
            Console.WriteLine("b.    Continuation task should be executed when the parent task finished without success.");
            Console.WriteLine("c.    Continuation task should be executed when the parent task would be finished with fail and parent task thread should be reused for continuation.");
            Console.WriteLine("d.    Continuation task should be executed outside of the thread pool when the parent task would be cancelled.");
            Console.WriteLine("Demonstrate the work of the each case with console utility.");
            Console.WriteLine();

            // a.    Continuation task should be executed regardless of the result of the parent task.
            var taskA = Task.Run(() => Console.WriteLine("Parent task A"))
                .ContinueWith(t => Console.WriteLine("Continuation task A"), TaskContinuationOptions.None);
            taskA.Wait();

            // b.    Continuation task should be executed when the parent task finished without success.
            var taskB = Task.Run(() => throw new Exception())
                .ContinueWith(t => Console.WriteLine("Continuation task B"), TaskContinuationOptions.OnlyOnFaulted);
            try
            {
                taskB.Wait();
            }
            catch { }

            // c.    Continuation task should be executed when the parent task would be finished with fail and parent task thread should be reused for continuation
            var taskC = Task.Run(() => throw new Exception())
                .ContinueWith(t => Console.WriteLine("Continuation task C"), TaskContinuationOptions.OnlyOnFaulted | TaskContinuationOptions.ExecuteSynchronously);
            try
            {
                taskC.Wait();
            }
            catch { }

            // d.    Continuation task should be executed outside of the thread pool when the parent task would be cancelled
            var cts = new CancellationTokenSource();
            var taskD = Task.Run(() => throw new OperationCanceledException(), cts.Token)
                .ContinueWith(t => Console.WriteLine("Continuation task D"), TaskContinuationOptions.OnlyOnCanceled | TaskContinuationOptions.LongRunning);
            cts.Cancel();
            try
            {
                taskD.Wait();
            }
            catch { }

            Console.ReadLine();
        }
    }
}
