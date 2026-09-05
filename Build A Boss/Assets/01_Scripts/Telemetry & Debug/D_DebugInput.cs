using UnityEngine;
using UnityEngine.InputSystem;

public class D_DebugInput : MonoBehaviour
{
    D_RecruitContentManager recruitListDebug;
    [SerializeField] GameObject debugCanvas;

    bool debugEnabled = false;

    void Start()
    {
        debugCanvas.GetComponent<Canvas>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            if (!debugEnabled)
            {
                debugCanvas.GetComponent<Canvas>().enabled = true;
                debugEnabled = true;
            }
            else
            {
                debugCanvas.GetComponent<Canvas>().enabled = false;
                debugEnabled = false;
            }
        }
    }
}
