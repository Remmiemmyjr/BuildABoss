using UnityEngine;

public class PlayerRefGetter : MonoBehaviour
{
    public static PlayerRefGetter Instance {  get; private set; }
    public BossProfileInstance PlayerInstance { get; private set; }

    [SerializeField]
    private BossData bossClass;

    private void Awake()
    {
        if(Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
        PlayerInstance = new BossProfileInstance(bossClass);
    }
}
