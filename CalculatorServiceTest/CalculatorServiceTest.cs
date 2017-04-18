using NUnit.Framework;

namespace CalculatorServiceTest
{
    [TestFixture]
    public class CalculatorServiceTest
    {
        private const string HTTP_ENDPOINT_NAME = "BasicHttpBinding_ICalculatorService";

        [Test]
        public void AddTest()
        {
            using (var client = new ServiceReference.CalculatorServiceClient(HTTP_ENDPOINT_NAME))
            {
                var sum = client.Add(2, 8);

                Assert.AreEqual(10, sum);
            }
        }

        [Test]
        public void SubtractTest()
        {
            using (var client = new ServiceReference.CalculatorServiceClient(HTTP_ENDPOINT_NAME))
            {
                var substraction = client.Subtract(2, 8);

                Assert.AreEqual(-6, substraction);
            }
        }

        [Test]
        public void MultiplyTest()
        {
            using (var client = new ServiceReference.CalculatorServiceClient(HTTP_ENDPOINT_NAME))
            {
                var multiplication = client.Multiply(2, 8);

                Assert.AreEqual(16, multiplication);
            }
        }

        [Test]
        public void DivideTest()
        {
            using (var client = new ServiceReference.CalculatorServiceClient(HTTP_ENDPOINT_NAME))
            {
                var division = client.Divide(2, 8);

                Assert.AreEqual(0.25, division);
            }
        }

        [Test]
        public void DivideByZeroTest()
        {
            using (var client = new ServiceReference.CalculatorServiceClient(HTTP_ENDPOINT_NAME))
            {
                var division = client.Divide(2, 0);

            }
        }
    }
}
