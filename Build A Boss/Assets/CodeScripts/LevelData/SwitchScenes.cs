using UnityEngine;
using UnityEngine.SceneManagement;

public class SwitchScenes : MonoBehaviour
{
    public string SceneNameToLoad;
    private void OnTriggerEnter(Collider other)
    {
        SceneManager.LoadScene(SceneNameToLoad, LoadSceneMode.Single);
    }
}
