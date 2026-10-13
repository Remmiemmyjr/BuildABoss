
public interface IBattleUnit
{
    int MaxHP { get; }
    int QueueDamage(int amount);
    void Heal(int amount);
    void ApplyStatusCondition(StatusConditionDefinition statusCondition); 
}
