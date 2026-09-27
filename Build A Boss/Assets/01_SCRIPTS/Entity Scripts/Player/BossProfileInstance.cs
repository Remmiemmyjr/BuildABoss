using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class BossProfileInstance : EntityProfileInstance
{
    #region Variables
    [Header("Class Data")]
    public string Nickname;
    //public BossClass BossClass;
    public int Reputation;
    public int CurrXP;
    public List<Ingratiate> KnownIngratiates;
    #endregion

    // Constructor
    public BossProfileInstance(BossClass _bossClass) : base(_bossClass)
    {
        //BossClass = _bossClass;
        Level = 1;
        CurrXP = 0;
        Reputation = 0;
        KnownIngratiates = _bossClass.knownIngratiates;
    }
}
