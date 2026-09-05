using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

// DEBUG INFO SCRIPT
public class D_RecruitEntry : MonoBehaviour
{
    [SerializeField]
    private Image image;
    [SerializeField]
    private TMP_Text displayName;
    [SerializeField]
    private TMP_Text level;

    public void Setup(Sprite _icon, string _name, int _level)
    {
        image.sprite = _icon;
        displayName.SetText(_name);
        level.SetText($"Lvl: {_level.ToString()}");
    }
}
