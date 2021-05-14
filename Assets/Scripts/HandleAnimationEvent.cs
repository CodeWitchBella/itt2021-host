using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class HandleAnimationEvent : MonoBehaviour
{
    public UnityAction OnDone;
    public GameObject[] activate;
    public bool LoadMultiplayer = false;
    public bool DeactivateSelf = true;
    public bool HideSelf = false;
    public string eventName = "Done";

    void Start()
    {
        OnDone += Handler;
    }

    void Destroy()
    {
        OnDone -= Handler;
    }


    private bool called;
    public bool wasCalled { get { return called; } }
    private void Handler()
    {
        if (called) return;
        called = true;

        foreach (var o in activate) {
            o.SetActive(true);
        }
        if (DeactivateSelf) {
            gameObject.SetActive(false);
        }
        if (HideSelf) {
            foreach (var r in GetComponentsInChildren<Renderer>()) {
                r.enabled = false;
            }
        }
        if (LoadMultiplayer) {
            var mult = FindObjectOfType<SkipToMultiplayer>();
            if (mult) mult.LoadMultiplayer();
            else SceneManager.LoadScene("4 Multiplayer", LoadSceneMode.Single);
        }
    }
}
