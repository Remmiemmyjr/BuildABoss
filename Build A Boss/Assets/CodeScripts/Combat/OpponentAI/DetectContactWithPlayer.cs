using UnityEngine;

public class DetectContactWithPlayer : MonoBehaviour
{
    private OpponentController controller;

    private void Start()
    {
        controller = GetComponent<OpponentController>();
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("Player"))
        {
            //StartCoroutine(BattleManager.Instance.BeginBattle(controller.minionInstance));
            BattleEvents.RequestBattle(controller.opponentInstance);
        }
    }
}
