using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(menuName = "Build A Boss/Entities/Minion")]
public class MinionClass : OpponentClass
{
    [Header("Minion Info")]
    public MinionSpeciesType species;
    public Gender gender;
    public PersonalityTypeClass personality;
    public Nations habitat;
    //public int approvalStat;

    public List<Ingratiate> ingratiatesThatWork;
    public List<Ingratiate> hatedIngratiates;

    // dropped items list
    // info about preferred methods of recruitment
    // list of ingratiates that work
}
