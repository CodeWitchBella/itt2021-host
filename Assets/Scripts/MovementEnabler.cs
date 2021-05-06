using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementEnabler : MonoBehaviour
{
    public MouseHandler mouseHandler;
    public VRRecenter recenter;

    void Start()
    {
        Destroy(gameObject);
        if (mouseHandler) mouseHandler.movementEnabled = true;
        if (recenter) Destroy(recenter);
    }
}
