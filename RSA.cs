using System;
using System.Collections.Generic;
using System.Text;

namespace Crypto
{
    class RSA // H[i] = (2^(H[i-1]) )^M[i] mod n
    {
        static int p, q, e;
        static List<int> Hash = new List<int>();
        static List<int> Crypted = new List<int>();
        
        public RSA(int pp, int qq, int IV)
        {
            p = pp;
            q = qq;
            Hash.Add(IV);
        }
        ~RSA()
        {
            Console.WriteLine(".....RSA key deleted.....");
        }
        public int[] getCrypted() 
        {
            return Crypted.ToArray();
        }
        public int[] getHash()
        {
            return Hash.ToArray();
        }
        List<int> GenMessage(string strToEncrypt)
        {
            string alphabet = "абвгдеєжзиiїйклмнопрстуфхцчшщьюя";
            
            List<int> M = new List<int>();
            foreach (char l in strToEncrypt)
            {
                M.Add(alphabet.IndexOf(l)+1);
                Console.WriteLine("Leter '{0}' = {1}",l, M[M.Count-1]);
            }

            return M;
        }
        int IntPow(int x, int pow, int mod)
        {
            int res = x;
            for (int i = 1; i < pow; i++)
            {
                res *= x;
                res %= mod;
            }
            return res;
        }
        public void GenHash(string strochka)
        {
            List<int> M = GenMessage(strochka);
            int mod = p * q;
            int i = 1;
            foreach(int m in M)
            {
                Hash.Add( IntPow(IntPow(2, Hash[Hash.Count-1],mod), m,mod));
                Console.WriteLine("H{0} = (2^{1})^{2} mod {3} = {4}",i++, Hash[Hash.Count - 2],m,mod, Hash[Hash.Count - 1]);
        
            }
            Hash.RemoveAt(0);
        }
        static int revElement(int a, int m)
        {
            int x1 = 0, x2 = 1,tempA,tempX;

            while (true)
            {
                tempA = m % a;
                tempX = x1 - (m / a) * x2;
                x1 = x2;
                x2 = tempX;
                m = a;
                a = tempA;
                if (a == 1) return x2;
                if (a == 0) throw new ArgumentException(String.Format("(e,m) != 1"),"e");
            }
        }

        public void Encrypt(int e)//e - openKey; d-secret key
        {
            int f = (p - 1) * (q - 1);
            int d = revElement( e , f);
            Console.WriteLine("d={0}^-1 mod {1} = {2}", e, f, d);

            //h H^d
            int i = 1;
            foreach(int hash in Hash)
            {
                int res = IntPow(hash, d, (p * q));
                Crypted.Add(res);
                Console.WriteLine("C{0} = {1}^{2} mod {3} = {4}",i++, hash, d, p * q, res);
            }
            Hash.Clear();
        }

    }
}
