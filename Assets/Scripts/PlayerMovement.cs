using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    [SerializeField] 
    private float speed;
    [SerializeField]
    private Transform CameraDirection;

    [SerializeField]
    private Rigidbody rb;

    void Start() {
        InputController.instance.OnMove.AddListener(MovePlayer);
        rb = GetComponent<Rigidbody>();
    }

    private void MovePlayer(Vector3 dir) {
        Quaternion rotation = Quaternion.Euler(0,CameraDirection.rotation.eulerAngles.y,0);
        dir = rotation * dir;
        rb.linearVelocity = speed * dir;
    }
}
