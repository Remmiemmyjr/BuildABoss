using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTeleporter : MonoBehaviour
{
    public RoomDoorTypes targetEntryPoint;
    public string targetScene;


    private void OnTriggerEnter(Collider other)
    {
        SceneTransitionManager.Instance.LoadRoom(targetScene, targetEntryPoint);
    }
}
