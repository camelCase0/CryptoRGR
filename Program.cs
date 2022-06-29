using System;
using System.Collections.Generic;
using System.Text;

namespace Crypto
{

    class Program
    {
        
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            //2 RGR
            Console.WriteLine("\n....TASK 2....");
            int[] polynom = { 2, 4, 6, 7 };//x5 + x3 + x1 + x0
            Registr r1 = new Registr("00011000", polynom);//01100100

            r1.Generate(50);
            //r1.RGenerate(20);
            r1.getAllRounds();
            //

            //3 RGR
            Console.WriteLine("\n....TASK 3....");
            ConSquaGen kon1 = new ConSquaGen(1709, 25);
            kon1.Kolobok();
            kon1.outSeq();

            //4 RGR
            Console.WriteLine("\n....TASK 4....");
            RSA elKey1 = new RSA(83, 103, 38);//p q IV
            elKey1.GenHash("рижук");//use "i" from EN
            elKey1.Encrypt(5);//e
            elKey1.getCrypted();

            //5
            Console.WriteLine("\n....TASK 5....");
            int a= Convert.ToInt32("397", 16), b = Convert.ToInt32("202", 16), c= Convert.ToInt32("453", 16);
            int res = (a & b) ^ (~a & c);

            Console.WriteLine($"(A & B) xor (!A & C) = ({res})DEC");
            //6 RGR
            Console.WriteLine("\n....TASK 6....");
            //int a = -4, b = 20, d = 5, r = 4;
            //int p = 31;
            ElGamal crypto = new ElGamal();//a,b,p,d,r
            crypto.buildDots();
            crypto.encrypt();
            crypto.decrypt();
           
        }
    }

}

            