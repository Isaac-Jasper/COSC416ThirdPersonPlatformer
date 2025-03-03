using UnityEngine;

public class RoofChecker : MonoBehaviour
{
    [SerializeField]
    private float radius;
    [SerializeField]
    public LayerMask BonkMask;

    public bool DidBonk() {
        if (Physics.CheckSphere(transform.position, radius, BonkMask)) {
            return true;
        }
        return false;
    }
}
