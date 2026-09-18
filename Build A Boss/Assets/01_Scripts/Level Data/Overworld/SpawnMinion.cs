using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class SpawnMinion : MonoBehaviour
{
    public List<MinionClass> minionsThatCanSpawn;
    public GameObject prefab; // TODO: Dictionary/map of minions to spawn, Prefab & Quantity?
    public List<Transform> listOfSpawnpoints;

    private void Start()
    {
        for(int i = 0; i < listOfSpawnpoints.Count; i++)
            SpawnRandomOpponent(listOfSpawnpoints[i], minionsThatCanSpawn[(int)Random.Range(0, minionsThatCanSpawn.Count)]);
    }

    //public void SpawnRandomMinion()
    //{
    //    GameObject newMinion = Instantiate(prefab, transform.position, Quaternion.identity);
    //    MinionInstance instance = new MinionInstance(minionClass);
    //    MinionController controller = newMinion.GetComponent<MinionController>();
    //    controller.SetInstance(instance);
    //}

    public void SpawnRandomOpponent(Transform _spawnPoint, MinionClass _minionToSpawn)
    {
        GameObject newMinion = Instantiate(prefab, _spawnPoint.position, Quaternion.identity);
        newMinion.GetComponent<SpriteRenderer>().sprite = _minionToSpawn.sprite;
        OpponentProfileInstance instance = new OpponentProfileInstance(_minionToSpawn);
        OpponentController controller = newMinion.GetComponent<OpponentController>();
        controller.SetInstance(instance);
        controller.opponentInstance.livingObject = newMinion;
    }
}
