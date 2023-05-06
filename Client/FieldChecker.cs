using System.Collections.Generic;

namespace Client
{
    internal static class FieldChecker
    {
        public static bool IsValid(string str)
        {
            var forbiddenChars = new HashSet<char>
            {
                ' ', '_'
            };

            foreach (char c in str)
            {
                if (forbiddenChars.Contains(c))
                    return false;
            }

            return true;
        }

        public static bool IsValidPassword(string? password)
        {
            if (password?.Length < 8 || password == null)
                return false;

            return IsValid(password);
        }

        public static bool IsValidStr(string? text)
        {
            if (text?.Length <= 1 || text == null)
                return false;

            return IsValid(text);
        }
    }
}
