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
        StartCoroutine(MicrophoneInit());
    }

    IEnumerator MicrophoneInit()
    {
        yield return Application.RequestUserAuthorization(UserAuthorization.Microphone);
        if (Application.HasUserAuthorization(UserAuthorization.Microphone)) {
            Debug.Log("Microphone found");
        } else {
            Debug.Log("Microphone not found");
        }
        foreach (var device in Microphone.devices) {
            Microphone.Start(device, false, 10, 44100);
            Microphone.End(device);
        }
        HandleInitStep();
    }

    int initCounter = 2;
    void HandleInitStep()
    {
        initCounter--;
        if (initCounter <= 0) {
            Destroy(this);
        }
    }

    private void OnConnect(Realtime realtime)
    {
        var rt = GetComponent<Realtime>();
        if (rt) rt.didConnectToRoom -= OnConnect;
        Destroy(realtime);

        HandleInitStep();
    }

    void OnDestroy()
    {
        var rt = GetComponent<Realtime>();
        if (rt) rt.didConnectToRoom -= OnConnect;
    }
}
