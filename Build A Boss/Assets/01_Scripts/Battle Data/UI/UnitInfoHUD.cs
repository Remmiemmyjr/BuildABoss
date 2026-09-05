using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;

public class UnitInfoHUD : MonoBehaviour
{
    [SerializeField] TMP_Text nameText;
    [SerializeField] TMP_Text levelText;
    [SerializeField] HPBar hpBar;
    [SerializeField] ManaBar manaBar;

    public void SetData(BattleEntity battleUnit)
    {
        nameText.text = battleUnit.Entity.displayName;
        levelText.text = "Lvl " + battleUnit.Entity.level;

        battleUnit.OnHealthChanged += UpdateHPBar;
        battleUnit.OnManaChanged += UpdateManaBar;

        hpBar.InitHPBar((float)battleUnit.currHP / battleUnit.Entity.baseStats.maxHP);
        manaBar?.InitManaBar((float)battleUnit.currMana / battleUnit.Entity.baseStats.maxMana);
    }

    public void UpdateHPBar(BattleEntity battleUnit)
    {
        hpBar.SetHP((float)battleUnit.currHP / battleUnit.Entity.baseStats.maxHP);
    }

    public void UpdateManaBar(BattleEntity battleUnit)
    {
        manaBar?.SetMana((float)battleUnit.currMana / battleUnit.Entity.baseStats.maxMana);
    }
}
