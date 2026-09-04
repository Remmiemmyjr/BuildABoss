using UnityEngine;
using UnityEngine.SceneManagement;

public class SwitchScenes : MonoBehaviour
{
    public string SceneToLoad;


    private void OnTriggerEnter(Collider other)
    {
        PlayerRefManager.Ref.BossInstance.SwitchScenes(SceneManager.GetActiveScene().name, SceneToLoad);
        SceneManager.LoadSceneAsync(SceneToLoad);
    }
}
