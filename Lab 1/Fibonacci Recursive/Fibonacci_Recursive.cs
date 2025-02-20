using System;
using System.Diagnostics;

class FibonacciRecursive
{
    static long Fibonacci(int n)
    {
        if (n <= 1) return n;
        return Fibonacci(n - 1) + Fibonacci(n - 2);
    }

    static void ComputeAndMeasure(int maxN)
    {
        Stopwatch stopwatch = Stopwatch.StartNew();
        for (int i = 0; i <= maxN; i++) { Fibonacci(i); }
        stopwatch.Stop();

        long ticks = stopwatch.ElapsedTicks;
        double ms = ticks / (double)TimeSpan.TicksPerMillisecond;

        Console.WriteLine($"Recursive: Computed Fibonacci numbers up to {maxN} in {ticks} ticks ({ms} ms).");
    }

    static void Main()
    {
        int[] testCases = { 5, 10, 15, 20, 25, 30, 35, 40 };
        foreach (int n in testCases) ComputeAndMeasure(n);
    }
}