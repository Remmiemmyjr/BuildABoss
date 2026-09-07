using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;

// DEBUG INFO SCRIPT
public class D_RecruitContentManager : MonoBehaviour
{
    [SerializeField] private D_RecruitEntry entry;
    [SerializeField] private Transform container;


    //private void OnEnable()
    //{
    //    RecruitList.RecruitAdded += GrabMinionFromList;
    //}

    //private void OnDisable()
    //{
    //    RecruitList.RecruitAdded -= GrabMinionFromList;
    //}

    // Dynamic adding
    public void GrabMinionFromList(MinionClass _minion)
    {
        var newEntry = Instantiate(entry, container);
        newEntry.Setup(_minion.sprite, _minion.name, _minion.level);
    }




    // Alternative, destroy and generate the list whenever debug ui is opened/closed. 100% reflective, but inefficient
    public void GenerateVisualList()
    {
        List<MinionClass> recruitListCopy = PlayerRefManager.Ref.BossInstance.recruitList.GetListOfRecruits();
        foreach (MinionClass _minion in recruitListCopy)
        {
            var newEntry = Instantiate(entry, container);
            newEntry.Setup(_minion.sprite, _minion.name, _minion.level);
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
