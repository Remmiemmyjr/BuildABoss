using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Build A Boss/Lair/Normal Asset")]
public class LairAsset : ScriptableObject
{
    [field: SerializeField]
    public string Name { get; private set; }
    [field: SerializeField]
    public Vector2Int GridSize { get; private set; } = Vector2Int.one;
    [field: SerializeField]
    public GameObject Prefab { get; private set; }

    private int currAmount;
    public event Action OnCurrAmountChanged;

    public virtual void OnPlace() {}
    public virtual void OnRemove() {}


    public int GetCurrAmount()
    {
        return currAmount;
    }

    public void SetCurrAmount(int _amount)
    {
        currAmount = _amount;
    }

    public void ChangeCurrAmountBy(int _amount)
    {
        currAmount += _amount;
        OnCurrAmountChanged.Invoke();
    }
}
