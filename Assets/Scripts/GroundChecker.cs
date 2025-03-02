using UnityEngine;

public class GroundChecker : MonoBehaviour
{
    [SerializeField]
    private float radius;
    [SerializeField]
    public LayerMask jumpMask;

    public bool IsGrounded() {
        if (Physics.CheckSphere(transform.position, radius,jumpMask)) {
            return true;
        }
        return false;
    }
}
