using UnityEngine;

public class ApprovalBarDisplay : MonoBehaviour
{
    [SerializeField] ApprovalBar approvalBar;

    public void SetData()
    {
        approvalBar?.InitApprovalBar((float)BattleManager.Instance.Context.Approval / 100);
        BattleManager.Instance.ApprovalChanged += UpdateApprovalBar;
    }

    public void UpdateApprovalBar()
    {
        approvalBar?.SetApproval((float)BattleManager.Instance.Context.Approval / 100);
    }
}
