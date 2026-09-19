using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(menuName = "Build A Boss/Entities/The Boss")]
public class BossClass : EntityClass
{
    public string chosenNickname;
    public int reputation;

    public List<Ingratiate> knownIngratiates;


    // inventory / equipment
    // perk?

    // probably need a constructor to set chosennickname to displayname
}
