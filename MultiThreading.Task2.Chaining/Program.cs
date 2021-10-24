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
        private static Random randomNum = new Random();
        static void Main(string[] args)
        {
            Console.WriteLine(".Net Mentoring Program. MultiThreading V1 ");
            Console.WriteLine("2.	Write a program, which creates a chain of four Tasks.");
            Console.WriteLine("First Task – creates an array of 10 random integer.");
            Console.WriteLine("Second Task – multiplies this array with another random integer.");
            Console.WriteLine("Third Task – sorts this array by ascending.");
            Console.WriteLine("Fourth Task – calculates the average value. All this tasks should print the values to console");
            Console.WriteLine();

            Task<int[]> firstTask = Task.Run(() => FirstTaskArray());
            Task<int[]> secondTask = firstTask.ContinueWith(x => SecondTaskArray(firstTask.Result));
            Task<int[]> thirdTask = secondTask.ContinueWith(x => ThirdTaskArray(secondTask.Result));
            Task fourthTask = thirdTask.ContinueWith(x => FourthTask(thirdTask.Result));

            Console.ReadLine();
        }

        internal static int[] FirstTaskArray()
        {
            var randomArray = new int[10];

            for (int i = 0; i < 10; i++)
            {
                randomArray[i] = randomNum.Next(1, 999);
                Console.WriteLine($"The random array element {i} is {randomArray[i]}");
            }

            return randomArray;
        }

        internal static int[] SecondTaskArray(int[] numbersArray)
        {
            var randomValue = randomNum.Next(100, 999);
            var secondRandomArray = new int[10];
            foreach (var item in numbersArray)
            {
                secondRandomArray[Array.IndexOf(numbersArray, item)] = item * randomValue;
                Console.WriteLine($"The random Second array element {secondRandomArray[Array.IndexOf(numbersArray, item)]} is {secondRandomArray[Array.IndexOf(numbersArray, item)] }");
            }

            return secondRandomArray;
        }

        internal static int[] ThirdTaskArray(int[] thirdRandomArray)
        {
            Array.Sort(thirdRandomArray);

            foreach (var item in thirdRandomArray)
            {
                Console.WriteLine($"The random Third array element {thirdRandomArray[Array.IndexOf(thirdRandomArray, item)]} is {item}");
            }

            return thirdRandomArray;
        }

        private static void FourthTask(int[] fourthRandomArray)
        {
            var result = Enumerable.Average(fourthRandomArray);
            Console.WriteLine($"The average of array is {result}");

        }
    }
}
