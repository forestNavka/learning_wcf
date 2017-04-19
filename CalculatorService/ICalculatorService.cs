using System;
using System.ServiceModel;

namespace CalculatorService
{
    [ServiceContract]
    public interface ICalculatorService
    {
        [OperationContract]
        int Add(int a, int b);

        [OperationContract]
        int Subtract(int a, int b);

        [OperationContract]
        int Multiply(int a, int b);

        [OperationContract]
        [FaultContract(typeof(ArgumentException))]
        double Divide(int a, int b);
    }
}
