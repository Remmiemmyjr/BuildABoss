using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public static class RecruitList
{
    // do i want a list or a dictionary?
    static List<MinionClass> Recruits;
    static List<MinionClass> RecruitsWaitList;
    static List<MinionClass> SelectedRecruitList;
    static int recruitCapacity;
    static int remainingCapacity;

    // Allow player to recruit whatever minions, but they should be added to a temporary list first due to the capacity, then after a run player can
    // select which minions they would like to actually bring with them. 

    public static List<MinionClass> GetListOfRecruits()
    {
        return Recruits;
    }

    public static List<MinionClass> GetWaitList()
    {
        return RecruitsWaitList;
    }

    public static void AddNewRecruit(MinionClass recruit)
    {
        // should add selected minion to recruit list
        Recruits.Add(recruit);
    }

    public static void AddRecruitToWaitList(MinionClass recruit)
    {
        RecruitsWaitList.Add(recruit);
    }
}
