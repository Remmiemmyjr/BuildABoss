using UnityEngine;

public class PlayerRefManager : MonoBehaviour
{
    public static PlayerRefManager Ref {  get; private set; }
    public BossProfileInstance BossInstance { get; private set; }

    [SerializeField]
    private BossClass bossClass;

    private void Awake()
    {
        if(Ref != null)
        {
            Destroy(gameObject);
            return;
        }

        Ref = this;
        DontDestroyOnLoad(gameObject);
        BossInstance = new BossProfileInstance(bossClass);
    }
}
