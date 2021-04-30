using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class OnAnimatorDone : MonoBehaviour
{
    public UnityAction OnDone;
    public GameObject[] activate;

    void Start()
    {
        OnDone += Handler;
    }

    void Destroy()
    {
        OnDone -= Handler;
    }

    private void Handler()
    {
        foreach (var o in activate) {
            o.SetActive(true);
        }
    }
}
