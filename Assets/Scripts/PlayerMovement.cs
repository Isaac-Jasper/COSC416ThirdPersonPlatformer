using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    [SerializeField] 
    private float speed;

    [SerializeField]
    private Rigidbody rb;

    void Start() {
        InputController.instance.OnMove.AddListener(MovePlayer);
        rb = GetComponent<Rigidbody>();
    }

    private void MovePlayer(Vector3 dir) {
        rb.AddForce(speed * dir);
    }
}
