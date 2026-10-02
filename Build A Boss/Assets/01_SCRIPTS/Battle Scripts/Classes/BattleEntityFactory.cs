using UnityEngine;


public static class BattleEntityFactory
{
    public static BattleEntity CreateFromPlayerBoss(BossProfileInstance _boss)
    {
        BattleEntity bEntity = new BattleEntity(_boss);

        return bEntity;
    }

    public static BattleEntity CreateFromOpponent(OpponentProfileInstance _opponent)
    {
        BattleEntity bEntity = new BattleEntity(_opponent);
        return bEntity;
    }
}
