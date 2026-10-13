using UnityEngine;

// TODO: Make this a generic tool and put it in Core/Utils
public class CanvasVisibility : MonoBehaviour
{
    public GameObject canvas;
    public GameObject battleCamera;

    private void Awake()
    {
        canvas.SetActive(false);
        battleCamera.SetActive(false);

        BattleEvents.BattleStarted += TurnOn;
        BattleEvents.BattleEnded += TurnOff;
    }


    public void TurnOn()
    {
        // Animation
        canvas.SetActive(true);
        battleCamera.SetActive(true);
    }

    public void TurnOff()
    {
        canvas.SetActive(false);
        battleCamera.SetActive(false);
        // Animation
    }
}
