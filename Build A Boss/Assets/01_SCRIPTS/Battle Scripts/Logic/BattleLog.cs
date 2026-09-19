using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using TMPro;
using UnityEngine;

public class BattleLog : MonoBehaviour 
{
    public static BattleLog Instance { get; private set; }
    public TMP_Text dialogue;

    Queue<string> messages = new();

    private void Awake()
    {
        Instance = this;
    }

    public void QueueMessage(string _message)
    {
        messages.Enqueue(_message);
    }

    public IEnumerator PlayQueue()
    {
        while (messages.Count > 0)
        {
            yield return PlayMessage(messages.Dequeue());
        }
    }

    public Coroutine PlayMessage(string _message)
    {
        return StartCoroutine(TextTyper.TypeText(dialogue, _message));
    }

    public IEnumerator PlayMessageAndWait(string _message)
    {
        yield return StartCoroutine(TextTyper.TypeText(dialogue, _message));
    }

}
