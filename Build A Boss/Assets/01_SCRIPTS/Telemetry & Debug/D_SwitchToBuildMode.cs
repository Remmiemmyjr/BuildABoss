using UnityEngine;
using UnityEngine.InputSystem;

public class D_SwitchToBuildMode : MonoBehaviour
{
    GameObject playerCam;
    public GameObject gridCam;
    public static bool inBuildMode = false;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        inBuildMode = false;
        gridCam.SetActive(false);
        playerCam = GameObject.FindWithTag("MainCamera");
    }


    // Update is called once per frame
    void Update()
    {
        if (Keyboard.current.spaceKey.wasPressedThisFrame) 
        {
            if (inBuildMode == false)
            {
                gridCam.SetActive(true);
                playerCam.SetActive(false);
                inBuildMode = true;
            }
            else
            {
                gridCam.SetActive(false);
                playerCam.SetActive(true);
                inBuildMode = false;
            }
        } 
            
    }
}
