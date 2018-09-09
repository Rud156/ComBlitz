using UnityEngine;

namespace ComBlitz.Extensions
{
    public static class ExtensionFunctions
    {
        public static int GetClosestMultiple(float number, int? multiple = 5)
        {
            int a = (int)(number / 5) * 5;
            int b;

            if (number < 0)
                b = a - 5;
            else
                b = a + 5;

            return (Mathf.Abs(number - a) > Mathf.Abs(b - number)) ? b : a;
        }
    }
}