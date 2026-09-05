using UnityEngine;
using System.Collections;

public class ApprovalBar : MonoBehaviour
{
    [SerializeField] GameObject approvalBar;

    public void InitApprovalBar(float _amount)
    {
        approvalBar.transform.localScale = new Vector3(_amount, approvalBar.transform.localScale.y);
    }

    public void SetApproval(float _newA)
    {
        if (_newA > 1)
            _newA = 1;
        StartCoroutine(SetSmoothApproval(_newA));
    }

    IEnumerator SetSmoothApproval(float _newA)
    {
        Mathf.Clamp(_newA, 0, 1);

        float currApproval = approvalBar.transform.localScale.x;
        //float changeAmount = currApproval - _newA;

        while (Mathf.Abs(currApproval - _newA) > Mathf.Epsilon)
        {
            currApproval = Mathf.MoveTowards(currApproval, _newA, Time.deltaTime * 0.25f);
            approvalBar.transform.localScale = new Vector3(currApproval, 1f);
            yield return null;
        }
        approvalBar.transform.localScale = new Vector3(_newA, approvalBar.transform.localScale.y);
    }
}
