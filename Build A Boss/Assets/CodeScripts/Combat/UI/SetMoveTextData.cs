using UnityEngine;
using TMPro;

public class SetMoveTextData : MonoBehaviour
{
    public TMP_Text manaCost;
    public TMP_Text description;
    public SpecialMove move;

    public void SetManaText()
    {
        description.text = move.description;
        string costText;

        if (PlayerDataManager.Instance.Boss.currMana > move.manaCost)
            costText = $"Mana Cost: {move.manaCost}";
        else
            costText = "Not Enough Mana";
        manaCost.text = costText;
    }

    public void ClearManaText()
    {
        description.text = $"Select A Move";
        string costText = $"";
        manaCost.text = costText;
    }
}
