using System;
using System.Collections.Generic;
using System.Text;

namespace UtilityLibrary.Utils
{
    public class ConfirmationCode
    {
        private const int size = 10;
        private const bool lowerCase = false;

        // Instantiate random number generator.  
        private static Random _random = new Random();
        public static string RandomString()
        {
            var builder = new StringBuilder(size);

            // Unicode/ASCII Letters are divided into two blocks
            // (Letters 65–90 / 97–122):
            // The first group containing the uppercase letters and
            // the second group containing the lowercase.  

            // char is a single Unicode character  
            char offset = lowerCase ? 'a' : 'A';
            const int lettersOffset = 26; // A...Z or a..z: length=26  

            for (var i = 0; i < size; i++)
            {
                var @char = (char)_random.Next(offset, offset + lettersOffset);
                builder.Append(@char);
            }

            return lowerCase ? builder.ToString().ToLower() : builder.ToString();
        }
    }
}
