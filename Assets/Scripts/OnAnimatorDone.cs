using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class OnAnimatorDone : MonoBehaviour
{
    public UnityAction OnDone;
    public GameObject[] activate;
    public string LoadScene;
    public bool DeactivateSelf = true;

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
        if (DeactivateSelf) {
            gameObject.SetActive(false);
        }
        if (LoadScene != "") {
            SceneManager.LoadScene(LoadScene, LoadSceneMode.Single);
        }
    }
}
