using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InputController : MonoBehaviour {
    public static InputController instance { get; private set;}
    public UnityEvent<Vector3> OnMove = new UnityEvent<Vector3>();
    public UnityEvent OnSpacePressed = new UnityEvent();
    public UnityEvent OnDashPressed = new UnityEvent();
    public UnityEvent OnResetPressed = new UnityEvent();

    void Awake() {
        if (instance != null && instance != this) {
            Destroy(this.gameObject);
            return;
        } else {
            instance = this;
        }
    }

    void Update() {
        Vector3 moveDir = Input.GetAxis("Horizontal")*Vector3.right + Input.GetAxis("Vertical")*Vector3.forward;

        OnMove?.Invoke(moveDir);

        if (Input.GetButtonDown("Jump")) {
            OnSpacePressed?.Invoke();
        }

        if (Input.GetButtonDown("Dash")) {
            OnSpacePressed?.Invoke();
        }

        if (Input.GetButtonDown("Reset")) {
            OnResetPressed?.Invoke();
        }
    }
}