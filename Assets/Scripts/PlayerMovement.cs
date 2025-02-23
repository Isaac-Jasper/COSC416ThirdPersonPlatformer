using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    [SerializeField] private float speed;

    private Rigidbody rb;

    void Start() {
        InputController.instance.OnMove.AddListener(MovePlayer);
        rb = GetComponent<Rigidbody>();
    }

    private void MovePlayer(Vector2 dir) {
        rb.AddForce(speed * dir);
    }
}
