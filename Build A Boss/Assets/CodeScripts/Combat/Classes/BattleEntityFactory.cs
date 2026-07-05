using UnityEngine;


public static class BattleEntityFactory
{
    public static BattleEntity CreateFromPlayerBoss(PlayerBossInstance _boss)
    {
        BattleEntity bEntity = new BattleEntity(_boss);

        //bEntity.currHP = boss.currHP;
        //bEntity.currMana = boss.currMana;
        //bEntity.statusCondition = boss.statusCondition;

        return bEntity;
    }

    public static BattleEntity CreateFromOpponent(OpponentProfileInstance _opponent)
    {
        BattleEntity bEntity = new BattleEntity(_opponent);

        bEntity.CombatAI = DB_CombatAI.GetProfile[_opponent.AIType];

        //bEntity.currHP = opponent.currHP;
        //bEntity.currMana = opponent.currMana;
        //bEntity.statusCondition = opponent.statusCondition;


        return bEntity;
    }
}
