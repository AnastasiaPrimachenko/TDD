using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace EquationCSharp
{
    public class LinearEquation
    {
        private List<double> coefs;
        public LinearEquation(string _coef) // equation from the string of coefficients
        {
            string[] coef = Regex.Split(_coef, @"[^\d.-]"); //splits the string by regular expression
            coefs = new List<double>();
            for (int i = 0; i < coef.Length; i++)
            {
                if (coef[i] != "")
                {
                    coef[i] = coef[i].Replace('.', ',');
                    coefs.Add(double.Parse(coef[i]));
                }
            }
        }
        public LinearEquation(double[] coef) // equation from the array
        {
            coefs = new List<double>(coef);
        }

        public LinearEquation(List<double> coef) // equation from the list
        {
            coefs = new List<double>(coef);
        }
        public LinearEquation(IEnumerable<double> coef)
        {
            coefs = new List<double>(coef);
        }

        public LinearEquation(int n) // equation from n zeros
        {
            if (n > 0)
            {
                coefs = new List<double>();
                for (int i = 0; i <= n; i++)
                    coefs.Add(0);
            }
            else throw new ArgumentException();
        }
        public int Size //get number of coefficients
        {
            get { return coefs.Count; }
        }

        public void randInit() // an equation with random numbers
        {
            Random rand = new Random();
            for (int i = 0; i < Size; i++) //put random numbers to each vector's element
                coefs[i] = rand.Next(55) / 10;
        }

        public void equalInit(double value) // an equation with equal numbers
        {
            for (int i = 0; i < Size; i++) //put some number to each vector's element
                coefs[i] = value;
        }
        public double this[int index] //access to coefficient by index
        {
            get
            {
                if (index >= 0 && index < Size) //get coefficient by index
                    return coefs[index];
                else throw new IndexOutOfRangeException();
            }
            set
            {
                if (index >= 0 && index < Size) //set a value to coefficient by index
                    coefs[index] = value;
                else throw new IndexOutOfRangeException();
            }
        }

        public static LinearEquation operator +(LinearEquation a, LinearEquation b) // a+b
        {
            var result = a.coefs.Zip(b.coefs, (first, second) => first + second); //use elements of two lists, the result is new list
            return new LinearEquation(result);
        }

        public static LinearEquation operator -(LinearEquation a, LinearEquation b) // a-b
        {
            var result = a.coefs.Zip(b.coefs, (first, second) => first - second); //use elements of two lists, the result is new list
            return new LinearEquation(result);
        }

        public static LinearEquation operator *(LinearEquation a, double r) // a*r
        {
            List<double> result = new List<double>();
            for (int i = 0; i < a.Size; i++) // put a*r to each element
                result.Add(a[i] * r);
            return new LinearEquation(result);
        }

        public static LinearEquation operator *(double r, LinearEquation a) // r*a
        {
            return a * r; // put a*r to each element using operator *
        }

        public static LinearEquation operator -(LinearEquation a) // -a
        {
            List<double> result = new List<double>();
            for (int i = 0; i < a.Size; i++) // put -a to each element
                result.Add(-a[i]);
            return new LinearEquation(result);
        }

        public override string ToString() // output to console
        {
            string res = "";
            int i;
            for (i = 0; i < Size - 2; i++) //add coefs to string, increase degree of x
                res += (coefs[i + 1] >= 0) ? (coefs[i].ToString() + 'x' + (i + 1).ToString() + '+') : (coefs[i].ToString() + 'x' + (i + 1).ToString());
            res += (coefs[i].ToString() + 'x' + (i + 1).ToString());
            res += '=' + coefs[Size - 1].ToString();
            return res;
        }

        public static bool operator ==(LinearEquation a, LinearEquation b)
        {
            return a.Equals(b); //comparison
        }

        public static bool operator !=(LinearEquation a, LinearEquation b)
        {
            return !(a == b); //comparison using operator ==
        }

        public static implicit operator double[](LinearEquation a)
        {
            return a.coefs.ToArray(); //copy coefficients from list to array
        }

        public static bool operator false(LinearEquation a) //false - contradictory equation
        {
            if (a)
                return false;
            else
                return true;
        }

        public static bool operator true(LinearEquation a) //true - solvable equation
        {
            for (int i = 0; i < a.Size - 1; i++)
                if (a[i] != 0)
                    return true;
            return (a[a.Size - 1] != 0) ? false : true;
        }

        public override bool Equals(object obj) //comparison
        {
            LinearEquation a = (LinearEquation)obj;
            for (int i = 0; i < Size; i++)
                if (Math.Abs(this[i] - a[i]) > 1e-9) //if the difference is bigger than 10 ^ (-9), then we think they are not equal
                    return false;
            return true;
        }

        public bool IsNull() // empty check
        {
            for (int i = 0; i < Size; i++)
                if (this[i] != 0)
                    return false;
            return true;
        }

        public void CopyTo(LinearEquation a) //copy coefficients to list
        {
            a.coefs = coefs.ToList();
        }
    }
}
