using UnityEngine;

[CreateAssetMenu(fileName = "TextStyle", menuName = "Scriptable Objects/Text Style")]
public class TextStyle : ScriptableObject
{
    [System.Flags]
    public enum ConstantAnimations
    {
        Wave = 0,
        Fade = 1,
        Pivot = 2,
        Funky = 4,
        Char = 8,
        Shake = 16,
        Grow = 32,
        Palette = 64,
        Spread = 128,
        Pivotc = 256,
        Shear = 512,
        Dangle = 1024,
        Sketchy = 2048,
        Swing = 4096,
        Jump = 8192
    }

}
