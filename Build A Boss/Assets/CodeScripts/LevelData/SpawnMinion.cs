using UnityEngine;

public class SpawnMinion : MonoBehaviour
{
    public MinionClass minionClass;
    public GameObject prefab;

    private void Start()
    {
        SpawnRandomOpponent();
    }

    //public void SpawnRandomMinion()
    //{
    //    GameObject newMinion = Instantiate(prefab, transform.position, Quaternion.identity);
    //    MinionInstance instance = new MinionInstance(minionClass);
    //    MinionController controller = newMinion.GetComponent<MinionController>();
    //    controller.SetInstance(instance);
    //}



    public void SpawnRandomOpponent()
    {
        GameObject newMinion = Instantiate(prefab, transform.position, Quaternion.identity);
        OpponentProfileInstance instance = new OpponentProfileInstance(minionClass);
        OpponentController controller = newMinion.GetComponent<OpponentController>();
        controller.SetInstance(instance);
    }
}
