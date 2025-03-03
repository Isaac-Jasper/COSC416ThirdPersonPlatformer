using UnityEngine;

public class CursorLocker : MonoBehaviour {
    void Start() {
        InputController.instance.OnExitPressed.AddListener(UnlockCursor);
        InputController.instance.OnMouse0Pressed.AddListener(LockCursor);
    }
    private void LockCursor() {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void UnlockCursor() {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}
