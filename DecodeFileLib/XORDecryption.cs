using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DecodeFileLib
{
    class XORDecryption
    {
        private int[] key = { 0, 0, 0, 0, 0, -1 };
        private string caracteres;
        private int keyLength;

        public XORDecryption(string caracteres, int keyLength)
        {
            this.caracteres = caracteres;
            this.keyLength = keyLength;
        }

        public string Decrypt(string textToEncrypt, string key)
        {
            StringBuilder result = new StringBuilder();
            for (int c = 0; c < textToEncrypt.Length; c++)
                result.Append((char)((uint)textToEncrypt[c] ^ (uint)key[c % key.Length]));
            return result.ToString();
        }

        public string FindKey()
        {
            string response = "";
            key = IncreaseKey(key, keyLength - 1, caracteres.Length - 1);
            Console.WriteLine(key[5]);
            for (int i = 0; i < keyLength; i++)
            {
                response += caracteres[key[i]];
            }
            return response;
        }

        private int[] IncreaseKey(int[] key, int current, int length)
        {
            if (key[current] != length)
            {
                key[current]++;
                return key;
            }
            else
            {
                if (current == 0) return null;
                key[current] = 1;
                return IncreaseKey(key, current - 1, length);
            }
        }

    }
}
