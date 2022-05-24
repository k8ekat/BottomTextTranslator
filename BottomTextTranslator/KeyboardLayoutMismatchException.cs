namespace BottomTextTranslator; 

sealed class KeyboardLayoutMismatchException : Exception
{
    public KeyboardLayoutMismatchException(string? message) : base(message)
    {
    }
}
