using System;
using System.Diagnostics;

class FibonacciBinet
{
    static long Fibonacci(int n)
    {
        double sqrt5 = Math.Sqrt(5);
        double phi = (1 + sqrt5) / 2;
        return (long)Math.Round(Math.Pow(phi, n) / sqrt5);
    }

    static void ComputeAndMeasure(int maxN)
    {
        Stopwatch stopwatch = Stopwatch.StartNew();
        for (int i = 0; i <= maxN; i++) { Fibonacci(i); }
        stopwatch.Stop();

        long ticks = stopwatch.ElapsedTicks;
        double ms = ticks / (double)TimeSpan.TicksPerMillisecond;

        Console.WriteLine($"Binet: Computed Fibonacci numbers up to {maxN} in {ticks} ticks ({ms} ms).");
    }

    static void Main()
    {
        int[] testCases = { 5, 10, 50, 100, 200, 500, 1000, 2000, 5000, 10000, 15000, 20000 };
        foreach (int n in testCases) ComputeAndMeasure(n);
    }
}