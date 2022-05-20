using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BottomTextTranslator;

public class KeyboardLayout
{
    private KeyboardLayout(string value) { Alphabet = value; }

    public string Alphabet { get; private set; }

    public static KeyboardLayout QWERTY {  get { return new KeyboardLayout("asdfghjkl;"); } }
    public static KeyboardLayout Dvorak { get { return new KeyboardLayout("aoeuidhtns"); } }
    public static KeyboardLayout Colemak { get { return new KeyboardLayout("arstdhneio"); } }
    public static KeyboardLayout ColemakDH { get { return new KeyboardLayout("arstgmneio"); } }
    public static KeyboardLayout Workman { get { return new KeyboardLayout("ashtgyneoi")} }

}
