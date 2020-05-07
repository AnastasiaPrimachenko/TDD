using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using EquationCSharp;
using System.Collections.Generic;

namespace EquationTestCSharp
{
    [TestClass]
    public class UnitTest1
    {

        [TestMethod]
        public void String()
        {
            string coeff = "1, 2, 3, 4.5, 6";
            LinearEquation a1 = new LinearEquation(coeff);
            Assert.AreEqual(4.5, a1[3]);
        }

        [TestMethod]
        public void Array()
        {
            const int n = 5;
            double[] coeff = new double[n] { 1, 2, 3, 4.5, 6 };
            LinearEquation a = new LinearEquation(coeff);
            Assert.AreEqual(3, a[2]);
        }

        [TestMethod]
        public void List()
        {
            List <double> coeff = new List <double>() { 1, 2, 3, 4.5, 6 };
            LinearEquation a1 = new LinearEquation(coeff);
            Assert.AreEqual(6, a1[4]);
        }

        [TestMethod]
        public void nZeros()
        {
            int n = 3;
            LinearEquation a = new LinearEquation(n);
            Assert.AreEqual("0x1+0x2+0x3=0", a.ToString());
        }

        [TestMethod]
        public void CorrectAddition()
        {
            string coeff1 = "5,3,4.1,12,-6";
            LinearEquation a = new LinearEquation(coeff1);
            string coeff2 = "-10,6,8,10,0.5";
            LinearEquation b = new LinearEquation(coeff2);
            string res = "-5,9,12.1,22,-5.5";
            Assert.AreEqual(new LinearEquation(res), a + b);
        }

        [TestMethod]
        public void CorrectSubstraction()
        {
            string coeff1 = "5,3,4.1,12,-6";
            LinearEquation a = new LinearEquation(coeff1);
            string coeff2 = "-10,6,8,10,0.5";
            LinearEquation b = new LinearEquation(coeff2);
            string res = "15,-3,-3.9,2,-6.5";
            Assert.AreEqual(new LinearEquation(res), a - b);
        }

        [TestMethod]
        public void CorrectNegation()
        {
            string coeff = "5,3,4.1,12,-6";
            LinearEquation a = new LinearEquation(coeff);
            string res = "-5,-3,-4.1,-12,6";
            Assert.AreEqual(new LinearEquation(res), -a);
        }

        [TestMethod]
        public void CorrectMultRight()
        {
            string coeff = "5,3,4.1,12,-6";
            LinearEquation a = new LinearEquation(coeff);
            string res = "25,15,20.5,60,-30";
            Assert.AreEqual(new LinearEquation(res), a * 5);
        }

        [TestMethod]
        public void CorrectMultLeft()
        {
            string coeff = "5,3,4.1,12,-6";
            LinearEquation a = new LinearEquation(coeff);
            string res = "25,15,20.5,60,-30";
            Assert.AreEqual(new LinearEquation(res), 5 * a);
        }

        [TestMethod]
        public void CorrectEqualInit() 
        {
            LinearEquation a = new LinearEquation(3);
            a.equalInit(3.9);
            string res = "3.9,3.9,3.9";
            Assert.AreEqual(new LinearEquation(res), a);
        }

        [TestMethod]
        public void CheckContradictoryEquation()
        {
            string res = "0,0,0,5";
            LinearEquation a = new LinearEquation(res);
            bool check = (a) ? true : false;
            Assert.AreEqual(false, check);
        }

        [TestMethod]
        public void CheckSolvableEquation()
        {
            string res = "0,0,0,1,3";
            LinearEquation a = new LinearEquation(res);
            bool check = (a) ? true : false;
            Assert.AreEqual(true, check);
        }
    }
}
