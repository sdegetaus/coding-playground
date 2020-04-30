namespace CodingPlayground
{
    public static class Math
    {
        public const double PI = 3.14159265358979323846;

        public static double Factorial(int n) => (n == 0) ? 1 : n * Factorial(n - 1);
        public static int Fibonacci(int n) => (n <= 1) ? n : Fibonacci(n - 1) + Fibonacci(n - 2);

        public static int Abs(int value) => (value < 0) ? value * -1 : value;
        public static float Abs(float value) => (value < 0) ? value * -1 : value;

        public static int Max(int val1, int val2) => (val1 > val2) ? val1 : val2;
        public static float Max(float val1, float val2) => (val1 > val2) ? val1 : val2;

        public static int Min(int val1, int val2) => (val1 < val2) ? val1 : val2;
        public static float Min(float val1, float val2) => (val1 < val2) ? val1 : val2;

        public static double Pow(double x, double y)
        {
            double result = x;
            for (int i = 0; i < y - 1; i++)
            {
                result *= x;
            }
            return result;
        }

        // TODO: implement Taylor Series
        public static double Cos(double a) => System.Math.Cos(a);
        public static double Sin(double a) => System.Math.Sin(a);
        public static double Tan(double a) => System.Math.Tan(a);

        #region Extensions

        public static double ToRadians(this double v) => (PI / 180d) * v;
        public static double ToDegrees(this double v) => v * 180d / PI;

        #endregion

    }
}