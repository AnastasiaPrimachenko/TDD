#include "pch.h"
#include "CppUnitTest.h"
#include "../EquationCPP/EquationSystem.h"

using namespace Microsoft::VisualStudio::CppUnitTestFramework;

namespace EquationSystemTestCPP
{
	TEST_CLASS(UnitTest1)
	{
	public:

		TEST_METHOD(CorrectIndex)
		{
			EquationSystem newSystem(3);
			newSystem.addEquation(LinearEquation("4,-5,2,1"));
			newSystem.addEquation(LinearEquation("3,-3,2,2"));
			newSystem.addEquation(LinearEquation("2,-3,1,3"));
			Assert::AreEqual(-3.0, newSystem[1][1]);
		}

		TEST_METHOD(CorrectSolving) //if the difference is bigger than 10^(-9), then we think SLE solved wrong
		{
			int n = 3;
			EquationSystem newSystem(n);
			LinearEquation a1("4,-5,2,1");
			LinearEquation a2("3,-3,2,2");
			LinearEquation a3("2,-3,1,3");
			newSystem.addEquation(a1);
			newSystem.addEquation(a2);
			newSystem.addEquation(a3);
			newSystem.steppingUp();
			vector<double> solve1 = newSystem.solveGauss(); //solving
			bool check = true;
			vector<double>solve2{ -11,-5,10 }; //right answer
			for (int i = 0; i < solve1.size(); i++)
				if (abs(solve1[i] - solve2[i]) > 1e-9) check = false;
			Assert::AreEqual(true, check);
		}

		TEST_METHOD(CheckNoSolutions) //number of variables is bigger than size
		{
			auto func = []()
			{
				int n = 3;
				EquationSystem newSystem(n);
				LinearEquation a1("4,-3,2,-1,8");
				LinearEquation a2("3,-2,1,-3,7");
				LinearEquation a3("5,-3,1,-8,1");
				newSystem.addEquation(a1);
				newSystem.addEquation(a2);
				newSystem.addEquation(a3);
				newSystem.steppingUp();
				vector<double> solve1 = newSystem.solveGauss();
			};
			Assert::ExpectException<invalid_argument>(func);
		}

		TEST_METHOD(InfinitelyManySolutions) // number of variables is less than size
		{
			auto func = []()
			{
				int n = 4;
				EquationSystem newSystem(n);
				LinearEquation a1("2,3,-1,1,1");
				LinearEquation a2("8,12,-9,8,3");
				LinearEquation a3("4,6,3,-2,3");
				LinearEquation a4("2,3,9,-7,3");
				newSystem.addEquation(a1);
				newSystem.addEquation(a2);
				newSystem.addEquation(a3);
				newSystem.addEquation(a4);
				newSystem.steppingUp();
				vector<double> solve1 = newSystem.solveGauss();
			};
			Assert::ExpectException<invalid_argument>(func);
		}
	};
}
