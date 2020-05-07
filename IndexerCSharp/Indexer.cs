using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TDD
{
    public class Indexer
    {
        double[] NN;
        int first, last;

        public Indexer(double[] array, int element, int length)
        {
            if (element < 0 || length <= 0 || array.Length <= element + length) throw new ArgumentException();
            this.NN = array;
            this.first = element;
            this.last = element + length - 1;
        }
        public int Length
        {
            get { return last - first + 1; }
        }

        public double this[int i]
        {
            get
            {
                if (i < 0 || i >= this.Length) throw new IndexOutOfRangeException();
                return this.NN[i + first];
            }
            set
            {
                if (i < 0 || i >= this.Length) throw new IndexOutOfRangeException();
               this.NN[i + first] = value;
            }
        }
    }

}
