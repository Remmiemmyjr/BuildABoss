using UnityEngine;

public class OpponentController : MonoBehaviour
{
    public OpponentProfileInstance opponentInstance { get; private set; }

    public void SetInstance(OpponentProfileInstance _opponentInstance)
    {
        opponentInstance = _opponentInstance;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
