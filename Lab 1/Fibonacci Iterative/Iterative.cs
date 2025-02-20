using System;
using System.Diagnostics;
class FibonacciIterative
{
    static long Fibonacci(int n)
    {
        if (n <= 1) return n;
        long[] fib = new long[n + 1];
        fib[0] = 0;
        fib[1] = 1;
        for (int i = 2; i <= n; i++) fib[i] = fib[i - 1] + fib[i - 2];
        return fib[n];
    }
    static void ComputeAndMeasure(int maxN)
    {
        Stopwatch stopwatch = Stopwatch.StartNew();
        for (int i = 0; i <= maxN; i++) { Fibonacci(i); }
        stopwatch.Stop();
        long ticks = stopwatch.ElapsedTicks;
        double ms = ticks / (double)TimeSpan.TicksPerMillisecond;
        Console.WriteLine($"Iterative: Computed Fibonacci numbers up to {maxN} in {ticks} ticks ({ms} ms).");
    }
    static void Main()
    {
        int[] testCases = { 5, 10, 50, 100, 200, 500, 1000, 2000, 5000, 10000, 15000, 20000 };
        foreach (int n in testCases) ComputeAndMeasure(n);
    }
}