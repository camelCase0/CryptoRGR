using System;
using System.Collections.Generic;
using System.Text;

namespace Crypto
{
    class ElGamal 
    {
        int a { get; set; }
        int b { get; set; }
        int p { get; set; }
        int d { get; set; }
        int r { get; set; }
        DotCurve P = new DotCurve(26, 16);
        DotCurve e1, e2, c1, c2, M;
        List<DotCurve> e = new List<DotCurve>();

        public ElGamal(int aa = -4, int bb = 20, int pp = 31, int dd = 5, int rr = 4)
        {
            a = aa;
            b = bb;
            p = pp;
            d = dd;
            r = rr;
        }
        
        public void buildDots()
        {
            int[] x = new int[p];
            int[] y = new int[p];
            for (int i = 0; i < p; i++)
            {
                x[i] = (i * i * i + a * i + b) % p;
                y[i] = (i * i) % p;
            }

            for (int i = 0; i < p; i++)
            {
                for (int j = 0; j < p; j++)
                {
                    if (x[i] == y[j]) e.Add(new DotCurve(i, j));
                }
            }
            foreach (DotCurve dot in e)
                Console.Write("(" + dot.X + " " + dot.Y + "), ");
        }
        private void takeE1()
        {
            Console.WriteLine("Choose e1 [0 - {0}]", e.Count);
            int inp = Convert.ToInt32(Console.ReadLine());
            e1 = e[inp - 1];
            Console.WriteLine("e1 = ({0},{1})", e1.X, e1.Y);
        }

        private void calcE2() 
        {
            e2 = d * e1;
            Console.WriteLine($"e2 = d * e1 = ({e2.X},{e2.Y})");
        }
        private void calcC1() {
            c1 = r * e1;
            Console.WriteLine($"c1 = r * e1 = ({c1.X},{c1.Y})");
        }
        private void calcC2() {
            c2 = P + (r * e2);
            Console.WriteLine($"c2 = P + (r * e2) = ({c2.X},{c2.Y})");
        }
        public void encrypt() {
            takeE1();
            calcE2();
            calcC1();
            calcC2();
        }
        public void decrypt() {
            M = c2 + !(d * c1);
            Console.WriteLine($"d*c1 = ({(d*c1).X},{(d*c1).Y})");
            Console.WriteLine($"M =c2 + !(d * c1) = ({c2.X},{c2.Y}) + ({(!(d * c1)).X},{(!(d * c1)).Y})=({M.X},{M.Y})");
        }
    }
}
