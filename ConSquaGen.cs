using System;
using System.Collections.Generic;
using System.Text;

namespace Crypto
{
    class ConSquaGen //ax^2+bx+c mod n
    {
        static int m, a, b, c, x0, x;
        static List<char> Sequence = new List<char>();
        public ConSquaGen(int mm, int N)
        {
            a = N + 32;
            b = N + 59;
            c = N + 94;
            m = mm;
            x0 = 3 * N + 21;//x0 = 3 * N + 21;
            Console.WriteLine("x0=3*{0}+21={1}",N,x0);
            x = x0;
        }
        public void Kolobok()
        {

            for(int i=1;i<=20;i++)
            {
                x = (a * x0 * x0 + b * x0 + c) % m;
                string binX = Convert.ToString(Convert.ToInt32(x), 2);
                Sequence.Add(binX[binX.Length - 1]);
                Console.WriteLine("x{0}={1}*{2}^2+{3}*{4}+{5}={6} ({7})",i,a,x0,b,x0,c,x,binX);

                x0 = x;
            }
            
        }
        public void outSeq()
        {
            foreach (var n in Sequence)
                Console.Write(n);
        }
    }
}
