using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Client
{
    internal class Field
    {
        public bool IsValid(string str)
        {
            var forbiddenChars = new HashSet<char>
            {
                ' ', '_'
            };

            foreach (char c in str)
            {
                if (forbiddenChars.Contains(c))
                    return true;
            }

            return false;
        }

        public bool IsValidPassword(string password)
        {
            if (password.Length < 8)
                return false;

            return IsValid(password);
        }

        public bool IsValidStr(string text)
        {
            if (text.Length < 1)
                return false;

            return IsValid(text);
        }
    }
}
