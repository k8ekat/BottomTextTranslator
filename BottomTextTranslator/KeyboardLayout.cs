namespace BottomTextTranslator;

sealed class KeyboardLayout
{
    private KeyboardLayout(string value) { Alphabet = value; }

    //private KeyboardLayout() : this(string.Empty) {  }

    public string Alphabet { get; private set; }

    public static KeyboardLayout QWERTY { get { return new KeyboardLayout("asdfghjkl;"); } }
    public static KeyboardLayout Dvorak { get { return new KeyboardLayout("aoeuidhtns"); } }
    public static KeyboardLayout Colemak { get { return new KeyboardLayout("arstdhneio"); } }
    public static KeyboardLayout ColemakDH { get { return new KeyboardLayout("arstgmneio"); } }
    public static KeyboardLayout Workman { get { return new KeyboardLayout("ashtgyneoi"); } }

    public static KeyboardLayout GetLayout(string Layout)
    {
        var propertyInfo = typeof(KeyboardLayout).GetProperty(Layout);
        return propertyInfo?.GetValue(null) as KeyboardLayout ?? throw new InvalidKeyboardLayoutException($"Invalid Keyboard Layout specified. Valid layouts are: {String.Join(", ", KeyboardLayout.ListLayouts())}");
    }

    public static IEnumerable<String> ListLayouts()
    {
        var layouts = typeof(KeyboardLayout).GetProperties();
        return layouts.Where(x => x.GetAccessors(false)[0].IsStatic).Select(x => x.Name);
    }

    public static bool ValidateKeyboardLayout(string message, KeyboardLayout alphabet)
    {
        foreach(var c in message.Distinct().ToArray())
        {
            if(!alphabet.Alphabet.Contains(c))
            {
                return false;
            }
        }

        return true;
    }

}
