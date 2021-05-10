using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreakTrigger : MonoBehaviour
{
    
    AudioSource source;

    void Start() {
        source = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other) {
        if (other.tag == "Player") {
            source.Stop();
            source.Play();
        }
    }
}
