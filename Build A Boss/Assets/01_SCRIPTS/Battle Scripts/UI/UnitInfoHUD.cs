using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;

public class UnitInfoHUD : MonoBehaviour
{
    [SerializeField] TMP_Text nameText;
    [SerializeField] TMP_Text levelText;
    [SerializeField] Image image;
    [SerializeField] HPBar hpBar;
    [SerializeField] ManaBar manaBar;

    public void SetData(BattleEntity battleUnit)
    {
        nameText.text = battleUnit.EntityProfile.Entity.displayName;
        levelText.text = "Lvl " + battleUnit.EntityProfile.Level;
        //image.sprite = battleUnit.EntityProfile.Entity.sprite;

        battleUnit.OnHealthChanged += UpdateHPBar;
        battleUnit.OnManaChanged += UpdateManaBar;

        hpBar.InitHPBar((float)battleUnit.currHP / battleUnit.EntityProfile.Stats.maxHP);
        manaBar?.InitManaBar((float)battleUnit.currMana / battleUnit.EntityProfile.Stats.maxMana);
    }

    public void UpdateHPBar(BattleEntity battleUnit)
    {
        hpBar.SetHP((float)battleUnit.currHP / battleUnit.EntityProfile.Stats.maxHP);
    }

    public void UpdateManaBar(BattleEntity battleUnit)
    {
        manaBar?.SetMana((float)battleUnit.currMana / battleUnit.EntityProfile.Stats.maxMana);
    }
}
