using System;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using ScottPlot;

class SleepSort
{
    static void SleepSort(int[] arr, bool print)
    {
        int count = arr.Length;
        int[] sortedArr = new int[count];
        int index = 0;
        object lockObj = new object();
        ManualResetEvent done = new ManualResetEvent(false);
        int completedThreads = 0;
        foreach (int num in arr)
        {
            new Thread(() =>
            {
                Thread.Sleep(num);
                lock (lockObj) { sortedArr[index++] = num; }
                if (Interlocked.Increment(ref completedThreads) == count) done.Set();
            }).Start();
        }
        done.WaitOne(); 
        if (print) Console.WriteLine($"Sorted Array: {string.Join(", ", sortedArr)}");
    }
    static void Main()
    {
        int[] sizes = { 25, 100, 2000 };
        Random rand = new Random();
        double[] xValues = sizes.Select(s => (double)s).ToArray();
        double[] yValues = new double[sizes.Length];
        for (int s = 0; s < sizes.Length; s++)
        {
            int size = sizes[s];
            Console.WriteLine($"\nSorting {size}-element arrays with SleepSort:");
            long totalTime = 0;
            for (int i = 0; i < 10; i++)
            {
                int[] arr = Enumerable.Range(0, size).Select(_ => rand.Next(1, size)).ToArray();
                Stopwatch sw = Stopwatch.StartNew();
                SleepSort(arr, size <= 2000);
                sw.Stop();
                totalTime += sw.ElapsedMilliseconds;
                Console.WriteLine($"Run {i + 1}: Time taken: {sw.ElapsedMilliseconds} ms");
            }
            yValues[s] = totalTime / 10.0;
            Console.WriteLine($"Average time for {size} elements: {yValues[s]} ms");
        }
        var plt = new ScottPlot.Plot();
        plt.Add.Scatter(xValues, yValues);
        plt.Title("SleepSort Performance");
        plt.XLabel("Array Size");
        plt.YLabel("Time (ms)");
        plt.SavePng("SleepSortChart.png", 800, 600);
        Console.WriteLine("\nChart saved as 'SleepSortChart.png'");
    }
}
