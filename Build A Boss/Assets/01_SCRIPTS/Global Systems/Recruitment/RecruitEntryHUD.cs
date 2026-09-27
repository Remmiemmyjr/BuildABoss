using log4net.Core;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RecruitEntryHUD : MonoBehaviour, IRecruitEntryViewBlock
{
    [SerializeField] private Image image;
    [SerializeField] private TMP_Text displayName;
    [SerializeField] private TMP_Text level;
    [SerializeField] private Button button;
    [SerializeField] private CanvasGroup container;

    public void Bind(MinionProfileInstance _minion, Action<MinionProfileInstance> _onSelected)
    {
        image.sprite = _minion.BaseClass.sprite;
        displayName.SetText(_minion.BaseClass.name);
        level.SetText($"Lvl: {_minion.BaseClass.level.ToString()}");

        button.onClick.RemoveAllListeners();
        if (_onSelected != null)
        {
            button.onClick.AddListener(() => { Debug.Log("clicked"); _onSelected(_minion); });// lambda syntax
        }
    }

    public void SetAvailable(bool available)
    {
        button.interactable = available;
        container.alpha = available ? 1f : 0.5f; // if the minion is unavailable, disable clicking and make gray
    }
}
