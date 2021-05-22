using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeoutMultiplayer : MonoBehaviour
{

    private RawImage img;
    private WaitForStart waitForStart;

    // Start is called before the first frame update
    void Start()
    {
        img = gameObject.GetComponent<RawImage>();
        waitForStart = GameObject.Find("WaitForStart").GetComponent<WaitForStart>();
        gameObject.GetComponent<RawImage>().color = new Color(0.0f, 0.0f, 0.0f, 1.0f);
    }

    float timeRemaining;

    // Update is called once per frame
    void Update()
    {
        var tr = waitForStart.timeRemaining;
        timeRemaining -= Time.deltaTime;
        if (tr - timeRemaining > 1 || tr - timeRemaining < -1) timeRemaining = tr;
        float alpha = 1f - timeRemaining / 90f;
        if (alpha < 0) alpha = 0;
        if (alpha < 1) {
            gameObject.GetComponent<RawImage>().color = new Color(0.0f, 0.0f, 0.0f, alpha);
        }
    }
}
