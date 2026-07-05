using UnityEngine;

public static class SyncBattleProfileChanges
{
    public static void SaveBackToPlayerBoss(BattleEntity battleEntity)
    {
        // status effects need resynced
        //boss.currHP = battleEntity.currHP;
        PlayerDataManager.Instance.Boss.currHP = battleEntity.currHP;
        PlayerDataManager.Instance.Boss.currMana = battleEntity.currMana;
        //boss.currMana = battleEntity.currMana;
    }
}
