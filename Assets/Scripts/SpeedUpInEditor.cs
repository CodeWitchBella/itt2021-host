using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

public class SpeedUpInEditor : MonoBehaviour
{
    void Start()
    {
        if (Application.isEditor) {
            // If you want to load speed up animations in editor just add
            // speed.txt with one line to Assets
            var file = Application.dataPath + "/speed.txt";
            if (System.IO.File.Exists(file)) {
                var lines = System.IO.File.ReadAllLines(file);
                float speed;
                if (float.TryParse(lines[0], out speed)) {
                    var animator = GetComponent<Animator>();
                    if (animator) animator.speed = speed;
                }
            }
        }
        Destroy(this);
    }

}
