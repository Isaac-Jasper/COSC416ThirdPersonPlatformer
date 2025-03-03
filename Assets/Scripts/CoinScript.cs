using UnityEngine;

public class CoinScript : MonoBehaviour {
    [SerializeField]
    private float spinSpeed;

    void Update() {
        transform.Rotate(spinSpeed * Time.deltaTime * Vector3.up);
    }
    
    void OnTriggerEnter(Collider other) {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player")) {
            //increment score
            Destroy(gameObject);
        }
    }
}
