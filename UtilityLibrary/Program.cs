using System;
using UtilityLibrary.Utils;

namespace UtilityLibrary
{
    class Program
    {
        static void Main(string[] args)
        {
            string text = "ponnambalamramanadan@gmail.com";
            Console.WriteLine("Original: {0}",text);
            string encrypted = EncryptUtil.EncryptString(text);
            Console.WriteLine("Encrypted: {0}", encrypted);
            string decrypt = EncryptUtil.DecryptString(encrypted);
            Console.WriteLine("Decrypted: {0}", decrypt);
        }
    }
}
