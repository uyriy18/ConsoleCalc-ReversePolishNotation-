using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Traning
{
    internal class OperationTimer : IDisposable
    {
        long _startTime;
        string _text;
        int _colectionCount;

        public OperationTimer(string text)
        {
            PrepareForOperation();
            _text = text;
            _colectionCount = GC.CollectionCount(0);
            _startTime = Stopwatch.GetTimestamp();
        }

        public void Dispose()
        {
            Console.WriteLine($"{_text}\t{(Stopwatch.GetTimestamp() - _startTime) / (double)Stopwatch.Frequency:0.00} seconds (garbrage collectins {GC.CollectionCount(0) - _colectionCount})");
        }
        private static void PrepareForOperation()
        {
            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();
        }
    }


    class Program
    {
        private static void ValueTypePerfTest()
        {
            const int COUNT = 10000000;
            using (new OperationTimer("List"))
            {
                List<int> list = new List<int>(COUNT);
                for(int n = 0; n < COUNT; n++)
                {
                    list.Add(n);
                    int x = list[n];
                }
                list = null;
            }
            using(new OperationTimer("ArrayList"))
            {
                ArrayList array = new ArrayList();
                for(int n = 0; n < COUNT; n++)
                {
                    array.Add(n);
                    int x = (int)array[n];
                }
                array = null;
            }
        }
 
        static void Main(String[] args)
        {
            ValueTypePerfTest();
        }


    }
}
