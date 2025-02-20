using System;
using System.Diagnostics;

class FibonacciMatrix
{
    static long Fibonacci(int n)
    {
        if (n == 0) return 0;
        long[,] F = { { 1, 1 }, { 1, 0 } };
        Power(F, n - 1);
        return F[0, 0];
    }

    static void Power(long[,] F, int n)
    {
        if (n <= 1) return;
        long[,] M = { { 1, 1 }, { 1, 0 } };
        Power(F, n / 2);
        Multiply(F, F);
        if (n % 2 != 0) Multiply(F, M);
    }

    static void Multiply(long[,] F, long[,] M)
    {
        long x = F[0, 0] * M[0, 0] + F[0, 1] * M[1, 0];
        long y = F[0, 0] * M[0, 1] + F[0, 1] * M[1, 1];
        long z = F[1, 0] * M[0, 0] + F[1, 1] * M[1, 0];
        long w = F[1, 0] * M[0, 1] + F[1, 1] * M[1, 1];
        F[0, 0] = x;
        F[0, 1] = y;
        F[1, 0] = z;
        F[1, 1] = w;
    }

    static void ComputeAndMeasure(int maxN)
    {
        Stopwatch stopwatch = Stopwatch.StartNew();
        for (int i = 0; i <= maxN; i++) { Fibonacci(i); }
        stopwatch.Stop();

        long ticks = stopwatch.ElapsedTicks;
        double ms = ticks / (double)TimeSpan.TicksPerMillisecond;

        Console.WriteLine($"Matrix: Computed Fibonacci numbers up to {maxN} in {ticks} ticks ({ms} ms).");
    }

    static void Main()
    {
        int[] testCases = { 5, 10, 50, 100, 200, 500, 1000, 2000, 5000, 10000, 15000, 20000};
        foreach (int n in testCases) ComputeAndMeasure(n);
    }
}