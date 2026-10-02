using UnityEngine;

[CreateAssetMenu(menuName = "Build A Boss/Entities/Hero")]
public class HeroData : OpponentData
{
    [Header ("Hero Info")]
    public Gender gender;
    //public PersonalityTypeClass personality;
    public Nations nation;
}
