using System;
using System.Diagnostics;
using System.Linq;
using ScottPlot;

class HeapSortDemo
{
    static void HeapSort(int[] arr)
    {
        int n = arr.Length;
        for (int i = n / 2 - 1; i >= 0; i--)
            Heapify(arr, n, i);

        for (int i = n - 1; i > 0; i--)
        {
            (arr[0], arr[i]) = (arr[i], arr[0]);
            Heapify(arr, i, 0);
        }
    }

    static void Heapify(int[] arr, int n, int i)
    {
        int largest = i, left = 2 * i + 1, right = 2 * i + 2;
        if (left < n && arr[left] > arr[largest]) largest = left;
        if (right < n && arr[right] > arr[largest]) largest = right;
        if (largest != i)
        {
            (arr[i], arr[largest]) = (arr[largest], arr[i]);
            Heapify(arr, n, largest);
        }
    }

    static void Main()
    {
        int[] sizes = { 25, 100, 2000, 15000, 90000 };
        Random rand = new Random();
        double[] xValues = sizes.Select(s => (double)s).ToArray();
        double[] yValues = new double[sizes.Length];

        for (int s = 0; s < sizes.Length; s++)
        {
            int size = sizes[s];
            Console.WriteLine($"\nSorting {size}-element arrays with HeapSort:");

            long totalTime = 0;
            for (int i = 0; i < 10; i++)
            {
                int[] arr = Enumerable.Range(0, size).Select(_ => rand.Next(1, size)).ToArray();

                Stopwatch sw = Stopwatch.StartNew();
                HeapSort(arr);
                sw.Stop();
                totalTime += sw.ElapsedMilliseconds;

                if (size <= 2000)
                    Console.WriteLine($"Sorted Array: {string.Join(", ", arr)}");

                Console.WriteLine($"Run {i + 1}: Time taken: {sw.ElapsedMilliseconds} ms");
            }

            yValues[s] = totalTime / 10.0;
            Console.WriteLine($"Average time for {size} elements: {yValues[s]} ms");
        }

        var plt = new ScottPlot.Plot();
        plt.Add.Scatter(xValues, yValues);
        plt.Title("HeapSort Performance");
        plt.XLabel("Array Size");
        plt.YLabel("Time (ms)");
        plt.SavePng("HeapSortChart.png", 800, 600);
        Console.WriteLine("\nChart saved as 'HeapSortChart.png'");
    }
}
