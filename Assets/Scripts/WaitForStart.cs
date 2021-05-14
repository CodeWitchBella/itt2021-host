using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class WaitForStart : MonoBehaviour
{
    TextMeshPro tmp;
    public string TargetScene = "3 Experience";
    string origText;

    void Start()
    {
        tmp = GetComponent<TextMeshPro>();
        if (!tmp) SceneManager.LoadScene(TargetScene, LoadSceneMode.Single);
        else {
            StartCoroutine(GetTime());
            origText = tmp.text;
        }
    }

    IEnumerator GetTime()
    {
        int timeRemaining = -1;
        try {
            while (true) {
                UnityWebRequest www = UnityWebRequest.Get("https://projects.iim.cz/itt2/2021/red/timer.php");
                www.SetRequestHeader("Authorization", "Basic aXR0MjppdHQyLXByb2plY3Q=");
                yield return www.SendWebRequest();
                if (www.result != UnityWebRequest.Result.Success && timeRemaining < 0) {
                    Debug.Log(www.error);
                    tmp.SetText(origText + "Failed to check for experience start time. Starting anyway but the experience may be incomplete.");
                    yield return new WaitForSeconds(5);
                    break;
                } else {
                    Debug.Log(www.downloadHandler.text);
                    int time;
                    if (int.TryParse(www.downloadHandler.text, out time)) timeRemaining = time;
                    if (timeRemaining <= 0) break;
                    for (int i = 0; i < 5; i++) {
                        DisplayTime(timeRemaining);
                        yield return new WaitForSeconds(1);
                        timeRemaining--;
                        if (timeRemaining <= 0) break;
                    }
                    if (timeRemaining <= 0) break;
                }
            }
        }
        finally {
            SceneManager.LoadScene(TargetScene, LoadSceneMode.Single);
        }
    }

    void DisplayTime(int timeRemaining)
    {
        if (timeRemaining < 60) {
            tmp.SetText(origText + "Time remaining: " + timeRemaining + " second" + (timeRemaining > 1 ? "s" : ""));
            return;
        }
        if (timeRemaining < 60) {
            timeRemaining /= 60;
            tmp.SetText(origText + "Time remaining: " + timeRemaining + " minute" + (timeRemaining > 1 ? "s" : ""));
            return;
        }
        timeRemaining /= 60;
        tmp.SetText(origText + "Time remaining: " + timeRemaining + " hour" + (timeRemaining > 1 ? "s" : ""));
    }
}
