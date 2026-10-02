using Codice.Client.Common.GameUI;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LairMinionPost : MonoBehaviour, ISelectableLairAsset
{
    [HideInInspector] public MinionProfileInstance assignedMinion;
    [SerializeField] private SpriteRenderer displaySprite;
    [SerializeField] private GameObject canvas;
    [SerializeField] private RecruitListDisplayer listDisplay;

    private void Awake()
    {
        canvas.SetActive(false);
        ClearAssignee();
    }

    public void OnSelected()
    {
        //throw new System.NotImplementedException();
        // show canvas list of minions they can select from
        listDisplay.Configure(_onEntrySelected: AssignToPost);
        listDisplay.Refresh(RecruitListManager.Instance.GetListOfRecruits());
        canvas.SetActive(true);
    }

    public void OnDeselected()
    {
        canvas.SetActive(false);
    }


    void AssignToPost(MinionProfileInstance _minion)
    {
        assignedMinion = _minion;
        displaySprite.sprite = _minion.Entity.sprite;
        Debug.Log("You clicked the entry!!!!!!!");
        // assign minion to spot
    }

    public void ClearAssignee()
    {
        assignedMinion = null;
        displaySprite.sprite = null;
    }

    // unselecting or clicking off hides the canvas
}