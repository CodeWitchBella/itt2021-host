using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerOnPlayerEnter : MonoBehaviour
{
    public GameObject[] activate;
    public float delay = 0;
    private bool scheduled = false;

    void OnTriggerEnter(Collider other)
    {
        if (scheduled) return;
        if (other.gameObject.GetComponentInChildren<Camera>()) {
            scheduled = true;
            Invoke("ActivateTargets", delay);
        }
    }

    void ActivateTargets()
    {
        foreach (var o in activate) {
            o.SetActive(true);
        }
        Destroy(gameObject);
    }
}
