using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class WaitForStart : MonoBehaviour
{
    TextMeshPro textMesh;
    public string TargetScene = "3 Experience";
    public string Key = "Experience";
    string origText;
    public int timeRemaining;

    void Start()
    {
        textMesh = GetComponent<TextMeshPro>();

        StartCoroutine(GetTime());
        if (textMesh) {
            origText = textMesh.text;
        }
    }

    static bool ParseTime(string text, string key, out int outTime)
    {
        foreach (var line in text.Split('\n')) {
            if (line.StartsWith(key + ": ")) {
                if (int.TryParse(line.Replace(key + ":", "").Trim(), out outTime)) {
                    return true;
                }
            }
        }
        Debug.Log("parse fail " + key + "\n" + text);
        outTime = -1;
        return false;
    }

    IEnumerator GetTime()
    {
        timeRemaining = -1;
        try {
            while (true) {
                UnityWebRequest www = UnityWebRequest.Get("https://projects.iim.cz/itt2/2021/red/timer.php");
                www.SetRequestHeader("Authorization", "Basic aXR0MjppdHQyLXByb2plY3Q=");
                yield return www.SendWebRequest();
                if (www.result != UnityWebRequest.Result.Success && timeRemaining < 0) {
                    Debug.Log(www.error);
                    Debug.Log("Failed to check for experience start time. Starting anyway but the experience may be incomplete.");
                    textMesh?.SetText(origText + "Failed to check for experience start time. Starting anyway but the experience may be incomplete.");
                    yield return new WaitForSeconds(5);
                    break;
                } else {
                    int time;
                    if (ParseTime(www.downloadHandler.text, Key, out time)) timeRemaining = time;
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
        if (!textMesh) return;
        if (timeRemaining < 60) {
            textMesh.SetText(origText + "Time remaining: " + timeRemaining + " second" + (timeRemaining > 1 ? "s" : ""));
            return;
        }
        if (timeRemaining < 60) {
            timeRemaining /= 60;
            textMesh.SetText(origText + "Time remaining: " + timeRemaining + " minute" + (timeRemaining > 1 ? "s" : ""));
            return;
        }
        timeRemaining /= 60;
        textMesh.SetText(origText + "Time remaining: " + timeRemaining + " hour" + (timeRemaining > 1 ? "s" : ""));
    }
}
