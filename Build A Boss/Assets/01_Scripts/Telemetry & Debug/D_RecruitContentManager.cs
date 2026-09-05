using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;

// DEBUG INFO SCRIPT
public class D_RecruitContentManager : MonoBehaviour
{
    [SerializeField] private D_RecruitEntry entry;
    [SerializeField] private Transform container;


    private void OnEnable()
    {
        RecruitList.RecruitAdded += GrabMinionFromList;
    }

    private void OnDisable()
    {
        RecruitList.RecruitAdded -= GrabMinionFromList;
    }

    public void GrabMinionFromList(MinionClass _minion)
    {
        var newEntry = Instantiate(entry, container);
        newEntry.Setup(_minion.sprite, _minion.name, _minion.level);
    }
}
