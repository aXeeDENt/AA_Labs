using System;
using System.Diagnostics;
using System.Linq;
using ScottPlot;

class MergeSort
{
    static void MergeSort(int[] arr, int left, int right)
    {
        if (left < right)
        {
            int mid = left + (right - left) / 2;
            MergeSort(arr, left, mid);
            MergeSort(arr, mid + 1, right);
            Merge(arr, left, mid, right);
        }
    }

    static void Merge(int[] arr, int left, int mid, int right)
    {
        int[] leftArr = new int[mid - left + 1];
        int[] rightArr = new int[right - mid];
        Array.Copy(arr, left, leftArr, 0, leftArr.Length);
        Array.Copy(arr, mid + 1, rightArr, 0, rightArr.Length);
        int i = 0, j = 0, k = left;
        while (i < leftArr.Length && j < rightArr.Length) arr[k++] = leftArr[i] <= rightArr[j] ? leftArr[i++] : rightArr[j++];
        while (i < leftArr.Length) arr[k++] = leftArr[i++];
        while (j < rightArr.Length) arr[k++] = rightArr[j++];
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
            Console.WriteLine($"\nSorting {size}-element arrays with MergeSort:");
            long totalTime = 0;
            for (int i = 0; i < 10; i++)
            {
                int[] arr = Enumerable.Range(0, size).Select(_ => rand.Next(1, size)).ToArray();
                Stopwatch sw = Stopwatch.StartNew();
                MergeSort(arr, 0, size - 1);
                sw.Stop();
                totalTime += sw.ElapsedMilliseconds;
                if (size < 2000) Console.WriteLine($"Sorted Array: {string.Join(", ", arr)}");
                Console.WriteLine($"Run {i + 1}: Time taken: {sw.ElapsedMilliseconds} ms");
            }
            yValues[s] = totalTime / 10.0;
            Console.WriteLine($"Average time for {size} elements: {yValues[s]} ms");
        }
        var plt = new ScottPlot.Plot();
        plt.Add.Scatter(xValues, yValues);
        plt.Title("MergeSort Performance");
        plt.XLabel("Array Size");
        plt.YLabel("Time (ms)");
        plt.SavePng("MergeSortChart.png", 800, 600);
        Console.WriteLine("\nChart saved as 'MergeSortChart.png'");
    }
}
