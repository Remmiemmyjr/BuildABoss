using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransitionManager : MonoBehaviour
{
    public static SceneTransitionManager Instance { get; private set; }
    private RoomDoorTypes targetEntry;

    void Awake()
    {
        if(Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void LoadRoom(string _nextScene, RoomDoorTypes _targetEntry)
    {
        targetEntry = _targetEntry;
        SceneManager.sceneLoaded += OnSceneLoaded;
        SceneManager.LoadScene(_nextScene);
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
        SpawnPlayerAtEntry(targetEntry);
    }

    void SpawnPlayerAtEntry(RoomDoorTypes _targetEntry)
    {
        SceneEntryPoint[] entryPoints = FindObjectsByType<SceneEntryPoint>(FindObjectsSortMode.None);

        foreach (SceneEntryPoint entryPoint in entryPoints)
        {
            if (entryPoint.entryType == _targetEntry)
            {
                PlayerRefManager.Ref.BossInstance.playerController.gameObject.transform.SetPositionAndRotation(entryPoint.transform.position,
                                                                                                               entryPoint.transform.rotation);
                return;
            }
        }
    }
}
