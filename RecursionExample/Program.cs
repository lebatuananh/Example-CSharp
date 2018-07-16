using System;

namespace RecursionExample
{
    internal static class Program
    {
        public static void Main(string[] args)
        {
           Console.WriteLine(Factorial(5));
           Console.WriteLine(Fibonacci(5));
        }

        private static int Factorial(int n)
        {
            if (n == 0 || n == 1) return 1;
            else
            {
                return n*Factorial(n - 1);
            }
        }

        private static int Fibonacci(int n)
        {
            if (n == 0 || n == 1) return 1;
            else
            {
                return Fibonacci(n - 1) + Fibonacci(n - 2);
            }
        }
    }
}