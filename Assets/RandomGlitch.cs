using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomGlitch : MonoBehaviour
{

    public double startTime;
    private bool deforming = false;
    private bool glitching = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Space)) {
            foreach (GameObject g in GameObject.FindGameObjectsWithTag("Glitchable")) {
                //if (deforming) break;
                g.GetComponent<MeshRenderer>().material = (Material)Resources.Load("DeformMaterial", typeof(Material));
            }
            deforming = true;
            glitching = false;
        }
        else if (Input.GetKey(KeyCode.F)) {
            foreach (GameObject g in GameObject.FindGameObjectsWithTag("Glitchable")) {
                if (glitching) break;
                g.GetComponent<MeshRenderer>().material = (Material)Resources.Load("DissolveMaterial", typeof(Material));
            }
            glitching = true;
            deforming = false;
        }
        else {
            foreach (GameObject g in GameObject.FindGameObjectsWithTag("Glitchable")) {
                g.GetComponent<MeshRenderer>().material = (Material)Resources.Load("NormalMaterial", typeof(Material));
            }
        }
    }
    /*private bool isObjectVisible(GameObject obj) {
    Plane[] planes = GeometryUtility.CalculateFrustumPlanes(currCam);
      if (GeometryUtility.TestPlanesAABB(planes, obj.collider.bounds))
          return true;
      else
          return false;
    }*/
}
