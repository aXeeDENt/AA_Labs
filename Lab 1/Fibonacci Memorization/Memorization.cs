using System;
using System.Diagnostics;

class FibonacciMemoization
{
    static long Fibonacci(int n, long[] memo)
    {
        if (n <= 1) return n;
        if (memo[n] != 0) return memo[n];
        return memo[n] = Fibonacci(n - 1, memo) + Fibonacci(n - 2, memo);
    }

    static void ComputeAndMeasure(int maxN)
    {
        Stopwatch stopwatch = Stopwatch.StartNew();
        long[] memo = new long[maxN + 1];
        for (int i = 0; i <= maxN; i++) { Fibonacci(i, memo); }
        stopwatch.Stop();

        long ticks = stopwatch.ElapsedTicks;
        double ms = ticks / (double)TimeSpan.TicksPerMillisecond;

        Console.WriteLine($"Memoization: Computed Fibonacci numbers up to {maxN} in {ticks} ticks ({ms} ms).");
    }

    static void Main()
    {
        int[] testCases = { 5, 10, 50, 100, 200, 500, 1000, 2000, 5000, 10000, 15000, 20000 };
        foreach (int n in testCases) ComputeAndMeasure(n);
    }
}