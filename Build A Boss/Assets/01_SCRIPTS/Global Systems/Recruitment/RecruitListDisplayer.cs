using NUnit.Framework;
using System.Collections.Generic;
using System;
using UnityEngine;

public class RecruitListDisplayer : MonoBehaviour
{
    [SerializeField] private GameObject entryBlockPrefab; // must implement IRecruitEntryView
    [SerializeField] private Transform listContainer;

    // These are used just to be able to pass methods as function parameters. 
    private Action<MinionProfileInstance> onEntrySelected;
    private Func<MinionProfileInstance, bool> isAvailable; // could be a boolean, depends on how i set up my Minion.isavailable logic

    public void Configure(Action<MinionProfileInstance> _onEntrySelected = null, Func<MinionProfileInstance, bool> _isAvailable = null)
    {
        this.onEntrySelected = _onEntrySelected;
        this.isAvailable = _isAvailable;
    }

    public void Refresh(List<MinionProfileInstance> _recruits)
    {
        Clear();
        Debug.Log($"Refreshing list...");
        foreach (var minion in _recruits)
        {
            var entryObj = Instantiate(entryBlockPrefab, listContainer);
            if(!entryObj.TryGetComponent(out IRecruitEntryViewBlock view))
            {
                Debug.Log($"{entryBlockPrefab.name} has no IRecruitEntryViewBlock component");
                return;
            }
            view.Bind(minion, onEntrySelected);
            if (isAvailable != null)
                view.SetAvailable(isAvailable(minion));
        }
    }

    public void Clear()
    {
        foreach (Transform child in listContainer)
            Destroy(child.gameObject);
    }
}
