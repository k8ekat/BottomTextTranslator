using System.Runtime.Serialization;

namespace BottomTextTranslator
{
    [Serializable]
    internal class InvalidAlphabetException : Exception
    {
        public InvalidAlphabetException()
        {
        }

        public InvalidAlphabetException(string? message) : base(message)
        {
        }

        public InvalidAlphabetException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected InvalidAlphabetException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}