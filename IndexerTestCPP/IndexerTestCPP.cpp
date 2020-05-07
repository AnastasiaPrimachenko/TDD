#include "pch.h"
#include "CppUnitTest.h"
#include "../IndexerCPP/Indexer.h"
using namespace Microsoft::VisualStudio::CppUnitTestFramework;

namespace IndexerTestCPP
{
	TEST_CLASS(UnitTest1)
	{
	public:

		double* array = new double[4]{ 1, 2, 3, 4 };

		TEST_METHOD(HaveCorrectLength)
		{
			Indexer indexer = Indexer(array, 4, 1, 2); // {2,3}
			Assert::AreEqual(2, indexer.Length()); // length == 2
		}
		TEST_METHOD(GetCorrectly)
		{
			Indexer indexer = Indexer(array, 4, 1, 2); // {2,3}
			Assert::AreEqual((double)2, indexer[0]);
			Assert::AreEqual((double)3, indexer[1]);
		}
		TEST_METHOD(SetCorrectly)
		{
			Indexer indexer = Indexer(array, 4, 1, 2); // {2,3}
			indexer[0] = 10; // {10,3}
			Assert::AreEqual((double)10, array[1]);
		}
		TEST_METHOD(IndexerDoesNotCopyArray)
		{
			Indexer indexer1 = Indexer(array, 4, 1, 2); // {2,3}
			Indexer indexer2 = Indexer(array, 4, 0, 2); // {1,2}
			indexer1[0] = 100500; // {100500,2}
			Assert::AreEqual((double)100500, indexer2[1]);
		}
		TEST_METHOD(FailWithWrongArguments1)
		{
			Assert::ExpectException<std::invalid_argument>([&] { Indexer(array, 4, -1, 3); }); //start can't be -1
		}
		TEST_METHOD(FailWithWrongArguments2)
		{
			Assert::ExpectException<std::invalid_argument>([&] { Indexer(array, 4, 1, -1); }); // length can't be -1

		}
		TEST_METHOD(FailWithWrongArguments3)
		{
			Assert::ExpectException<std::invalid_argument>([&] { Indexer(array, 4, 1, 10); }); // length can't be bigger than arrayLength
		}
		TEST_METHOD(FailWithWrongIndexing1)
		{
			auto indexer = Indexer(array, 4, 1, 2);
			Assert::ExpectException<std::out_of_range>([&] { indexer[-1]; }); // index can't be less than 0
		}
		TEST_METHOD(FailWithWrongIndexing2)
		{
			auto indexer = Indexer(array, 4, 1, 2);
			Assert::ExpectException<std::out_of_range>([&] { indexer[10]; }); // index can't be bigger than 3
		}
	};
}
