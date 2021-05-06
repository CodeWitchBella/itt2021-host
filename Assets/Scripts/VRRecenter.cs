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
        StartCoroutine("WaitForTrack");
    }

    IEnumerator WaitForTrack()
    {
        //while(driver.)
        //yield return null;
        yield return null;
    }

    void Update()
    {
        var pos = -cam.position;
        pos.y = 0;
        if (pos.magnitude > 0.25) {
            transform.position = pos;
        }
    }
}
