using UnityEngine;


public class DetectContactWithPlayer : MonoBehaviour
{
    // NOTE: Put this on Player Controller instead, use collision.gameobject.opponentInstance and make an if statement depending on who it is, pass bool to request battle.
    private OpponentController controller;
    bool isHero = false;

    private void Start()
    {
        controller = GetComponent<OpponentController>();
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("Player"))
        {
            BattleEvents.RequestBattle(controller.opponentInstance, isHero);
        }
    }
}
