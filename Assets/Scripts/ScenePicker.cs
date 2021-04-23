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

    bool HasVR()
    {
        List<XRDisplaySubsystemDescriptor> displays = new List<XRDisplaySubsystemDescriptor>();
        SubsystemManager.GetSubsystemDescriptors(displays);

        foreach (var display in displays)
        {
            if (display.id.Contains("MockHMD") || !VREnabled) continue;
            return true;
        }
        return false;
    }

    public IEnumerator InitXR()
    {
        bool vrLoaded = false;
        var sceneNameBase = SceneManager.GetActiveScene().name.Replace(" Loader", "");

        if (XRGeneralSettings.Instance.Manager.isInitializationComplete)
            XRGeneralSettings.Instance.Manager.DeinitializeLoader();
        while (XRGeneralSettings.Instance.Manager.isInitializationComplete) yield return null;

        if (HasVR())
        {
            Debug.Log("Press V to load into VR, or D to load into desktop mode");
            bool v = false;
            bool d = false;
            while (!d && !v)
            {
                v = Keyboard.current.vKey.wasReleasedThisFrame;
                d = Keyboard.current.dKey.wasReleasedThisFrame;
                yield return null;
            }

            Debug.Log("V: " + (v ? "Yes" : "No") + " D: " + (d ? "Yes" : "No"));
            if (d) Debug.Log("Desktop mode selected");
            else
            {
                Debug.Log("VR mode selected");
                SceneManager.LoadScene(sceneNameBase + " VR", LoadSceneMode.Single);
                vrLoaded = true;
            }
        }

        if (!vrLoaded)
            SceneManager.LoadScene(sceneNameBase + " PC", LoadSceneMode.Single);
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("InitXR");
    }

}
