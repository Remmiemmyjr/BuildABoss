using UnityEngine;

[CreateAssetMenu(menuName = "Build A Boss/Moves/Ingratiate")]
public class Ingratiate : ScriptableObject
{
    public string ingratiateName;
    public int approvalBoost;
    public MinionSpeciesType effectiveSpecies;
}
