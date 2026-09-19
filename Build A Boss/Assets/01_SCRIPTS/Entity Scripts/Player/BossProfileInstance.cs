using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class BossProfileInstance : EntityProfileInstance
{
    #region Variables
    [Header("Class Data")]
    public BossClass bossClass;
    public int reputation;
    public int currXP;
    public List<Ingratiate> KnownIngratiates;
    public RecruitList recruitList;

    public PlayerController playerController;
    #endregion

    // Constructor
    public BossProfileInstance(BossClass _bossClass) : base(_bossClass)
    {
        bossClass = _bossClass;
        
        level = 1;
        currXP = 0;
        reputation = 0;
        KnownIngratiates = _bossClass.knownIngratiates;
        recruitList = new RecruitList();
        //playerController = new PlayerController();
    }
}
