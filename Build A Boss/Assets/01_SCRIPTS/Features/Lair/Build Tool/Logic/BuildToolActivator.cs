using UnityEngine;

public class BuildToolActivator : MonoBehaviour
{
    GameObject playerCam;
    public GameObject gridCam;
    public static bool inBuildMode = false;

    void Start()
    {
        inBuildMode = false;
        gridCam.SetActive(false);
        playerCam = GameObject.FindWithTag("MainCamera");
    }

    public void Interacted()
    {
        if (!inBuildMode)
        {
            ActivateLairBuilder();
        }
        else
        {
            DeactivateLairBuilder();
        }
    }

    public void ActivateLairBuilder()
    {
        gridCam.SetActive(true);
        playerCam.SetActive(false);
        inBuildMode = true;
        InputManager.Instance.ChangeInputToUI();
    }    

    // Update is called once per frame
    public void DeactivateLairBuilder()
    {
        gridCam.SetActive(false);
        playerCam.SetActive(true);
        inBuildMode = false;
        InputManager.Instance.ChangeInputToPlayer();
        //mouseInputLair.ToolDeactivated();
        //GameManager.Instance.GetComponent<PlayerInput>().SwitchCurrentActionMap("Gameplay");
    }
}
