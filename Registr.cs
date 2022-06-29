using System;
using System.Collections.Generic;
using System.Text;

namespace Crypto
{
    class Registr
    {
        static List<string> memory = new List<string>();
        static List<int> degreesPolynom = new List<int>();
        public Registr(string init, int[] degrees)
        {
            memory.Add(init);
            foreach (int el in degrees)
                degreesPolynom.Add(el);
        }
        static int yFunc(string x)
        {
            int res=0;
            foreach (int id in degreesPolynom)
                res ^= x[id];
            return res;
        }
        static int revFunc(string x, int curent0)
        {
            
            int res=0;
            foreach (int id in degreesPolynom)
            {
                if (id == x.Length) break;
                res ^= x[id];
            }
            res ^= curent0;
            return res;
        }
        
        public void Generate(int rounds) 
        {
            for (int i = 0; i<rounds; i++)
            {
                string next = memory[i];
                next = next.Substring(0, next.Length - 1);
                int funcBit = yFunc(memory[i]);
                memory.Add(Convert.ToString(funcBit) + next);
            }
        }
        public void RGenerate(int round) 
        {
            for (int i = 0; i<round; i++)
            {
                string next = memory[i];
                next = next.Substring(1, next.Length - 1);
                int funcBit = revFunc(next,memory[i][0]);
                memory.Add(next + Convert.ToString(funcBit));
            }
            memory.Reverse();
        }

        public string getRound(int el)
        {
            return memory[el];
        }
        public void getAllRounds()
        {
            int ix = 0;
            foreach (string el in memory)
            {
                Console.WriteLine("{0}|{1}", ix++, el);
            }
        }
    }
}
