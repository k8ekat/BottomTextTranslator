using System.Text;
using Cambia.BaseN;
using System.Numerics;

namespace BottomTextTranslator;

public static class BottomText
{
    private static BaseNAlphabet _bottomAlphabet = new BaseNAlphabet("asdfghjkl;");

    public static string Encode(string TopText)
    {
        if (String.IsNullOrEmpty(TopText))
        {
            throw new ArgumentNullException();
        }

        //build base-alphabet
        var alphabetString = ((char)0).ToString() + String.Join("", TopText.Distinct().ToList());
        var alphabetBytes = Encoding.ASCII.GetBytes(alphabetString);

        //encode text by decoding text using base-alphabet, converting into bigint
        var bigintBytes = BaseConverter.Parse(TopText, new BaseNAlphabet(alphabetString)).ToByteArray();

        //build message = alphabet length + alphabet bytes + bigint text bytes        
        var messageArray = new byte[1 + alphabetBytes.Length + bigintBytes.Length];
        messageArray[0] = (byte)alphabetBytes.Length;
        Array.Copy(alphabetBytes, 0, messageArray, 1, alphabetBytes.Length);
        Array.Copy(bigintBytes, 0, messageArray, alphabetBytes.Length + 1, bigintBytes.Length);

        //encode message in base-bottom and return message
        return BaseConverter.ToBaseN(new BigInteger(messageArray), _bottomAlphabet);
    }

    public static string Decode(string BottomText)
    {
        if (String.IsNullOrEmpty(BottomText))
        {
            throw new ArgumentNullException();
        }

        //decode message from base-bottom to bigint then convert to byte array
        var messageArray = BaseConverter.Parse(BottomText, _bottomAlphabet).ToByteArray();

        //pull alphabet from bigint byte array
        var alphabetLength = (int)messageArray[0];
        var alphabetBytes = new byte[alphabetLength];
        Array.Copy(messageArray, 1, alphabetBytes, 0, alphabetLength);

        //pull text from bigint byte array
        var bigintBytes = new byte[messageArray.Length - (alphabetLength + 1)];
        Array.Copy(messageArray, 1 + alphabetBytes.Length, bigintBytes, 0, bigintBytes.Length);

        //decode by encoding bigint bytes in base-alphabet and return text
        return BaseConverter.ToBaseN(new BigInteger(bigintBytes), new BaseNAlphabet(Encoding.ASCII.GetString(alphabetBytes)));
    }

}
