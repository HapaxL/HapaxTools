using System;
using System.Collections.Generic;
using System.Text;

namespace HapaxTools
{
    static class Program
    {
        static void Main()
        {
            var r = new Random();

            var u = new Pondered<int, int>();
            u.Add(0, 1);
            u.Add(1, 2);
            u.Add(2, 3);
            var v = u.FetchRandomInt(r);

            Console.WriteLine(v);
        }
    }
}
