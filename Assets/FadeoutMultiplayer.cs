using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FadeoutMultiplayer : MonoBehaviour
{
    private RawImage img;

    // Start is called before the first frame update
    void Start()
    {
        img = gameObject.GetComponent<RawImage>();
        img.color = new Color(0.0f, 0.0f, 0.0f, 0.0f);
    }

    [ReadOnly, SerializeField] float timeToFade = -1;
    [ReadOnly, SerializeField] float timePassed = 0;

    public bool wasTriggered
    {
        get { return timeToFade > 0; }
    }

    public void TriggerEnd(float timeToFade)
    {
        this.timeToFade = timeToFade;
        timePassed = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (!wasTriggered) return;

        timePassed += Time.deltaTime;
        float alpha = timePassed / timeToFade;
        if (alpha < 0) alpha = 0;
        if (alpha < 1) {
            img.color = new Color(0.0f, 0.0f, 0.0f, alpha);
        }
    }
}
