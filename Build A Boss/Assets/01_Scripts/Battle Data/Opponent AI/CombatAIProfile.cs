using System.Collections.Generic;
using System.Linq;
using UnityEngine;

//[CreateAssetMenu(menuName = "Build A Boss/ AI / Combat AI Profile")]
public abstract class CombatAIProfile : ScriptableObject
{
    //public float chanceOfAttack;
    //public float chanceOfDefend;
    //public float chanceOfSpecialMove;

    public class WeightedAction
    {
        public BattleActionType actionType;
        public int weight;
    }

    public List<WeightedAction> actionWeights = new();

    //public abstract BattleAction IntelligientActionSelection(BattleEntity self, BattleContext context);

    public virtual BattleActionType SimpleActionSelection(BattleEntity self, BattleContext context)
    {
        int totalWeight = actionWeights.Sum(a  => a.weight);

        int roll = Random.Range(0, totalWeight);

        int runningTotal = 0;

        foreach (WeightedAction action in actionWeights)
        {
            runningTotal += action.weight;
            if (runningTotal > roll)
            {
                BattleActionType selectedAction = action.actionType;
                return selectedAction;
            }
        }

        return BattleActionType.Attack;
    }

    public SpecialMove SelectSpecialMove(BattleEntity self, BattleContext context)
    {
        int index = Random.Range(0, self.KnownMoves.Count - 1);
        return self.KnownMoves[index];
    }
}
