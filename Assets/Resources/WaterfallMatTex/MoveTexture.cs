using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTexture : MonoBehaviour
{
    public float slideSpeed = 1.0f;
    Renderer rend;

    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<Renderer> ();
    }

    // Update is called once per frame
    void Update()
    {
        float slideAmount = Time.time * slideSpeed;
        rend.material.SetTextureOffset("_BaseMap", new Vector2(0, slideAmount));
    }
}
