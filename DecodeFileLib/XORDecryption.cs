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

        // Initialize XORDecryption
        public XORDecryption(string caracteres, int keyLength)
        {
            this.caracteres = caracteres;
            this.keyLength = keyLength;
        }

        // Decrypt XOR message using key
        public string Decrypt(string textToEncrypt, string key)
        {
            StringBuilder result = new StringBuilder();
            for (int c = 0; c < textToEncrypt.Length; c++)
                result.Append((char)((uint)textToEncrypt[c] ^ (uint)key[c % key.Length]));
            return result.ToString();
        }

        // Generate Key for XOR Decrytpion
        public string FindKey()
        {
            string response = "";
            key = IncreaseKey(key, keyLength - 1, caracteres.Length - 1);
            if (key == null) return null;

            for (int i = 0; i < keyLength; i++)
            {
                response += caracteres[key[i]];
            }
            return response;
        }

        // Increase current key example : aaa -> aab -> abb
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
