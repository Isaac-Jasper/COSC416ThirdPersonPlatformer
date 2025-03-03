using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    [SerializeField] 
    private float maxSpeed, dashSpeed, accelerationTime, jumpHeight, playerGravity;
    [SerializeField]
    private int jumps;
    [SerializeField]
    private AnimationCurve MoveSpeedUpCurve, MoveSpeedDownCurve, JumpVelocityUpCurve, JumpVelocityDownCurve;
    [SerializeField]
    private Transform CameraTransform;
    [SerializeField]
    GroundChecker groundChecker;
    [SerializeField]
    RoofChecker roofChecker;
    [SerializeField]
    private Rigidbody rb;

    private float jumpStartHeight, fallStartHeight;
    private Vector3 HorizontalPlaneVelocity;
    private int jumpsRemaining;
    private float currentMoveTime = 0;
    private bool doJump = false;
    private bool doMove = false;

    void Start() {
        InputController.instance.OnMove.AddListener(HorizontalPlanePlayerMovement);
        InputController.instance.OnSpacePressed.AddListener(Jump);
        InputController.instance.OnDashPressed.AddListener(OnDash);

        rb = GetComponent<Rigidbody>();

        jumpsRemaining = jumps;
        fallStartHeight = transform.position.y;
    }

    void Update() {
        if (doMove && currentMoveTime < accelerationTime) {
            currentMoveTime += Time.deltaTime;
        } else if (doMove && currentMoveTime >= accelerationTime) {
            currentMoveTime = accelerationTime;
        }

        if (!doMove && currentMoveTime > 0) {
            currentMoveTime -= Time.deltaTime;
        } else if (!doMove && currentMoveTime <= 0) {
            currentMoveTime = 0;
        }

        rb.linearVelocity = PlayerGravityHandler() + HorizontalPlaneVelocity;
    }

    private void OnDash() {
        //rb.AddForce(dashSpeed*HorizontalPlaneVelocity);
        //Couldn't get dash to work with how I implemented movment. I would need to
        //add a dash acceleration and decceleration curve, lock movment during dash,
        //and then fiddle with the curve to get it right.
    }

    //Uses the animation curves to control the player movment on the x-z plane
    private void HorizontalPlanePlayerMovement(Vector3 dir) { 
        if (dir != Vector3.zero) {  //if direction is non zero, accelerate
            doMove = true;
            Quaternion rotation = Quaternion.Euler(0,CameraTransform.rotation.eulerAngles.y,0);
            dir = rotation * dir;
            HorizontalPlaneVelocity = maxSpeed * MoveSpeedUpCurve.Evaluate(currentMoveTime/accelerationTime) * dir;
        } else { //if direction is 0, deccelerate
            doMove = false;
            HorizontalPlaneVelocity = MoveSpeedDownCurve.Evaluate(currentMoveTime/accelerationTime)*maxSpeed*HorizontalPlaneVelocity.normalized;
        }
    }

    private Vector3 PlayerGravityHandler() {
        if (doJump & transform.position.y - jumpStartHeight >= 0) { //while going up from jump use up jump animation curve
            float JumpVelocity= JumpVelocityUpCurve.Evaluate(1 - (transform.position.y - jumpStartHeight)/jumpHeight);
            if (transform.position.y - jumpStartHeight >= jumpHeight-0.05) {
                doJump = false; 
                fallStartHeight = transform.position.y;
            }
            return JumpVelocity*playerGravity*Vector3.up;
        }
        else { //while falling use jump down animation curve
            float fallVelocity = JumpVelocityDownCurve.Evaluate((fallStartHeight-transform.position.y)/jumpHeight) + 0.01f;
            return fallVelocity*playerGravity*Vector3.down;
        }
    }

    public void Jump() {
        if (jumpsRemaining > 0) {
            if (jumpsRemaining == jumps && !groundChecker.IsGrounded()) {
                jumpsRemaining--; //already fell off platform
            }
            jumpsRemaining--;
            doJump = true;
            jumpStartHeight = transform.position.y;
            rb.linearVelocity = new Vector3(rb.linearVelocity.x,0,rb.linearVelocity.z);
        }
    }

    void OnCollisionEnter(Collision col) {
        if (groundChecker.IsGrounded()) { //on collison with ground set start fall height to current height and reset jumps
            fallStartHeight = transform.position.y;
            jumpsRemaining = jumps;
        }

        if (roofChecker.DidBonk()) { //on collsion with roof cancel jump
            fallStartHeight = transform.position.y;
            doJump = false; 
        }
    }
}