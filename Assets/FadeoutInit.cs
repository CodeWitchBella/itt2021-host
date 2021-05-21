using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeoutInit : MonoBehaviour
{

    private RawImage img;
    private float alpha;
    private GameObject waitForStart;

    // Start is called before the first frame update
    void Start()
    {
        alpha = 0.0f;
        img = gameObject.GetComponent<RawImage>();
        waitForStart = GameObject.Find("WaitForStart");
    }

    // Update is called once per frame
    void Update()
    {
        if (waitForStart.GetComponent<WaitForStart>().timeRemaining < 91) {
            alpha += (Time.deltaTime / 90.0f);
            gameObject.GetComponent<RawImage>().color = new Color(0.0f, 0.0f, 0.0f, alpha);
        }
    }
}
