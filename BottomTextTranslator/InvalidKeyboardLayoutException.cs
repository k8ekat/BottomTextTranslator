namespace BottomTextTranslator;

public class InvalidKeyboardLayoutException : Exception
{
    public InvalidKeyboardLayoutException(string? message) : base(message)
    {
    }
}
