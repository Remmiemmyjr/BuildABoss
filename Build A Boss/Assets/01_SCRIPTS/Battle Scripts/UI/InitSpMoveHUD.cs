using UnityEngine;
using UnityEngine.UI;
using TMPro;
using NUnit.Framework;
using System.Collections.Generic;

public class InitSpMoveHUD : MonoBehaviour
{
    [SerializeField] List<TMP_Text> spMoveNames;

    void Awake()
    {
        for (int i = 0; i < spMoveNames.Count; i++)
        {
            spMoveNames[i].transform.parent.gameObject.SetActive(true);
        }
    }

    public void InitSpMoveButtons(List<SpecialMove> spMoves)
    {
        //choiceManager.AssignSpecialMoves(spMoves);
        for (int i = 0; i < spMoveNames.Count; i++)
        {
            if (i < spMoves.Count)
            {
                GameObject button = spMoveNames[i].transform.parent.gameObject;

                spMoveNames[i].text = spMoves[i].moveName;
                button.GetComponent<SetMoveTextData>().move = spMoves[i];
            }
            else
            {
                spMoveNames[i].transform.parent.gameObject.SetActive(false);
            }
        }
    }

}
