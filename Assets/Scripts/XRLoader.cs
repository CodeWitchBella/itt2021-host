using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR;
using UnityEngine.XR.Management;
using UnityEngine.InputSystem;

public class XRLoader : MonoBehaviour
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

            List<XRDisplaySubsystemDescriptor> displays = new List<XRDisplaySubsystemDescriptor>();
            SubsystemManager.GetSubsystemDescriptors(displays);
            Debug.Log("Number of display providers found: " + displays.Count);


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
            if (!v) Debug.Log("Skipping into desktop mode");
            else
            {
                Debug.Log("VR mode selected");

                Debug.Log("Initializing XR");
                yield return XRGeneralSettings.Instance.Manager.InitializeLoader();
                while (!XRGeneralSettings.Instance.Manager.isInitializationComplete) yield return null;

                Debug.Log("Starting XR subsystems");
                XRGeneralSettings.Instance.Manager.StartSubsystems();

                foreach (var display in displays)
                {
                    if (display.id.Contains("MockHMD") || !VREnabled) continue;

                    Debug.Log("Creating display " + display.id);
                    XRDisplaySubsystem dispInst = display.Create();

                    if (dispInst != null)
                    {
                        Debug.Log("Starting display " + display.id);
                        dispInst.Start();
                        while (!dispInst.running) yield return null;
                        Debug.Log("Loading VR scene");
                        SceneManager.LoadScene(sceneNameBase + " VR", LoadSceneMode.Single);
                        vrLoaded = true;
                        break;
                    }
                    else Debug.Log("Display instance is null");
                }
            }
        }

        if (!vrLoaded)
        {
            SceneManager.LoadScene(sceneNameBase + " PC", LoadSceneMode.Single);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("InitXR");
        DontDestroyOnLoad(this);
    }

    void OnDestroy()
    {
        if (XRGeneralSettings.Instance.Manager.isInitializationComplete)
            XRGeneralSettings.Instance.Manager.DeinitializeLoader();
    }
}
