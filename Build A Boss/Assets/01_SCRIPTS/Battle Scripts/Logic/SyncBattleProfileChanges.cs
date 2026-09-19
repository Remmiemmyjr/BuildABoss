using UnityEngine;

public static class SyncBattleProfileChanges
{
    public static void SaveBackToPlayerBoss(BattleEntity battleEntity, BattleContext context)
    {
        PlayerRefManager.Ref.BossInstance.currHP = battleEntity.currHP;
        PlayerRefManager.Ref.BossInstance.currMana = battleEntity.currMana;
        PlayerRefManager.Ref.BossInstance.statusCondition = battleEntity.statusCondition;
    }
}
