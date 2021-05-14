using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class SkipToMultiplayer : MonoBehaviour
{
    public bool KeyboardEnabledInBuild = false;
    void Update()
    {
        if (Keyboard.current.mKey.isPressed && (KeyboardEnabledInBuild || Application.isEditor)) {
            LoadMultiplayer();
        }
    }

    private bool loading = false;
    public void LoadMultiplayer()
    {
        if (loading) return;
        loading = true;

        StartCoroutine(LoadMultiplayerImpl());
    }

    IEnumerator LoadMultiplayerImpl()
    {
        yield return null;
        SceneManager.LoadScene("4 Multiplayer", LoadSceneMode.Single);
    }
}
