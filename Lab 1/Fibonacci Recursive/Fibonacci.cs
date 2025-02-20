using System;
using System.Diagnostics;

class FibonacciOptimizedIterative
{
    static long Fibonacci(int n)
    {
        if (n <= 1) return n;
        long a = 0, b = 1, c = 0;
        for (int i = 2; i <= n; i++)
        {
            c = a + b;
            a = b;
            b = c;
        }
        return c;
    }

    static void ComputeAndMeasure(int maxN)
    {
        Stopwatch stopwatch = Stopwatch.StartNew();
        for (int i = 0; i <= maxN; i++)
        {
            Fibonacci(i);
        }
        stopwatch.Stop();

        long ticks = stopwatch.ElapsedTicks;
        double ms = ticks / (double)TimeSpan.TicksPerMillisecond;

        Console.WriteLine($"Optimized Iterative: Computed Fibonacci numbers up to {maxN} in {ticks} ticks ({ms} ms).");
    }

    static void Main()
    {
        int[] testCases = { 5, 10, 50, 100, 200, 500, 1000, 2000, 5000, 10000, 15000, 20000 };
        foreach (int n in testCases)
            ComputeAndMeasure(n);
    }
}
