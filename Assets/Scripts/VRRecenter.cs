using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.XR;

public class VRRecenter : MonoBehaviour
{
    Transform cam;
    TrackedPoseDriver driver;
    void Start()
    {
        cam = GetComponentInChildren<Camera>().transform;
        driver = cam.gameObject.GetComponent<TrackedPoseDriver>();

        var pos = -cam.position;
        pos.y = 0;
        transform.position = pos;
    }

    void Update()
    {
        var pos = gameObject.transform.position + cam.localPosition;
        pos.y = 0;
        if (pos.magnitude > 0.5) {
            pos = -cam.localPosition;
            pos.y = 0;
            transform.position = pos;
        }
    }
}
