using UnityEngine;

public static class SyncBattleProfileChanges
{
    public static void SaveBackToPlayerBoss(BattleEntity battleEntity, BattleContext context)
    {
        PlayerDataManager.Instance.Boss.currHP = battleEntity.currHP;
        PlayerDataManager.Instance.Boss.currMana = battleEntity.currMana;
        PlayerDataManager.Instance.Boss.statusCondition = battleEntity.statusCondition;
    }
}
