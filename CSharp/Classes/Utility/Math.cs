namespace CodingPlayground
{
    public static class Math
    {
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

    }
}