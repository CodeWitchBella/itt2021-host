using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerOnPlayerEnter : MonoBehaviour
{
    public GameObject[] activate;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponentInChildren<Camera>()) {
            foreach (var o in activate) {
                o.SetActive(true);
            }
            Destroy(gameObject);
        }
    }
}
