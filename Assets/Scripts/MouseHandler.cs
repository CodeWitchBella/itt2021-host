using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MouseHandler : MonoBehaviour
{
    private float mouseSpeed = 0.125f;
    public bool movementEnabled = false;
    public float MovementSpeed = 2;
    private float xRotation = 0.0f;
    private float yRotation = 0.0f;
    private Camera cam = null;
    public GameObject tutorial = null;
    public Transform xTransform = null;
    public Transform yTransform = null;
    private float speedMultiplier = 1;

    private bool locked
    {
        set
        {
            if (value) Cursor.lockState = CursorLockMode.Locked;
            else Cursor.lockState = CursorLockMode.None;
        }
        get { return Cursor.lockState == CursorLockMode.Locked; }
    }

    PlayerPositionPreserver preserver;

    void Start()
    {
        speedMultiplier = AnimatorDebugger.GetSpeed();
        cam = FindObjectOfType<Camera>();
        locked = true;
        if (xTransform == null) xTransform = cam.transform;
        if (yTransform == null) yTransform = cam.transform;
        preserver = FindObjectOfType<PlayerPositionPreserver>();
        if (preserver) {
            xRotation = preserver.transform.eulerAngles.x;
            yRotation = preserver.transform.eulerAngles.y;
            transform.position = preserver.transform.position;
        } else {
            var preserverObj = new GameObject("PlayerPositionPreserver");
            preserver = preserverObj.AddComponent<PlayerPositionPreserver>();
        }
        foreach (var o in FindObjectsOfType<Normal.Realtime.RealtimeTransform>()) {
            o.RequestOwnership();
        }
        foreach (var o in FindObjectsOfType<Normal.Realtime.RealtimeView>()) {
            o.RequestOwnership();
        }
    }

    void OnApplicationFocus(bool hasFocus)
    {
        locked = hasFocus;
    }

    private float movementEnabledAt = -1;
    private bool didMove = false;

    float GetHorizontal()
    {
        var k = Keyboard.current;
        if (k == null) return 0;
        return (k.dKey.isPressed ? 1 : 0) + (k.aKey.isPressed ? -1 : 0) + (k.leftArrowKey.isPressed ? -1 : 0) + (k.rightArrowKey.isPressed ? 1 : 0);
    }
    float GetVertical()
    {
        var k = Keyboard.current;
        if (k == null) return 0;
        return (k.wKey.isPressed ? 1 : 0) + (k.sKey.isPressed ? -1 : 0) + (k.downArrowKey.isPressed ? -1 : 0) + (k.upArrowKey.isPressed ? 1 : 0);
    }

    void Update()
    {
        var c = Gamepad.current;
        var m = Mouse.current;
        var rightStick = (c?.rightStick.ReadValue() ?? Vector2.zero) * 0.75f + (m?.delta.ReadValue() ?? Vector2.zero) * mouseSpeed;
        var leftStick = c?.leftStick.ReadValue() ?? Vector2.zero;
        float mouseX = locked ? rightStick.x : 0;
        float mouseY = locked ? rightStick.y : 0;

        yRotation += mouseX;
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90, 90);

        xTransform.eulerAngles = new Vector3(xTransform.eulerAngles.x, yRotation, 0.0f);
        yTransform.eulerAngles = new Vector3(xRotation, xTransform.eulerAngles.y, 0.0f);


        float horizontal = movementEnabled ? GetHorizontal() + leftStick.x : 0;
        float vertical = movementEnabled ? GetVertical() + leftStick.y : 0;
        horizontal *= MovementSpeed;
        vertical *= MovementSpeed;

        var tr = cam.transform.rotation * (Vector3.right * horizontal + Vector3.forward * vertical) * Time.deltaTime * speedMultiplier;
        var mag = tr.magnitude;
        if (mag > 0.01 && !didMove) {
            didMove = true;
            if (tutorial) tutorial.SetActive(false);
        }
        tr.y = 0;
        tr = tr.normalized;
        if (tr.sqrMagnitude < 0.001) {
            tr = cam.transform.rotation * (Vector3.right * horizontal + Vector3.up * vertical) * Time.deltaTime * speedMultiplier;
            tr.y = 0;
            tr = tr.normalized;
        }
        tr *= mag;
        tr += transform.localPosition;
        tr.y = 0;
        transform.localPosition = tr;
        if (movementEnabled) {
            if (movementEnabledAt == -1) {
                movementEnabledAt = Time.fixedTime;
            }
            if (Time.fixedTime - movementEnabledAt > 3 && !didMove) {
                if (tutorial) tutorial.SetActive(true);
            }
        }
        preserver.transform.localPosition = transform.localPosition;
        preserver.transform.eulerAngles = new Vector3(xRotation, yRotation, 0.0f);

        if (Input.GetKey(KeyCode.Escape)) {
            locked = false;
        }

        if (Input.GetMouseButton(0)) {
            locked = true;
        }
    }
}
