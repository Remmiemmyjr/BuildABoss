using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InitIngratiateHUD : MonoBehaviour
{
    [SerializeField] List<TMP_Text> ingratiateNames;

    void Awake()
    {
        for (int i = 0; i < ingratiateNames.Count; i++)
        {
            ingratiateNames[i].transform.parent.gameObject.SetActive(true);
        }
    }

    public void InitIngratiateButtons(List<Ingratiate> ingratiate)
    {
        //choiceManager.AssignSpecialMoves(spMoves);
        for (int i = 0; i < ingratiateNames.Count; i++)
        {
            if (i < ingratiate.Count)
            {
                GameObject button = ingratiateNames[i].transform.parent.gameObject;

                ingratiateNames[i].text = ingratiate[i].ingratiateName;
            }
            else
            {
                ingratiateNames[i].transform.parent.gameObject.SetActive(false);
            }
        }
    }
}
