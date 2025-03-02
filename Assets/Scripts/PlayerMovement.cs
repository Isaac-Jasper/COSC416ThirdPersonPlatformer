using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    [SerializeField] 
    private float speed;
    [SerializeField]
    private float jumpForce;
    [SerializeField]
    int jumps;

    private int jumpsRemaining;

    [SerializeField]
    private Transform CameraDirection;
    [SerializeField]
    GroundChecker groundChecker;
    [SerializeField]
    private Rigidbody rb;

    void Start() {
        InputController.instance.OnMove.AddListener(MovePlayer);
        InputController.instance.OnSpacePressed.AddListener(Jump);
        rb = GetComponent<Rigidbody>();
        jumpsRemaining = jumps;
    }

    private void MovePlayer(Vector3 dir) {
        Quaternion rotation = Quaternion.Euler(0,CameraDirection.rotation.eulerAngles.y,0);
        dir = rotation * dir;
        rb.AddForce(speed*dir);
    }
    void OnCollisionEnter(Collision col) {
        if (groundChecker.IsGrounded()) { 
            jumpsRemaining = jumps;
        }
    }

    public void Jump() {
        if (jumpsRemaining > 0) {
            jumpsRemaining--;
            rb.linearVelocity = new Vector3(rb.linearVelocity.x,0,rb.linearVelocity.z);
            rb.AddForce(Vector3.up*jumpForce, ForceMode.Impulse);
        }
    }
}
