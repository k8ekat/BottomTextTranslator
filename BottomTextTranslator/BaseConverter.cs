using System.Numerics;

namespace BottomTextTranslator
{
    internal static class BaseConverter
    {
        public static string ToBaseAlphabet(BigInteger value, string alphabet)
        {
            var targetBase = new BigInteger(alphabet.Length);

            if(targetBase <= 1)
            {
                throw new InvalidAlphabetException();
            }

            LinkedList<char> result = new LinkedList<char>();
            
            do
            {
                result.Prepend(alphabet[(int)(value % targetBase)]);
                value /= targetBase;
            } while (value > 0);

            return String.Join("", result);
        }

        public static BigInteger FromBaseAlphabet(string value, string alphabet)
        {
            BigInteger result = 0;
            BigInteger multiplier = 1;

            for (int i = value.Length - 1; i >= 0; i--)
            {
                result += alphabet.IndexOf(value[i]) * multiplier;
                multiplier *= alphabet.Length;
            }

            return result;
        }
    }
}
