using System.Runtime.Serialization;

namespace BottomTextTranslator
{
    [Serializable]
    internal class InvalidKeyboardLayoutException : Exception
    {
        public InvalidKeyboardLayoutException()
        {
        }

        public InvalidKeyboardLayoutException(string? message) : base(message)
        {
        }

        public InvalidKeyboardLayoutException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected InvalidKeyboardLayoutException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}