using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EquationCSharp
{
    class EquationSystem
    { 
        
        private List<LinearEquation> system = new List<LinearEquation>();
        private int n;

        public EquationSystem(int n)
        {
            this.n = n;
        }

        public LinearEquation this[int index] // access to the equation by it's index
        {
            get
            {
                if (index >= 0 && index < Size)
                    return system[index]; //get equation by it's index
                else throw new IndexOutOfRangeException();
            }
            set
            {
                if (index >= 0 && index < Size)
                    system[index] = value; //set coefs to equation by it's index
                else throw new IndexOutOfRangeException();
            }
        }

        public int Size => system.Count; //get size of the system of linear equations

        public void addEquation(LinearEquation a) //add new equation to SLE
        {
            if (a.Size == n + 1) //if there is place for new equation
                system.Add(a); //add new equation to the end of SLE
            else throw new ArgumentException();
        }

        public void removeEquation(int index) //remove equation from SLE
        {
            system.RemoveAt(index);
        }

        public void steppingUp() //stepping up the SLE
        {
            int c, z;
            double p1, p2;
            for (int i = 0; i < Size; i++)
            {
                z = i;
                if (this[i][z] == 0)
                {
                    while (this[i][z] == 0 && z < n) z++; //looking for non-zero element in [i] string
                    c = 1;
                    while ((i + c) < Size && this[i + c][z] == 0) c++;//while the last string is not empty
                    if ((i + c) == Size) //SLE has stepped form
                        return;
                    swap(this[i], this[i + c]); //upper strings have less zeros. [0][0] element is not 0
                }
                for (int j = i + 1; j < Size; j++)
                {
                    p1 = this[i][z];
                    p2 = this[j][z];
                    this[j] = this[j] * p1 - this[i] * p2; //set to 0 first element in [j] column
                }
            }
        }

        public double[] solveSystem() // solving by Gauss method
        {
            while (this[Size - 1].IsNull()) //remove empty string
                this.removeEquation(Size - 1);
            if (this[Size - 1]) //while SLE is not empty
            {
                if (Size == n) //quantity of variables == quantity of equations
                {
                    double[] solve = new double[n];
                    for (int i = Size - 1; i >= 0; i--)
                    {
                        solve[i] = this[i][n];
                        for (int j = i + 1; j < n; j++)
                        {
                            solve[i] -= this[i][j] * solve[j]; //set first element in column to 0
                        }
                        solve[i] /= this[i][i];
                    }
                    return solve;
                }
                else throw new ArgumentException("The system has infinitely many solutions!");
            }
            else throw new ArgumentException("The system has no solutions!");
        }

        private void swap(LinearEquation a, LinearEquation b) //exchange
        {
            LinearEquation tmp = new LinearEquation(a);
            b.CopyTo(a);
            tmp.CopyTo(b);
        }

        public override string ToString() //output
        {
            string res = "";
            for (int i = 0; i < Size; i++)
                res += this[i].ToString() + '\n';
            return res;
        }
    }
    
}
