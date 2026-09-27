using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;

// DEBUG INFO SCRIPT
public class D_RecruitContentManager : MonoBehaviour
{
    [SerializeField] private D_RecruitEntry entry;
    [SerializeField] private Transform container;

    // Dynamic adding
    public void GrabMinionFromList(MinionProfileInstance _minion)
    {
        var newEntry = Instantiate(entry, container);
        newEntry.Setup(_minion.BaseClass.sprite, _minion.BaseClass.name, _minion.Level);
    }

    // Alternative, destroy and generate the list whenever debug ui is opened/closed. 100% reflective, but inefficient
    public void GenerateVisualList()
    {
        List<MinionProfileInstance> recruitListCopy = RecruitListManager.Instance.GetListOfRecruits();
        foreach (MinionProfileInstance _minion in recruitListCopy)
        {
            var newEntry = Instantiate(entry, container);
            newEntry.Setup(_minion.BaseClass.sprite, _minion.BaseClass.name, _minion.Level);
        }
    }

    public void DeleteVisualList()
    {
        foreach (Transform child in container)
        {
            Destroy(child.gameObject);
        }
    }
}
