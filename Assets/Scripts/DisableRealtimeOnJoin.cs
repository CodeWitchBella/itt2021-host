using System;
using System.Collections;
using System.Collections.Generic;
using Normal.Realtime;
using UnityEngine;

public class DisableRealtimeOnJoin : MonoBehaviour
{
    void Start()
    {
        GetComponent<Realtime>().didConnectToRoom += OnConnect;
    }

    private void OnConnect(Realtime realtime)
    {
        var rt = GetComponent<Realtime>();
        if (rt) rt.didConnectToRoom -= OnConnect;
        Destroy(realtime);
        Destroy(this);
    }

    void OnDestroy()
    {
        var rt = GetComponent<Realtime>();
        if (rt) rt.didConnectToRoom -= OnConnect;
    }
}
