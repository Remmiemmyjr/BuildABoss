using System.Collections;
using UnityEngine;
using TMPro;

public static class TextTyper
{
    private static float speed = 60f;

    public static IEnumerator TypeText(TMP_Text text, string words)
    {
        text.text = "";
        foreach (var letter in words)
        {
            text.text += letter;
            yield return new WaitForSeconds(1/speed);
        }
        yield return new WaitForSeconds(1.5f);
    }
}
