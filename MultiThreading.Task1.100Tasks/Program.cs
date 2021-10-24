/*
 * 1.	Write a program, which creates an array of 100 Tasks, runs them and waits all of them are not finished.
 * Each Task should iterate from 1 to 1000 and print into the console the following string:
 * “Task #0 – {iteration number}”.
 */
using System;
using System.Threading.Tasks;

namespace MultiThreading.Task1._100Tasks
{
    class Program
    {
        const int TaskAmount = 100;
        const int MaxIterationsCount = 1000;

        static void Main(string[] args)
        {
            Console.WriteLine(".Net Mentoring Program. Multi threading V1.");
            Console.WriteLine("1.	Write a program, which creates an array of 100 Tasks, runs them and waits all of them are not finished.");
            Console.WriteLine("Each Task should iterate from 1 to 1000 and print into the console the following string:");
            Console.WriteLine("“Task #0 – {iteration number}”.");
            Console.WriteLine();
            
            HundredTasks();

            // other variant
            //HundredTasksOther();

            Console.ReadLine();
        }

        static void HundredTasks()
        {
            for (int i = 0; i < TaskAmount; i++)
            {
                var taskNumber = i;
                Task iteratopnTask = new Task(() => IterationTask(taskNumber));
                iteratopnTask.Start();
                // other variant
                //Task.Factory.StartNew(() => IterationTask(taskNumber));
            }
        }

        static void IterationTask(int taskNumber)
        {
            for (int i = 0; i < MaxIterationsCount; i++)
            {
                var iterationNumber = i;
                Task outputTask = new Task(() => Output(taskNumber, iterationNumber));
                outputTask.Start();
                // other variant
                //Task.Factory.StartNew(() => Output(taskNumber, iterationNumber));
            }
        }

        static void HundredTasksOther()
        {
            for (int i = 0; i < 100; i++)
            {
                var taskNumber = i;
                var task = new Task(() =>
                {
                    for (int j = 0; j < 100; j++)
                    {
                        var iterationNumber = j;
                        var outputTask = new Task(() => Output(taskNumber, iterationNumber));
                        outputTask.Start();
                    }
                });
                task.Start();
            }
        }

        static void Output(int taskNumber, int iterationNumber)
        {
            Console.WriteLine($"Task #{taskNumber} – {iterationNumber}");
        }
    }
}
