/*
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
        static readonly int recursiveLevel = 10;
        static readonly Semaphore semaphoreSlim = new Semaphore(0, 10);

        static void Main(string[] args)
        {
            Console.WriteLine("4.	Write a program which recursively creates 10 threads.");
            Console.WriteLine("Each thread should be with the same body and receive a state with integer number, decrement it, print and pass as a state into the newly created thread.");
            Console.WriteLine("Implement all of the following options:");
            Console.WriteLine();
            Console.WriteLine("- a) Use Thread class for this task and Join for waiting threads.");
            Console.WriteLine("- b) ThreadPool class for this task and Semaphore for waiting threads.");

            Console.WriteLine();

            Console.WriteLine("--------- Threads with join -----------");
            Thread threadPartA = new Thread(() => RecursiveTask(recursiveLevel));
            threadPartA.Start();

            Thread.Sleep(2000);
            Console.WriteLine();
            Console.WriteLine("--------- Threads with semaphore -------");

            ThreadPool.QueueUserWorkItem(new WaitCallback(RecursiveTtaskPool), recursiveLevel);

            Console.ReadLine();
        }

        private static void RecursiveTtaskPool(object currentLevel)
        {
            if (currentLevel is int level)
            {
                Console.WriteLine($"The thread with state {level} is started. Current thread Id: {Thread.CurrentThread.ManagedThreadId}");
                if (level > 1)
                {
                    var newLevel = level - 1;
                    ThreadPool.QueueUserWorkItem(new WaitCallback(RecursiveTtaskPool), newLevel);
                    semaphoreSlim.WaitOne();
                }
                else
                {
                    Console.WriteLine("---------------------------------------");
                }

                semaphoreSlim.Release(1);
                Console.WriteLine($"The thread with state {level} will finished. Current thread Id: {Thread.CurrentThread.ManagedThreadId}");
            }
        }

        private static void RecursiveTask(int level)
        {
            Console.WriteLine($"The thread with state {level} is started. Current thread Id: {Thread.CurrentThread.ManagedThreadId}");

            if (level > 1)
            {
                var newLevel = level - 1;
                Thread thread = new Thread(() => RecursiveTask(newLevel));
                thread.Start();
                thread.Join();
            }
            else 
            {
                Console.WriteLine("---------------------------------------");
            }

            Console.WriteLine($"The thread with state {level} will finished. Current thread Id: {Thread.CurrentThread.ManagedThreadId}");
        }
    }
}
