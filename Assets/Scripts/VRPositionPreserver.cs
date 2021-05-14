using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRPositionPreserver : MonoBehaviour
{
    PlayerPositionPreserver GetPreserver()
    {
        foreach (var p in FindObjectsOfType<PlayerPositionPreserver>()) {
            return p;
        }
        var preserverObj = new GameObject("PlayerPositionPreserver");
        return preserverObj.AddComponent<PlayerPositionPreserver>();
    }
    PlayerPositionPreserver preserver;

    void Start()
    {
        preserver = GetPreserver();
    }

    // Update is called once per frame
    void Update()
    {
        preserver.transform.position = transform.position;
        preserver.transform.rotation = transform.rotation;
    }
}
