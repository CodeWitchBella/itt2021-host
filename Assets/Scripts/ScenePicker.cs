using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR;
using UnityEngine.XR.Management;
using UnityEngine.InputSystem;

public class ScenePicker : MonoBehaviour
{
    public bool VREnabled = true;
    // we might want to do "2 Timer"
    public string TargetScene = "3 Experience";

    bool HasVR()
    {
        List<XRDisplaySubsystemDescriptor> displays = new List<XRDisplaySubsystemDescriptor>();
        SubsystemManager.GetSubsystemDescriptors(displays);

        foreach (var display in displays) {
            if (display.id.Contains("MockHMD") || !VREnabled) continue;
            return true;
        }
        return false;
    }

    public IEnumerator InitXR()
    {
        Debug.Log("Press V to load into VR, or D to load into desktop mode");
        bool v = false;
        bool d = false;
        while (!d && !v) {
            v = Keyboard.current.vKey.isPressed;
            d = Keyboard.current.dKey.isPressed;
            yield return null;
        }
        Debug.Log("V: " + (v ? "Yes" : "No") + " D: " + (d ? "Yes" : "No"));
        VREnabled = v;

        DontDestroyOnLoad(gameObject);
        SceneManager.LoadScene(TargetScene, LoadSceneMode.Single);
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("InitXR");
    }

}
