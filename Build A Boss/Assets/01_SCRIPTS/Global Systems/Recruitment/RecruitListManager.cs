using System;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

public class RecruitListManager : MonoBehaviour
{
    public static RecruitListManager Instance { get; private set; }
    RecruitList RecruitListInstance;

    public static Action<MinionProfileInstance> RecruitAdded;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
        RecruitListInstance = new RecruitList();
    }


    #region Recruit Management
    public List<MinionProfileInstance> GetListOfRecruits()
    {
        return RecruitListInstance.Recruits;
    }

    public void AddNewRecruit(MinionProfileInstance recruit)
    {
        if (recruit == null)
        {
            Debug.Log("Recruit is null, check what the spawner is instantiating");
            return;
        }
        // should add selected minion to recruit list
        RecruitListInstance.Recruits.Add(recruit);
        RecruitAdded?.Invoke(recruit);
    }
    #endregion

}
