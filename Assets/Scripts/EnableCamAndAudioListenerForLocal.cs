using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableCamAndAudioListenerForLocal : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (GetComponent<Normal.Realtime.RealtimeView>().isOwnedLocallyInHierarchy) {
            GetComponent<AudioListener>().enabled = true;
            GetComponent<Camera>().enabled = true;
        }
        Destroy(this);
    }

}
