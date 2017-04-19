using System;
using System.ServiceModel;

namespace CalculatorService
{
    public class CalculatorService : ICalculatorService
    {
        public int Add(int a, int b)
        {
            return a + b;
        }

        public int Subtract(int a, int b)
        {
            return a - b;
        }

        public int Multiply(int a, int b)
        {
            return a * b;
        }

        public double Divide(int a, int b)
        {
            if (b == 0) throw new FaultException<ArgumentException>(new ArgumentException(),"Division by zero occured");
            return (double)a / b;
        }
    }
}
