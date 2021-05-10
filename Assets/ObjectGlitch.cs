using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectGlitch : MonoBehaviour
{
    // Start is called before the first frame update
    public float glitchStartTime;
    private Renderer objectRenderer;
    private bool glitching;
    public float glitchDuration = 0.3f;
    void Start()
    {
        objectRenderer = GetComponent<Renderer>();
        glitching = false;
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("Update");

        if (Time.time >= glitchStartTime && Time.time < glitchStartTime + glitchDuration && !glitching) {
            //this.GetComponent<MeshRenderer>().material = (Material)Resources.Load("GlitchMaterial", typeof(Material));
            //GetComponent<MeshRenderer>().material = (Material)Resources.Load("GlitchMaterial", typeof(Material));
            this.objectRenderer.material = (Material)Resources.Load("GlitchMaterial", typeof(Material));
            glitching = true;
        }
        if (Time.time >= glitchStartTime + glitchDuration && glitching) {
            this.objectRenderer.material = (Material)Resources.Load("NormalMaterial", typeof(Material));
            glitching = false;
        }
    }
}
