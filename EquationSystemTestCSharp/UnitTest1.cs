using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using EquationCSharp;
using System.Collections.Generic;

namespace EquationSystemTestCSharp
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void CorrectIndexing()
        {
            int n = 3;
            EquationSystem s = new EquationSystem(n);
            s.addEquation(new LinearEquation("4,-5,2,1"));
            s.addEquation(new LinearEquation("3,-3,2,2"));
            s.addEquation(new LinearEquation("2,-3,1,3"));
            Assert.AreEqual(new LinearEquation("3,-3,2,2"), s[1]);
        }

        [TestMethod]
        public void CorrectSolving()//if the difference is bigger than 10^(-9), then we think SLE solved wrong
        {
            int n = 3;
            EquationSystem s = new EquationSystem(n);
            s.addEquation(new LinearEquation("4,-5,2,1"));
            s.addEquation(new LinearEquation("3,-3,2,2"));
            s.addEquation(new LinearEquation("2,-3,1,3"));
            s.steppingUp();
            double[] solve1 = new double[] { -11, -5, 10 };//right answer
            double[] solve2 = s.solveSystem();//solving
            bool check = true;
            for (int i = 0; i < n; i++)
            {
                if (Math.Abs(solve1[i] - solve2[i]) > 1e-9)
                    check = false;
            }
            Assert.AreEqual(true, check);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CheckNoSolutions() //number of variables is bigger than size
        {
            int n = 3;
            EquationSystem s = new EquationSystem(n);
            s.addEquation(new LinearEquation("4,-3,2,-1,8"));
            s.addEquation(new LinearEquation("3,-2,1,-3,7"));
            s.addEquation(new LinearEquation("5,-3,1,-8,1"));
            s.steppingUp();
            Assert.Equals(typeof(ArgumentException), s.solveSystem());
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void InfinitelyManySolutions() // number of variables is less than size
        {
            int n = 3;
            EquationSystem s = new EquationSystem(n);
            s.addEquation(new LinearEquation("2,3,-1,1,1"));
            s.addEquation(new LinearEquation("8,12,-9,8,3"));
            s.addEquation(new LinearEquation("4,6,3,-2,3"));
            s.addEquation(new LinearEquation("2,3,9,-7,3"));
            s.steppingUp();
            Assert.Equals(typeof(ArgumentException), s.solveSystem());
        }
    }
}
