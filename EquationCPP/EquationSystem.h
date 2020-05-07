#pragma once
#include "LinearEquation.h"
class EquationSystem
{
private:
	vector<LinearEquation> SLE;
	int n;
public:
	EquationSystem(int _n) :n(_n) {}
	~EquationSystem() { vector<LinearEquation>().swap(SLE); }
	LinearEquation& operator[] (int index) {// access to the equation by it's index
		if (index >= 0 && index < SLE.size()) // correct index check
			return SLE[index];
		else throw out_of_range("Out of range!");
	}
	int size() {//get size of the system of linear equations
		return SLE.size();
	}
	void addEquation(LinearEquation& a) {//add new equation to SLE
		if (a.size() == n + 1) //if there is place for new equation
			SLE.push_back(a); //add new equation to the end of SLE
		else throw invalid_argument("Invalid argument!");
	}
	void steppingUp() {//stepping up the SLE
		int c, z;
		for (int i = 0; i < size(); i++)
		{
			z = i;
			if (SLE[i][z] == 0)
			{
				while (SLE[i][z] == 0 && z < n) z++; //looking for non-zero element in [i] string
				c = 1;
				while ((i + c) < size() && SLE[i + c][z] == 0) c++; //while the last string is not empty
				if ((i + c) == size()) //SLE has stepped form
					return;
				swap(SLE[i], SLE[i + c]); //upper strings have less zeros. [0][0] element is not 0
			}
			for (int j = i + 1; j < size(); j++)
			{
				LinearEquation tmp1 = SLE[j] * SLE[i][z]; //lead to a common denominator
				LinearEquation tmp2 = SLE[i] * SLE[j][z]; //lead to a common denominator
				SLE[j] = tmp1 - tmp2; //set to 0 first element in [j] column
			}
		}
	}
	vector<double> solveGauss() {// solving by Gauss method
		while (SLE[size() - 1].isNull()) //remove empty string
			SLE.pop_back();
		if (SLE[size() - 1]) //while SLE is not empty
		{
			if (size() == n) //quantity of variables == quantity of equations
			{
				vector<double> solve(n);
				for (int i = size() - 1; i >= 0; i--)
				{
					solve[i] = SLE[i][n];
					for (int j = i + 1; j < n; j++)
						solve[i] -= SLE[i][j] * solve[j]; //set to 0 first element in column
					solve[i] /= SLE[i][i];
				}
				return solve;
			}
			else throw invalid_argument("Infinitely many solutions!"); //there are more variables then equations
		}
		else throw invalid_argument("No solutions!");
	}
	operator string() {//output
		string res = "";
		for (int i = 0; i < size(); i++)
			res += (string)SLE[i] + '\n';
		return res;
	}
};

