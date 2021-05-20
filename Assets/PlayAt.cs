using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAt : MonoBehaviour
{
    // Start is called before the first frame update

    public float playTime;
    private bool playing = false;
    private AudioSource asrc;
    void Start()
    {
        asrc = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.timeSinceLevelLoad >= playTime && !playing) {
            asrc.Play();
            playing = true;
        }
    }
}
