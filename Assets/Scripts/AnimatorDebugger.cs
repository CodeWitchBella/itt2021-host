using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.InputSystem;

public class AnimatorDebugger : MonoBehaviour
{
    private float speed;
    Animator animator;
    void Start()
    {
        if (Application.isEditor) {
            animator = GetComponent<Animator>();

            // If you want to load speed up animations in editor just add
            // speed.txt with one line to Assets
            var file = Application.dataPath + "/speed.txt";
            if (System.IO.File.Exists(file)) {
                var lines = System.IO.File.ReadAllLines(file);
                if (float.TryParse(lines[0], out speed)) {
                    if (animator) animator.speed = speed;
                }
            }
        } else {
            Destroy(this);
        }
    }

    void Update()
    {
        if (!Application.isEditor) { Destroy(this); return; }
        if (Keyboard.current.spaceKey.wasReleasedThisFrame) {
            if (animator) {
                if (animator.speed > 0.01) animator.speed = 0;
                else animator.speed = speed;
            }
        }
    }
}
