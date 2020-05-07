using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TDD
{
    class Program
    {
        static void Main(string[] args)
        {
            double[] array = new double[] { 1, 2, 3, 4 };
            var indexer1 = new Indexer(array, 1, 2); // {2,3}
            var indexer2 = new Indexer(array, 0, 2); // {1,2}
            indexer1[0] = 100500; // {100500,2}
        }
    }
}
