using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSIA.WebFresher032023.Demo.UnitTests
{
    [TestFixture]
    public class CalculatorTests
    {
        [TestCase(1, 2, 3)]
        [TestCase(int.MaxValue, 2, ((long)int.MaxValue + 2))]
        [TestCase(int.MaxValue, int.MaxValue, ((long)int.MaxValue * 2))]
        public void Add_ValidInput_ReturnsSuccess(int a, int b, long expectedResult)
        {
            // Act (hành động)
            var calculator = new Calculator();
            var actualResult = calculator.Add(a, b);
            // Assert (khẳng định)
            Assert.That(actualResult, Is.EqualTo(expectedResult));
        }

        [TestCase(1, 2, -1)]
        [TestCase(int.MaxValue, 2, ((long)int.MaxValue - 2))]
        [TestCase(int.MaxValue, int.MinValue, ((long)int.MaxValue * 2) + 1)]
        public void Sub_ValidInput_ReturnsSuccess(int a, int b, long expectedResult)
        {
            // Act (hành động)
            var calculator = new Calculator();
            var actualResult = calculator.Sub(a, b);
            // Assert (khẳng định)
            Assert.That(actualResult, Is.EqualTo(expectedResult));
        }

        [TestCase(1, 2, 2)]
        [TestCase(int.MaxValue, 2, ((long)int.MaxValue * 2))]
        [TestCase(int.MaxValue, int.MinValue, ((long)int.MaxValue * int.MinValue))]
        public void Mul_ValidInput_ReturnsSuccess(int a, int b, long expectedResult)
        {
            // Act (hành động)
            var calculator = new Calculator();
            var actualResult = calculator.Mul(a, b);
            // Assert (khẳng định)
            Assert.That(actualResult, Is.EqualTo(expectedResult));
        }

        [TestCase(1, 2, (double)1 / 2)]
        [TestCase(int.MaxValue, 2, (double)(long)int.MaxValue / 2)]
        [TestCase(int.MaxValue, int.MinValue, (double)(long)int.MaxValue / int.MinValue)]
        public void Div_ValidInput_ReturnsSuccess(int a, int b, double expectedResult)
        {
            // Act (hành động)
            var calculator = new Calculator();
            var actualResult = calculator.Div(a, b);
            // Assert (khẳng định)
            Assert.That(actualResult, Is.EqualTo(expectedResult));
        }

        [TestCase(1, 0, "Không chia được cho 0")]
        public void Div_ZeroDivide_ReturnsException(int a, int b, string expectedResult)
        {
            // Act (hành động) & Assert (khẳng định)
            var calculator = new Calculator();
            //var handle = () => calculator.Div(a, b);
            var ex = Assert.Throws<Exception>(() => calculator.Div(a, b));
            Assert.That(ex.Message, Is.EqualTo(expectedResult));
        }
    }
}
