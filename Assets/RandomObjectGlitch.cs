using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomObjectGlitch : MonoBehaviour
{
    // Start is called before the first frame update
    private float currentTime;
    public float glitchStartTime1 = 20;
    public float deformTime = 320.0f;
    public float glitchDuration = 0.3f;
    private float deformDuration = 3;

    private float glitchStartTime2;
    private float glitchStartTime3;
    private float glitchStartTime4;
    private float glitchStartTime5;
    private float glitchStartTime6;
    private float glitchStartTime7;
    private float glitchStartTime8;
    private float glitchStartTime9;
    private float glitchStartTime10;

    private float recentGlitchTime;
    private bool glitching;
    private bool deforming;

    private string materialName;
    private Renderer objectRenderer;
    void Start()
    {
        objectRenderer = GetComponent<Renderer>();
        materialName = objectRenderer.material.ToString().Split(' ')[0];  //split to get rid of unity bullshit
        glitching = false;
        deforming = false;
        recentGlitchTime = glitchStartTime1;

        glitchStartTime2 = glitchStartTime1 + Random.Range(20, 40);
        glitchStartTime3 = glitchStartTime2 + Random.Range(20, 40);
        glitchStartTime4 = glitchStartTime3 + Random.Range(20, 40);
        glitchStartTime5 = glitchStartTime4 + Random.Range(20, 40);
        glitchStartTime6 = glitchStartTime5 + Random.Range(20, 40);
        glitchStartTime7 = glitchStartTime5 + Random.Range(20, 40);
        glitchStartTime8 = glitchStartTime6 + Random.Range(20, 40);
        glitchStartTime9 = glitchStartTime6 + Random.Range(20, 40);
        glitchStartTime10 = glitchStartTime6 + Random.Range(20, 40);

        glitchStartTime1 = glitchStartTime1 + Random.Range(-5, 5);
        glitchStartTime2 = glitchStartTime2 + Random.Range(-5, 5);
        glitchStartTime3 = glitchStartTime3 + Random.Range(-5, 5);
        glitchStartTime4 = glitchStartTime4 + Random.Range(-5, 5);
        glitchStartTime5 = glitchStartTime5 + Random.Range(-5, 5);
        glitchStartTime6 = glitchStartTime6 + Random.Range(-5, 5);
        glitchStartTime7 = glitchStartTime7 + Random.Range(-5, 5);
        glitchStartTime8 = glitchStartTime8 + Random.Range(-5, 5);
        glitchStartTime9 = glitchStartTime9 + Random.Range(-5, 5);
        glitchStartTime10 = glitchStartTime10 + Random.Range(-5, 5);
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("Update");
        currentTime = Time.time;


        if (currentTime >= glitchStartTime1 && currentTime < glitchStartTime1 + glitchDuration && !glitching && !deforming) {
            //Debug.Log("switching to glitch at: " + currentTime);
            this.objectRenderer.material = (Material)Resources.Load("Materials/" + materialName + "Glitch", typeof(Material));
            recentGlitchTime = currentTime + glitchDuration;
            glitching = true;
        }
        else if (currentTime >= glitchStartTime2 && currentTime < glitchStartTime2 + glitchDuration && !glitching && !deforming) {
            //Debug.Log("switching to glitch at: " + currentTime);
            this.objectRenderer.material = (Material)Resources.Load("Materials/" + materialName + "Glitch", typeof(Material));
            recentGlitchTime = currentTime + glitchDuration;
            glitching = true;
        }
        else if (currentTime >= glitchStartTime3 && currentTime < glitchStartTime3 + glitchDuration && !glitching && !deforming) {
            //Debug.Log("switching to glitch at: " + currentTime);
            this.objectRenderer.material = (Material)Resources.Load("Materials/" + materialName + "Glitch", typeof(Material));
            recentGlitchTime = currentTime + glitchDuration;
            glitching = true;
        }
        else if (currentTime >= glitchStartTime4 && currentTime < glitchStartTime4 + glitchDuration && !glitching && !deforming) {
            //Debug.Log("switching to glitch at: " + currentTime);
            this.objectRenderer.material = (Material)Resources.Load("Materials/" + materialName + "Glitch", typeof(Material));
            recentGlitchTime = currentTime + glitchDuration;
            glitching = true;
        }
        else if (currentTime >= glitchStartTime5 && currentTime < glitchStartTime5 + glitchDuration && !glitching && !deforming) {
            //Debug.Log("switching to glitch at: " + currentTime);
            this.objectRenderer.material = (Material)Resources.Load("Materials/" + materialName + "Glitch", typeof(Material));
            recentGlitchTime = currentTime + glitchDuration;
            glitching = true;
        }
        else if (currentTime >= glitchStartTime6 && currentTime < glitchStartTime6 + glitchDuration && !glitching && !deforming) {
            //Debug.Log("switching to glitch at: " + currentTime);
            this.objectRenderer.material = (Material)Resources.Load("Materials/" + materialName + "Glitch", typeof(Material));
            recentGlitchTime = currentTime + glitchDuration;
            glitching = true;
        }
        else if (currentTime >= glitchStartTime7 && currentTime < glitchStartTime7 + glitchDuration && !glitching && !deforming) {
            //Debug.Log("switching to glitch at: " + currentTime);
            this.objectRenderer.material = (Material)Resources.Load("Materials/" + materialName + "Glitch", typeof(Material));
            recentGlitchTime = currentTime + glitchDuration;
            glitching = true;
        }
        else if (currentTime >= glitchStartTime8 && currentTime < glitchStartTime8 + glitchDuration && !glitching && !deforming) {
            //Debug.Log("switching to glitch at: " + currentTime);
            this.objectRenderer.material = (Material)Resources.Load("Materials/" + materialName + "Glitch", typeof(Material));
            recentGlitchTime = currentTime + glitchDuration;
            glitching = true;
        }
        else if (currentTime >= glitchStartTime9 && currentTime < glitchStartTime9 + glitchDuration && !glitching && !deforming) {
            //Debug.Log("switching to glitch at: " + currentTime);
            this.objectRenderer.material = (Material)Resources.Load("Materials/" + materialName + "Glitch", typeof(Material));
            recentGlitchTime = currentTime + glitchDuration;
            glitching = true;
        }
        else if (currentTime >= glitchStartTime10 && currentTime < glitchStartTime10 + glitchDuration && !glitching && !deforming) {
            //Debug.Log("switching to glitch at: " + currentTime);
            this.objectRenderer.material = (Material)Resources.Load("Materials/" + materialName + "Glitch", typeof(Material));
            recentGlitchTime = currentTime + glitchDuration;
            glitching = true;
        }
        else if (currentTime >= deformTime && !deforming) {
            //Debug.Log("switching to deform at: " + currentTime + " deformTime is: " + deformTime);
            this.objectRenderer.material = (Material)Resources.Load("Materials/" + materialName + "Deform", typeof(Material));
            deforming = true;
            deformTime = currentTime;
        }
        else if (currentTime >= recentGlitchTime + glitchDuration && glitching && !deforming && currentTime >=1) { //mozne vymazat + glitchduration
            //Debug.Log("switchign to original at: " + currentTime);
            this.objectRenderer.material = (Material)Resources.Load("Materials/" + materialName, typeof(Material));
            glitching = false;
        }
        if (currentTime - deformTime >= deformDuration && deformTime >= 1) {
            //Debug.Log("Destroying: " + this.ToString());
            Destroy(this);
        }
    }
}
