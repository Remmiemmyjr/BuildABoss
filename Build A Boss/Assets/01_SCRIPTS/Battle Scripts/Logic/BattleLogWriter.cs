using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using TMPEffects.Components;
using TMPro;
using UnityEngine;

public class BattleLogWriter : MonoBehaviour
{
    public TMP_Text battleLogText;
    public TMPWriter writer;

    // RunTurn() takes queue of actions it needs to print. 

    public async Awaitable PrintTurnMessage(string _message, float wait)
    {
        var completion = new AwaitableCompletionSource();
        bool done = false;

        void Complete(TMPWriter _writer)
        {
            if (done) return;
            done = true;
            completion.SetResult();
        }
        void CompleteOnSkip(TMPWriter _writer, int skippedIndex) => Complete(_writer);

        writer.OnFinishWriter.AddListener(Complete);
        writer.OnSkipWriter.AddListener(CompleteOnSkip);
        writer.OnCharacterShown.AddListener((w, c) => Debug.Log($"{Time.unscaledTime:F3} shown idx {writer.CurrentIndex}"));
        battleLogText.text = _message;

        await completion.Awaitable;

        writer.OnFinishWriter.RemoveListener(Complete);
        writer.OnSkipWriter.RemoveListener(CompleteOnSkip);
        
        await Awaitable.WaitForSecondsAsync(wait, destroyCancellationToken);
    }
}
