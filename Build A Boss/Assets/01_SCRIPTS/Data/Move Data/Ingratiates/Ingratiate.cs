using UnityEngine;

[CreateAssetMenu(menuName = "Build A Boss/Moves/Ingratiate")]
public class Ingratiate : ScriptableObject
{
    public string ingratiateName;
    public int approvalBoost;
    public SpeciesType effectiveSpecies;
    // enum of preferences {loved, neutral, hated}
    // 
}
