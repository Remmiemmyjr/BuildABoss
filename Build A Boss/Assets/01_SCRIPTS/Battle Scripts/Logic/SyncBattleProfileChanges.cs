using UnityEngine;

public static class SyncBattleProfileChanges
{
    public static void SaveBackToPlayerBoss(BattleEntity battleEntity, BattleContext context)
    {
        PlayerRefGetter.Instance.PlayerInstance.CurrHP = battleEntity.currHP;
        PlayerRefGetter.Instance.PlayerInstance.CurrMana = battleEntity.currMana;
        PlayerRefGetter.Instance.PlayerInstance.CurrStatusCondition = battleEntity.statusCondition;
    }
}
