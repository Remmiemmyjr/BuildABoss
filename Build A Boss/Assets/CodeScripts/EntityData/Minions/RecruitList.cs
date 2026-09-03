using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class RecruitList 
{
    // do i want a list or a dictionary?
    List<MinionClass> Recruits;
    List<MinionClass> RecruitsWaitList;
    List<MinionClass> SelectedRecruitList;
    int recruitCapacity;
    int remainingCapacity;

    // Allow player to recruit whatever minions, but they should be added to a temporary list first due to the capacity, then after a run player can
    // select which minions they would like to actually bring with them. 

    public List<MinionClass> GetListOfRecruits()
    {
        return Recruits;
    }

    public List<MinionClass> GetWaitList()
    {
        return RecruitsWaitList;
    }

    public void AddNewRecruit(MinionClass recruit)
    {
        // should add selected minion to recruit list
        Recruits.Add(recruit);
    }

    public void AddRecruitToWaitList(MinionClass recruit)
    {
        RecruitsWaitList.Add(recruit);
    }
}
