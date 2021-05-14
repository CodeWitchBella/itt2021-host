using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Preserve : MonoBehaviour
{
    void Start()
    {
        transform.SetParent(null, true);
        DontDestroyOnLoad(this);
        Destroy(this);
    }
}
