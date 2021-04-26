using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlitchScript : MonoBehaviour {

    Material glitchMaterial;
    Material normalMaterial;
    // Start is called before the first frame update
    void Start()
    {
        //glitchMaterial = Resources.Load<Material>("TestMaterial");
        //MeshRenderer meshRenderer = GetComponent<MeshRenderer>();
        //Material normalMaterial = meshRenderer.material;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setMaterial(string s) {
        MeshRenderer meshRenderer = GetComponent<MeshRenderer>();
        switch (s) {
            case "glitch":
                meshRenderer.material = glitchMaterial;
                break;
            case "normal":
                meshRenderer.material = normalMaterial;
                break;
        }
    }
}
