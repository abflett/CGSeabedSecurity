using System;

namespace CGSeabedSecurity
{
    public static class Util
    {
        public static int GetNumericValue()
        {
            return int.Parse(Console.ReadLine());
        }

        public static int[] GetNumericValues()
        {
            return Array.ConvertAll(Console.ReadLine().Split(' '), int.Parse);
        }

        public static double CalculateDistance(int srcX, int srcY, int destX, int destY)
        {
            return Math.Sqrt(Math.Pow(destX - srcX, 2) + Math.Pow(destY - srcY, 2));
        }
    }
}