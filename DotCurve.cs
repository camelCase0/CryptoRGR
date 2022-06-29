using System;
using System.Collections.Generic;
using System.Text;
using System.Numerics;
using System.Collections;

namespace Crypto
{
    class DotCurve
    {
        static int a = -4, p = 31;
        public int X { get; set; }
        public int Y { get; set; }
        public DotCurve(int x, int y)
        {
            X = x;
            Y = y;
        }
        //static int revElement(int aa, int m)
        //{
        //    if (aa == 0) return 0;
        //    int x1 = 0, x2 = 1, tempA, tempX;

        //    while (true)
        //    {
        //        tempA = m % aa;
        //        tempX = x1 - (m / aa) * x2;
        //        x1 = x2;
        //        x2 = tempX;
        //        m = aa;
        //        aa = tempA;
        //        if (aa == 1) return x2;
        //        if (aa == 0) throw new ArgumentException(String.Format("(e,m) != 1"), "e");
        //    }
        //}
        static int smena(int a)
        {
            if (a < 0) return p+(a % p);
            return a;
        }
        static int invert(int a, int m)
        {
            a = smena(a);
            if (a < 1 || m < 2)
                return -1;

            int u1 = m;
            int u2 = 0;
            int v1 = a;
            int v2 = 1;

            while (v1 != 0)
            {
                int q = u1 / v1;
                int t1 = u1 - q * v1;
                int t2 = u2 - q * v2;
                u1 = v1;
                u2 = v2;
                v1 = t1;
                v2 = t2;
            }

            return u1 == 1 ? (u2 + m) % m : -1;
        }
        
        static List<int> takeDegree(int x)
        {

            string s = Convert.ToString(x);
            BigInteger bigint = BigInteger.Parse(s);
            BitArray ba = new BitArray(bigint.ToByteArray());

            List<int> ls = new List<int>();
            BigInteger check = 0;
            for(int i = 0; i<ba.Length; i++)
            {
                if (ba[i] == true) ls.Add(i);
            }
        //WriteLine(check); //проверяем
            ls.Reverse();

            return ls;
        //string result = check.ToString() + " = " + String.Join(" + ", ls);
        
        }
        public static DotCurve operator +(DotCurve dot1, DotCurve dot2)
        {

            int x, y, x1 = dot1.X, y1 = dot1.Y, x2 = dot2.X, y2 = dot2.Y;
            x = (int)(Math.Pow(((y2 - y1) * invert(smena((x2 - x1)), p)), 2) - x1 - x2)%p;
            y = (-y1 + (y2 - y1) * invert(smena(x2 - x1), p) * (x1 - x))%p;
            return new DotCurve(smena(x), smena(y));
        }
        public static DotCurve operator *(int a, DotCurve dot)
        {
            List<int> deg = takeDegree(a);
            DotCurve res = new DotCurve(0,0);
            bool first = true;
            foreach(int d in deg)
            {
                DotCurve curent = dot;
                for(int i=0; i<d;i++)
                {
                    curent = ~curent;
                    //Console.WriteLine($"{i} curent = ({curent.X},{curent.Y})");
                }
                if (first)
                {
                    res = curent;
                    first = false;
                }
                else res = res + curent;

                //Console.WriteLine($"foreach res = ({res.X},{res.Y})");
            }
            
            
            return res;
        }
        public static DotCurve operator !(DotCurve dot)//сим точка
        {
            int x = dot.X, y = dot.Y;
            return new DotCurve(smena(x), smena(-y));
        }
        public static DotCurve operator ~(DotCurve dot)//подвоення
        {
            int x, y, l, x1 = dot.X, y1 = dot.Y;
            l = invert(2 * y1, p) * (3 * x1 * x1 + a) % p;
            x = (l*l-2*x1)%p;
            y = (l*(x1-x)-y1)%p;
            return new DotCurve(smena(x), smena(y));
        }
    }
}
