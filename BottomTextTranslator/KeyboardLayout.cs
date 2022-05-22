namespace BottomTextTranslator;

public class KeyboardLayout
{
    private KeyboardLayout(string value) { Alphabet = value; }

    public string Alphabet { get; private set; }

    public static KeyboardLayout QWERTY { get { return new KeyboardLayout("asdfghjkl;"); } }
    public static KeyboardLayout Dvorak { get { return new KeyboardLayout("aoeuidhtns"); } }
    public static KeyboardLayout Colemak { get { return new KeyboardLayout("arstdhneio"); } }
    public static KeyboardLayout ColemakDH { get { return new KeyboardLayout("arstgmneio"); } }
    public static KeyboardLayout Workman { get { return new KeyboardLayout("ashtgyneoi"); } }

    public static KeyboardLayout GetLayout(string Layout)
    {
        var propertyInfo = typeof(KeyboardLayout).GetProperty(Layout);
        return propertyInfo?.GetValue(null) as KeyboardLayout ?? KeyboardLayout.QWERTY;
    }

}
