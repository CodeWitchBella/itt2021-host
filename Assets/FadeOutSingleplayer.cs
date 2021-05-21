using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeOutSingleplayer : MonoBehaviour
{

    private RawImage img;
    private float alpha;

    // Start is called before the first frame update
    void Start()
    {
        alpha = 0.0f;
        img = gameObject.GetComponent<RawImage>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.timeSinceLevelLoad > 307) {
            alpha += (Time.deltaTime / 5.0f);
        }
        gameObject.GetComponent<RawImage>().color = new Color(1.0f, 1.0f, 1.0f, alpha);
    }
}
