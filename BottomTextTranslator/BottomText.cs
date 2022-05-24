using System.Text;
using Cambia.BaseN;
using System.Numerics;

namespace BottomTextTranslator;

public static class BottomText
{

    private static string Encode(string message, KeyboardLayout keyboardLayout)
    {
        if (String.IsNullOrEmpty(message))
        {
            throw new ArgumentNullException();
        }

        var bottomAlphabet = new BaseNAlphabet(keyboardLayout.Alphabet);

        //build base-alphabet
        var alphabetString = ((char)0).ToString() + String.Join("", message.Distinct().ToList());
        var alphabetBytes = Encoding.UTF8.GetBytes(alphabetString);

        //encode text by decoding text using base-alphabet, converting into bigint
        var bigintBytes = BaseConverter.Parse(message, new BaseNAlphabet(alphabetString)).ToByteArray();

        //build message = alphabet length + alphabet bytes + bigint text bytes        
        var messageArray = new byte[1 + alphabetBytes.Length + bigintBytes.Length];
        messageArray[0] = (byte)alphabetBytes.Length;
        Array.Copy(alphabetBytes, 0, messageArray, 1, alphabetBytes.Length);
        Array.Copy(bigintBytes, 0, messageArray, alphabetBytes.Length + 1, bigintBytes.Length);

        //encode message in base-bottom and return message
        return BaseConverter.ToBaseN(new BigInteger(messageArray), bottomAlphabet);
    }

    private static string Decode(string message, KeyboardLayout keyboardLayout)
    {
        if (String.IsNullOrEmpty(message))
        {
            throw new ArgumentNullException();
        }

        if (!KeyboardLayout.ValidateKeyboardLayout(message, keyboardLayout))
        {
            throw new InvalidKeyboardLayoutException("Invalid characters detected in Message that do not exist in specified Keyboard Layout.");
        }

        var bottomAlphabet = new BaseNAlphabet(keyboardLayout.Alphabet);

        //decode message from base-bottom to bigint then convert to byte array
        var messageArray = BaseConverter.Parse(message, bottomAlphabet).ToByteArray();

        //pull alphabet from bigint byte array
        var alphabetLength = (int)messageArray[0];
        var alphabetBytes = new byte[alphabetLength];
        Array.Copy(messageArray, 1, alphabetBytes, 0, alphabetLength);

        //pull text from bigint byte array
        var bigintBytes = new byte[messageArray.Length - (alphabetLength + 1)];
        Array.Copy(messageArray, 1 + alphabetBytes.Length, bigintBytes, 0, bigintBytes.Length);

        //decode by encoding bigint bytes in base-alphabet and return text
        return BaseConverter.ToBaseN(new BigInteger(bigintBytes), new BaseNAlphabet(Encoding.UTF8.GetString(alphabetBytes)));
    }

    /// <summary>
    /// Encodes message into Bottom Text with default QWERTY keyboard layout.
    /// </summary>
    /// <param name="message">The unencoded text to encode.</param>
    /// <returns>Encoded text in QWERTY Bottom Text</returns>
    public static string Encode(string message)
    {
        return BottomText.Encode(message, KeyboardLayout.QWERTY);
    }

    /// <summary>
    /// Decodes message from Bottom Text using default QWERTY keyboard layout.
    /// </summary>
    /// <param name="message">The encoded text encoded in QWERTY</param>
    /// <returns>Decoded text</returns>
    public static string Decode(string message)
    {
        return BottomText.Decode(message, KeyboardLayout.QWERTY);        
    }

    /// <summary>
    /// Encodes message into Bottom Text with specified keyboard layout.
    /// </summary>
    /// <param name="message">The unencoded text to encode.</param>
    /// <param name="keytype">The keyboard layout to encode with.</param>
    /// <returns>Encoded text in keytype keyboard layout.</returns>
    public static string Encode(string message, string keytype)
    {
        return BottomText.Encode(message, KeyboardLayout.GetLayout(keytype));
    }

    /// <summary>
    /// Decodes message from Bottom Text using specified keyboard layout
    /// </summary>
    /// <param name="message">The encoded text in specified keytype</param>
    /// <param name="keytype">The specified keytype that message was encoded with</param>
    /// <returns>Decoded Text</returns>
    public static string Decode(string message, string keytype)
    {
        return BottomText.Decode(message, KeyboardLayout.GetLayout(keytype));
    }

    /// <summary>
    /// Gets a list of Keyboard Layouts supported by library.
    /// </summary>
    /// <returns>Enuberable collecton of supported keyboard layouts</returns>
    public static IEnumerable<String> GetSupportedKeyboardLayouts()
    {
        return KeyboardLayout.ListLayouts();
    }

}
