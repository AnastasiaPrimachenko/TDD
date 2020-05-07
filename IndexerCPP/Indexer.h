#include <stdexcept>
using namespace std;

class Indexer
{
private:
	double* array;
	int start, end;
public:
	int Length() {
		return this->end - this->start + 1;
	}
	double& operator[] (const int index) {//access to the element by it's index
		if (index < 0 || index >= Length())
			throw out_of_range("Index out of range");
		return this->array[index + start]; //pointer to the element
	}
	
	Indexer(double* array, int arrayLength, int start, int length) // access to the part of array by the some element and length from this element
	{
		if (start < 0 || length <= 0 || start + length >= arrayLength) //input check
			throw invalid_argument("Incorrect first index or length");
		else
		{
			this->array = array;
			this->start = start;
			this->end = start + length - 1;
		}
	}
};

