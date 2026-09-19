using NUnit.Framework;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class HPBar : MonoBehaviour 
{
    [SerializeField] GameObject hpBar;

    public void InitHPBar(float amount)
    {
        hpBar.transform.localScale = new Vector3(amount, hpBar.transform.localScale.y);
    }

    public void SetHP(float newHP)
    {
        StartCoroutine(SetSmoothHP(newHP));
    }

    IEnumerator SetSmoothHP(float newHP)
    {
        float currHP = hpBar.transform.localScale.x;
        //float changeAmount = currHP - newHP;

        while (Mathf.Abs(currHP - newHP) > Mathf.Epsilon)
        {
            //currHP -= changeAmount * Time.deltaTime; // TODO: make speed vary depending on damage amount and super effectiveness
            currHP = Mathf.MoveTowards(currHP, newHP, Time.deltaTime * 0.25f);
            hpBar.transform.localScale = new Vector3(currHP, 1f); 
            yield return null;
        }
        hpBar.transform.localScale = new Vector3(newHP, hpBar.transform.localScale.y);
    }
}
