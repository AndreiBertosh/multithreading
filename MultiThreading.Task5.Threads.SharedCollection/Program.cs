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
        private static object loker = new object();
        private static List<string> myCollection = new List<string>();
        private static int itemsCount = 10;

        static void Main(string[] args)
        {
            Console.WriteLine("5. Write a program which creates two threads and a shared collection:");
            Console.WriteLine("the first one should add 10 elements into the collection and the second should print all elements in the collection after each adding.");
            Console.WriteLine("Use Thread, ThreadPool or Task classes for thread creation and any kind of synchronization constructions.");
            Console.WriteLine();

            Task.Run(() => AddDataToCollectionAndPrint());

            Console.ReadLine();
        }

        private static void AddDataToCollectionAndPrint()
        {
            try
            {
                var task1 = Task.Run(() =>
                {
                    Monitor.Enter(loker);
                    for (int i = 0; i < itemsCount; i++)
                    {
                        AddItemAndPrint($"item{i}");
                        Monitor.Pulse(loker);
                        Monitor.Wait(loker);
                    }
                    Monitor.Pulse(loker);
                });

                var task2 = Task.Run(() => 
                {
                    Monitor.Enter(loker);
                    while (myCollection.Count <= itemsCount)
                    {
                        Console.WriteLine($"Print collection from {myCollection.Count} elementd.");
                        foreach (var item in myCollection)
                        {
                            Console.WriteLine($"      The Element is: {item}");
                        }
                        Monitor.Pulse(loker);
                        Monitor.Wait(loker);
                    }
                });

                Task.WaitAll(task1, task2);
            }
            finally
            {
                Monitor.Exit(loker);
            }
        }

        private static void AddItemAndPrint(object item)
        {
            if (item is string element)
            {
                myCollection.Add(element);
                Console.WriteLine($"The element {element} was added to collection.");
            }
        }
    }
}
