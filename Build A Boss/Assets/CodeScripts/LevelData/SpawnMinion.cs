using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class SpawnMinion : MonoBehaviour
{
    public MinionClass minionClass;
    public GameObject prefab;
    public List<Transform> listOfSpawnpoints;

    private void Start()
    {
        for(int i = 0; i < listOfSpawnpoints.Count; i++)
            SpawnRandomOpponent(listOfSpawnpoints[i]);
    }

    //public void SpawnRandomMinion()
    //{
    //    GameObject newMinion = Instantiate(prefab, transform.position, Quaternion.identity);
    //    MinionInstance instance = new MinionInstance(minionClass);
    //    MinionController controller = newMinion.GetComponent<MinionController>();
    //    controller.SetInstance(instance);
    //}

    public void SpawnRandomOpponent(Transform spawnPoint)
    {
        GameObject newMinion = Instantiate(prefab, spawnPoint.position, Quaternion.identity);
        OpponentProfileInstance instance = new OpponentProfileInstance(minionClass);
        OpponentController controller = newMinion.GetComponent<OpponentController>();
        controller.SetInstance(instance);
        controller.opponentInstance.livingObject = newMinion;
    }
}
