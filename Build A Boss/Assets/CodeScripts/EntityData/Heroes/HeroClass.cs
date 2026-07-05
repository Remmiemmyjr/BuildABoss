using UnityEngine;

[CreateAssetMenu(menuName = "Build A Boss/Entities/Hero")]
public class HeroClass : OpponentClass
{
    [Header ("Hero Info")]
    public Gender gender;
    public PersonalityTypeClass personality;
    public Nations nation;
}
