using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class PlayerBossInstance : EntityProfileInstance
{
    public BossClass bossClass;
    public int reputation;
    public int currXP;
    public List<Ingratiate> KnownIngratiates;

    // TODO: need to change this to minion instance class
    public List<MinionClass> recruitedMinions = new();


    // Constructor
    public PlayerBossInstance(BossClass _bossClass) : base(_bossClass)
    {
        bossClass = _bossClass;
        
        level = 1;
        currXP = 0;
        reputation = 0;
        KnownIngratiates = _bossClass.knownIngratiates;
    }
}
