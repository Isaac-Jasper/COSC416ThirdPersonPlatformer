using UnityEngine;
using UnityEngine.Events;

public class CoinScript : MonoBehaviour {
    [SerializeField]
    public UnityEvent CoinCollected = new UnityEvent();
    [SerializeField]
    private float spinSpeed;

    void Update() {
        transform.Rotate(spinSpeed * Time.deltaTime * Vector3.up);
    }

    void OnTriggerEnter(Collider other) {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player")) {
            CoinCollected?.Invoke();
            Destroy(gameObject);
        }
    }
}
