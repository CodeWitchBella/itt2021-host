using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRPositionRestorer : MonoBehaviour
{
    void Start()
    {
        var o = FindObjectOfType<PlayerPositionPreserver>();
        if (o) {
            transform.position = o.transform.position;
            transform.rotation = o.transform.rotation;
            Destroy(o);
        }
        Destroy(this);
    }
}
