using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectGlitch : MonoBehaviour
{
    // Start is called before the first frame update
    private float currentTime;
    public float glitchStartTime1 = -10;
    public float glitchStartTime2 = -10;
    public float glitchStartTime3 = -10;
    public float glitchStartTime4 = -10;
    public float glitchStartTime5 = -10;

    public float deformStartTime = 320.0f;
    public float glitchDuration = 0.3f;
    private float deformDuration = 70;

    private bool glitching;
    private bool deforming;

    private string materialName;
    private Renderer objectRenderer;
    void Start()
    {
        
        objectRenderer = GetComponent<Renderer>();

        materialName = objectRenderer.material.ToString().Split(' ')[0];

        glitching = false;
        deforming = false;
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("Update");
        currentTime = Time.time;
        if (currentTime >= glitchStartTime1 && currentTime < glitchStartTime1 + glitchDuration && !glitching && !deforming) {
            //Debug.Log("switching to glitch at: " + currentTime);
            this.objectRenderer.material = (Material)Resources.Load("Materials/" + materialName + "Glitch", typeof(Material));
            glitching = true;
        }
        else if (currentTime >= glitchStartTime2 && currentTime < glitchStartTime2 + glitchDuration && !glitching && !deforming) {
            //Debug.Log("switching to glitch at: " + currentTime);
            this.objectRenderer.material = (Material)Resources.Load("Materials/" + materialName + "Glitch", typeof(Material));
            glitching = true;
        }
        else if (currentTime >= glitchStartTime3 && currentTime < glitchStartTime3 + glitchDuration && !glitching && !deforming) {
            //Debug.Log("switching to glitch at: " + currentTime);
            this.objectRenderer.material = (Material)Resources.Load("Materials/" + materialName + "Glitch", typeof(Material));
            glitching = true;
        }
        else if (currentTime >= glitchStartTime4 && currentTime < glitchStartTime4 + glitchDuration && !glitching && !deforming) {
            //Debug.Log("switching to glitch at: " + currentTime);
            this.objectRenderer.material = (Material)Resources.Load("Materials/" + materialName + "Glitch", typeof(Material));
            glitching = true;
        }
        else if (currentTime >= glitchStartTime5 && currentTime < glitchStartTime5 + glitchDuration && !glitching && !deforming) {
            //Debug.Log("switching to glitch at: " + currentTime);
            this.objectRenderer.material = (Material)Resources.Load("Materials/" + materialName + "Glitch", typeof(Material));
            glitching = true;
        }
        else if (currentTime >= deformStartTime && !deforming) {
            Debug.Log("switching to deform at: " + currentTime + " deformTime is: " + deformStartTime);
            //Debug.Log(this.ToString());
            this.objectRenderer.material = (Material)Resources.Load("Materials/" + materialName + "Deform", typeof(Material));
            deforming = true;
            deformStartTime = currentTime;
        }
        else if (currentTime >= glitchStartTime1 + glitchDuration && glitching && !deforming && currentTime >=1) {
            //Debug.Log("switchign to original at: " + currentTime);
            this.objectRenderer.material = (Material)Resources.Load("Materials/" + materialName, typeof(Material));
            glitching = false;
        }
        if (currentTime >= deformStartTime + deformDuration && deformStartTime >= 1 && deforming) {
            //Debug.Log("Destroying: " + this.ToString());
            Destroy(gameObject);
        }
    }
}
