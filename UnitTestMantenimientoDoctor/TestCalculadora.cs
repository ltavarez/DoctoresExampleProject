using System;
using NUnit.Framework;
using Repository.Helpers;

namespace UnitTestMantenimientoDoctor
{
    public class TestCalculadora
    {

        private Calculadora calculadora;

        [SetUp]
        public void Setup()
        {
            calculadora = new Calculadora();
        }

        [Test]
        public void Sum_TwoNumbers_Returns1()
        {
            //Arrange

            int? num1 = 5;
            int? num2 = 5;

            //Act
            var result = calculadora.Sum(num1, num2);

            //Assert
            Assert.AreEqual(10,result);
        }

        [Test]
        public void Sum_FirstNumberNull_Returns1()
        {
            //Arrange

            int? num1 = null;
            int? num2 = 5;

            //Act
            var result = calculadora.Sum(num1, num2);

            //Assert
            Assert.AreEqual(5, result);
        }

        [Test]
        public void Sum_SecondNull_Returns1()
        {
            //Arrange

            int? num1 = 5;
            int? num2 = null;

            //Act
            var result = calculadora.Sum(num1, num2);

            //Assert
            Assert.AreEqual(5, result);
        }

        [Test]
        public void Substract_TwoNumbers_Returns1()
        {
            //Arrange

            int? num1 = 5;
            int? num2 = 5;

            //Act
            var result = calculadora.Substract(num1, num2);

            //Assert
            Assert.AreEqual(0, result);
        }

        [Test]
        public void Substract_FirstNumberNull_Returns1()
        {
            //Arrange

            int? num1 = null;
            int? num2 = 5;

            //Act
            var result = calculadora.Substract(num1, num2);

            //Assert
            Assert.AreEqual(-5, result);
        }

        [Test]
        public void Substract_SecondNull_Returns1()
        {
            //Arrange

            int? num1 = 5;
            int? num2 = null;

            //Act
            var result = calculadora.Substract(num1, num2);

            //Assert
            Assert.AreEqual(5, result);
        }

        [Test]
        public void Multiplication_TwoNumbers_Returns1()
        {
            //Arrange

            int? num1 = 5;
            int? num2 = 5;

            //Act
            var result = calculadora.Multiplication(num1, num2);

            //Assert
            Assert.AreEqual(25, result);
        }

        [Test]
        public void Multiplication_FirstNumberNull_Returns1()
        {
            //Arrange

            int? num1 = null;
            int? num2 = 5;

            //Act
            var result = calculadora.Multiplication(num1, num2);

            //Assert
            Assert.AreEqual(0, result);
        }

        [Test]
        public void Multiplication_SecondNull_Returns1()
        {
            //Arrange

            int? num1 = 5;
            int? num2 = null;

            //Act
            var result = calculadora.Multiplication(num1, num2);

            //Assert
            Assert.AreEqual(0, result);
        }

        [Test]
        public void Divide_TwoNumbers_Returns1()
        {
            //Arrange

            int? num1 = 5;
            int? num2 = 5;

            //Act
            var result = calculadora.Divide(num1, num2);

            //Assert
            Assert.AreEqual(1, result);
        }

        [Test]
        public void Divide_FirstNumberNull_Returns1()
        {
            //Arrange

            int? num1 = null;
            int? num2 = 5;

            //Act
            var result = calculadora.Divide(num1, num2);

            //Assert
            Assert.AreEqual(0, result);
        }

        [Test]
        public void Divide_SecondNull_Returns1()
        {
            //Arrange

            int? num1 = 5;
            int? num2 = null;

            //Act
            var result = calculadora.Divide(num1, num2);

            //Assert
            Assert.AreEqual(true, double.IsInfinity(result.Value) );
        }


    }
}