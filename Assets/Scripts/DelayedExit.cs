using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelayedExit : MonoBehaviour
{
    public float time = 15;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Run());
    }

    IEnumerator Run()
    {
        yield return new WaitForSeconds(time);
        Application.OpenURL("https://projects.iim.cz/itt2/2021/");
        Application.Quit(0);
    }
}
