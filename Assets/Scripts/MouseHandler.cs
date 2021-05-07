using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseHandler : MonoBehaviour
{
    // horizontal rotation speed
    public float horizontalSpeed = 1f;
    // vertical rotation speed
    public float verticalSpeed = 1f;
    public bool movementEnabled = false;
    public float MovementSpeed = 1;
    private float xRotation = 0.0f;
    private float yRotation = 0.0f;
    private Camera cam = null;
    public GameObject tutorial = null;
    CharacterController characterController;

    private bool locked
    {
        set
        {
            if (value) Cursor.lockState = CursorLockMode.Locked;
            else Cursor.lockState = CursorLockMode.None;
        }
        get { return Cursor.lockState == CursorLockMode.Locked; }
    }

    void Start()
    {
        cam = FindObjectOfType<Camera>();
        characterController = GetComponent<CharacterController>();
        locked = true;
    }

    void OnApplicationFocus(bool hasFocus)
    {
        locked = hasFocus;
    }

    private float movementEnabledAt = -1;
    private bool didMove = false;

    void Update()
    {
        if (locked) {
            float mouseX = Input.GetAxis("Mouse X") * horizontalSpeed;
            float mouseY = Input.GetAxis("Mouse Y") * verticalSpeed;

            yRotation += mouseX;
            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, -90, 90);

            cam.transform.eulerAngles = new Vector3(xRotation, yRotation, 0.0f);

            if (movementEnabled) {
                float horizontal = Input.GetAxis("Horizontal") * MovementSpeed;
                float vertical = Input.GetAxis("Vertical") * MovementSpeed;
                var tr = cam.transform.rotation * (Vector3.right * horizontal + Vector3.forward * vertical) * Time.deltaTime;
                var mag = tr.magnitude;
                if (mag > 0.01 && !didMove) {
                    didMove = true;
                    if (tutorial) tutorial.SetActive(false);
                }
                tr.y = 0;
                tr = tr.normalized;
                if (tr.sqrMagnitude < 0.001) {
                    tr = cam.transform.rotation * (Vector3.right * horizontal + Vector3.up * vertical) * Time.deltaTime;
                    tr.y = 0;
                    tr = tr.normalized;
                }
                tr *= mag;
                characterController.Move(tr);
                if (movementEnabledAt == -1) {
                    movementEnabledAt = Time.fixedTime;
                }
                if (Time.fixedTime - movementEnabledAt > 3 && !didMove) {
                    if (tutorial) tutorial.SetActive(true);
                }
            }
        }
        if (Input.GetKey(KeyCode.Escape)) {
            locked = false;
        }

        if (Input.GetMouseButton(0)) {
            locked = true;
        }
    }
}
