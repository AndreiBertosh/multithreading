/*
*  Create a Task and attach continuations to it according to the following criteria:
   a.    Continuation task should be executed regardless of the result of the parent task.
   b.    Continuation task should be executed when the parent task finished without success.
   c.    Continuation task should be executed when the parent task would be finished with fail and parent task thread should be reused for continuation
   d.    Continuation task should be executed outside of the thread pool when the parent task would be cancelled
   Demonstrate the work of the each case with console utility.
*/
using System;
using System.Threading;
using System.Threading.Tasks;

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

            Console.WriteLine();

            var task_a = Task.Run(() =>
            {
                Console.WriteLine("Subtask A:");
                Console.WriteLine("The parent Subtask A has started.");
                throw new Exception("Stopping parent Subtask A.");
            }).ContinueWith(o =>
            {
                Console.WriteLine($"The child of Subtask A has started. Cuttent Thread ID is {Thread.CurrentThread.ManagedThreadId}");
            });

            var task_b = Task.Run(() =>
            {
                Console.WriteLine("Subtask B:");
                Console.WriteLine("The parent Subtask B has started.");
                throw new Exception("Stopping parent Subtask B.");
            }).ContinueWith(o =>
            {
                Console.WriteLine($"The child of Subtask B has started. Cuttent Thread ID is {Thread.CurrentThread.ManagedThreadId}");
            }, TaskContinuationOptions.NotOnRanToCompletion);

            var task_c = Task.Run(() =>
            {
                Console.WriteLine("Subtask C:");
                Console.WriteLine("The parent Subtask C has started.");
                throw new Exception("Stopping parent Subtask C.");
            }).ContinueWith(o =>
            {
                Console.WriteLine($"The child of Subtask C has started. Cuttent Thread ID is {Thread.CurrentThread.ManagedThreadId}");
            }, TaskContinuationOptions.ExecuteSynchronously | TaskContinuationOptions.OnlyOnFaulted);

            var tokenSource = new CancellationTokenSource();
            var task_d = Task.Run(() =>
            {
                Console.WriteLine("Subtask D:");
                Console.WriteLine("The parent Subtask D has started.");
                tokenSource.Token.ThrowIfCancellationRequested();
                Thread.Sleep(2000);
            }, tokenSource.Token);
            tokenSource.Cancel();
            
            task_d.ContinueWith(o =>
            {
                Console.WriteLine($"The child of Subtask D has started. Cuttent Thread ID is {Thread.CurrentThread.ManagedThreadId}");
            }, TaskContinuationOptions.OnlyOnCanceled);

            Task.WaitAll(task_a, task_b, task_c, task_d);

            Console.ReadLine();
        }
    }
}
