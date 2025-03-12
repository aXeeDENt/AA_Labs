using System;
using System.Diagnostics;
using System.Linq;
using ScottPlot;

class QuickSort
{
    static void QuickSort(int[] arr, int low, int high)
    {
        if (low < high)
        {
            int pivot = Partition(arr, low, high);
            QuickSort(arr, low, pivot - 1);
            QuickSort(arr, pivot + 1, high);
        }
    }
    static int Partition(int[] arr, int low, int high)
    {
        int pivot = arr[high];
        int i = low - 1;
        for (int j = low; j < high; j++)
        {
            if (arr[j] < pivot)
            {
                i++;
                (arr[i], arr[j]) = (arr[j], arr[i]);
            }
        }
        (arr[i + 1], arr[high]) = (arr[high], arr[i + 1]);
        return i + 1;
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
            Console.WriteLine($"\nSorting {size}-element arrays with QuickSort:");
            long totalTime = 0;
            for (int i = 0; i < 10; i++) 
            {
                int[] arr = new int[size];
                for (int j = 0; j < size; j++) arr[j] = rand.Next(1, size);
                Stopwatch sw = Stopwatch.StartNew();
                QuickSort(arr, 0, size - 1);
                sw.Stop();
                totalTime += sw.ElapsedMilliseconds;
                if (size <= 100) Console.WriteLine($"Sorted Array: {string.Join(", ", arr)}");
                Console.WriteLine($"Run {i + 1}: Time taken: {sw.ElapsedMilliseconds} ms");
            }
            yValues[s] = totalTime / 10.0; 
            Console.WriteLine($"Average time for {size} elements: {yValues[s]} ms");
        }
        var plt = new ScottPlot.Plot();
        plt.Add.Scatter(xValues, yValues);
        plt.Title("QuickSort Performance");
        plt.XLabel("Array Size");
        plt.YLabel("Time (ms)");
        plt.SavePng("QuickSortChart.png", 800, 600)ж
        Console.WriteLine("\nChart saved as 'QuickSortChart.png'");
    }
}
