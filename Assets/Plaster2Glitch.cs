using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plaster2Glitch : MonoBehaviour
{
    // Start is called before the first frame update
    public float glitchStartTime1;
    //public float glitchStartTime2;
    private Renderer objectRenderer;
    private bool glitching;
    public float glitchDuration = 0.3f;
    void Start()
    {
        objectRenderer = GetComponent<Renderer>();
        glitching = false;
        glitchStartTime1 = glitchStartTime1 + Random.Range(-2, 2);
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("Update");

        if (Time.time >= glitchStartTime1 && Time.time < glitchStartTime1 + glitchDuration && !glitching) {
            //this.GetComponent<MeshRenderer>().material = (Material)Resources.Load("GlitchMaterial", typeof(Material));
            //GetComponent<MeshRenderer>().material = (Material)Resources.Load("GlitchMaterial", typeof(Material));
            this.objectRenderer.material = (Material)Resources.Load("Materials/Plaster (2) 1", typeof(Material));
            glitching = true;
        }
        if (Time.time >= glitchStartTime1 + glitchDuration && glitching) {
            this.objectRenderer.material = (Material)Resources.Load("Materials/Plaster (2)", typeof(Material));
            glitching = false;
        }
    }
}
