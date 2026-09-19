using UnityEngine;
using UnityEngine.InputSystem;

public class BuildToolActivator : MonoBehaviour
{
    GameObject playerCam;
    public GameObject gridCam;
    public static bool inBuildMode = false;
    public MouseInputLairBuilding mouseInputLair;

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
        mouseInputLair.ToolActivated();
        PlayerRefManager.Ref.GetComponent<PlayerInput>().SwitchCurrentActionMap("LairBuilding");
    }    

    // Update is called once per frame
    public void DeactivateLairBuilder()
    {
        gridCam.SetActive(false);
        playerCam.SetActive(true);
        inBuildMode = false;
        mouseInputLair.ToolDeactivated();
        PlayerRefManager.Ref.GetComponent<PlayerInput>().SwitchCurrentActionMap("Gameplay");
    }
}
