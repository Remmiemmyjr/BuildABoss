using UnityEngine;

public class MinionProfileInstance : OpponentProfileInstance
{
    public string Nickname;
    public int ID;
    public RecruitEntryData RecruitData;
    public struct RecruitEntryData
    {
        bool availableForJob;
        // Dialogue Table
    }

    public MinionProfileInstance(MinionData _minionClass) : base(_minionClass)
    {

    }
}
