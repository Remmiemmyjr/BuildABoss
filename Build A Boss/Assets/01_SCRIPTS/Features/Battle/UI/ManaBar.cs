using System.Collections;
using UnityEngine;

public class ManaBar : MonoBehaviour
{
    // Only the Boss's Mana is shown during combat, so can be specific if desired
    [SerializeField] GameObject manaBar;

    public void InitManaBar(float amount)
    {
        manaBar.transform.localScale = new Vector3(amount, manaBar.transform.localScale.y);
    }

    public void SetMana(float manaNormalized)
    {
        StartCoroutine(SetSmoothMana(manaNormalized));
    }

    IEnumerator SetSmoothMana(float newMana)
    {
        float currMana = manaBar.transform.localScale.x;
        //float changeAmount = currHP - newMana;

        while (Mathf.Abs(currMana - newMana) > Mathf.Epsilon)
        {
            //currHP -= changeAmount * Time.deltaTime * .75f;
            currMana = Mathf.MoveTowards(currMana, newMana, Time.deltaTime * 0.25f);
            manaBar.transform.localScale = new Vector3(currMana, 1f); 
            yield return null;
        }
        manaBar.transform.localScale = new Vector3(newMana, manaBar.transform.localScale.y);
    }
}
