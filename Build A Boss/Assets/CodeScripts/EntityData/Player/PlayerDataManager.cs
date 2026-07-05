using UnityEngine;

public class PlayerDataManager : MonoBehaviour
{
    public static PlayerDataManager Instance {  get; private set; }

    [SerializeField]
    private BossClass startingBossDef;

    public PlayerBossInstance Boss { get; private set; }

    private void Awake()
    {
        if(Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
        Boss = new PlayerBossInstance(startingBossDef);
    }
}
