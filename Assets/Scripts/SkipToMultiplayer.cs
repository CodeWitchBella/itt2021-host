using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class SkipToMultiplayer : MonoBehaviour
{
    void Update()
    {
        if (Keyboard.current.mKey.isPressed) {
            SceneManager.LoadScene("4 Multiplayer", LoadSceneMode.Single);
        }
    }
}
