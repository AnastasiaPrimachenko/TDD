#include "pch.h"
#include "CppUnitTest.h"
#include <stdexcept>
#include <string>
#include <vector>
#include <algorithm>
#include "../EquationCPP/LinearEquation.h"
using namespace Microsoft::VisualStudio::CppUnitTestFramework;

namespace EquatiomTestCPp
{
	TEST_CLASS(UnitTest1)
	{
	public:

		TEST_METHOD(CorrectRead1)//split, read from string
		{
			string s = "1,2,3,4.5,6";
			LinearEquation a(s);
			Assert::AreEqual(4.5, a[3]);
		}

		TEST_METHOD(CorrectRead2) //read from vector
		{
			vector<double>_a{ 1,2,3,4.5,6 };
			LinearEquation a(_a);
			Assert::AreEqual(3.0, a[2]);
		}

		TEST_METHOD(CorrectRead3) //initialization with n zeros
		{
			LinearEquation a(5);
			Assert::AreEqual(0.0, a[2]);
		}

		TEST_METHOD(EqualInit) //initialization with equal numbers
		{
			LinearEquation a(5);
			a.equalInit(3.4);
			Assert::AreEqual(3.4, a[3]);
		}

		TEST_METHOD(CorrectAddition)
		{
			string s1 = "5,3,4.1,12,-6";
			string s2 = "-10,6,8,10,0.5";
			LinearEquation a(s1);
			LinearEquation b(s2);
			LinearEquation res("-5,9,12.1,22,-5.5");
			Assert::AreEqual(true, res == (a + b));
		}

		TEST_METHOD(CorrectSubstraction)
		{
			string s1 = "5,3,4.1,12,-6";
			string s2 = "-10,6,8,10,0.5";
			LinearEquation a(s1);
			LinearEquation b(s2);
			LinearEquation res("15,-3,-3.9,2,-6.5");
			Assert::AreEqual(true, res == (a - b));
		}

		TEST_METHOD(CorrectMultRight)
		{
			string s = "1,2,3,4.5,6";
			LinearEquation a(s);
			a = a * 5.0;
			Assert::AreEqual(22.5, a[3]);
		}

		TEST_METHOD(CorrectMultLeft)
		{
			string s = "1,2,3,4.5,6";
			LinearEquation a(s);
			a = 5.0 * a;
			Assert::AreEqual(22.5, a[3]);
		}

		TEST_METHOD(CorrectNegation)
		{
			LinearEquation a("1,2,3,4.5,6");
			a = -a;
			Assert::AreEqual(-3.0, a[2]);
		}

		TEST_METHOD(CheckContradictoryEquation)
		{
			LinearEquation a("0,0,0,3");
			bool check = (a) ? true : false;
			Assert::AreEqual(false, check);
		}

		TEST_METHOD(CheckSolvableEquation)
		{
			LinearEquation a("0,0,0,1,3");
			bool check = (a) ? true : false;
			Assert::AreEqual(true, check);
		}

		TEST_METHOD(CopyToList)
		{
			LinearEquation a("1,2,3,4.5,6");
			list<double> tmp = a;
			Assert::AreEqual(1.0, tmp.front());
		}
	};
}
