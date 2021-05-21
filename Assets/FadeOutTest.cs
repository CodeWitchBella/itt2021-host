using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeOutTest : MonoBehaviour
{

    private RawImage img;
    private float alpha;
    private GameObject waitForStart;

    // Start is called before the first frame update
    void Start()
    {
        alpha = 1.0f;
        gameObject.GetComponent<RawImage>().color = new Color(0.0f,0.0f,0.0f,alpha);
        //clr = img.color;
        //clr.a = 1.0f;
        
        //waitForStart = GameObject.Find("WaitForStart");
    }

    // Update is called once per frame
    void Update()
    {
        alpha -= (Time.deltaTime / 10);
        gameObject.GetComponent<RawImage>().color = new Color(0.0f, 0.0f, 0.0f, alpha);
        //if (waitForStart.GetComponent<WaitForStart>().timeRemaining < 91) {
        //}
    }
}
