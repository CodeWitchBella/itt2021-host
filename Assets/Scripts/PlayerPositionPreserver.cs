using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPositionPreserver : MonoBehaviour
{
    void Start()
    {
        transform.SetParent(null, true);
        DontDestroyOnLoad(this);
    }
}
