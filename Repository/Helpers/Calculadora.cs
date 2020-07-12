using System;
using System.Collections.Generic;
using System.Text;

namespace Repository.Helpers
{
    public class Calculadora
    {

        public int? Sum(int? num1, int? num2)
        {
            num1 = num1 ?? 0;
            num2 = num2 ?? 0;

            return num1 + num2;
        }

        public int? Substract(int? num1, int? num2)
        {
            num1 = num1 ?? 0;
            num2 = num2 ?? 0;

            return num1 - num2;
        }

        public int? Multiplication(int? num1, int? num2)
        {
            num1 = num1 ?? 0;
            num2 = num2 ?? 0;
            return num1 * num2;
        }

        public double? Divide(double? num1, double? num2)
        {

            try
            {
                num1 = num1 ?? 0;
                num2 = num2 ?? 0;

                return num1 / num2;
            }
            catch (Exception e)
            {
                return null;
            }
          
        }

    }
}
