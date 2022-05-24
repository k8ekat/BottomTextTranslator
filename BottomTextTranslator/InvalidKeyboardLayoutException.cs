namespace BottomTextTranslator;

sealed class InvalidKeyboardLayoutException : Exception
{
    public InvalidKeyboardLayoutException(string? message) : base(message)
    {
    }
}
