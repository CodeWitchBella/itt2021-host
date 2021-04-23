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

    public IEnumerator InitXR()
    {
        List<XRDisplaySubsystemDescriptor> displays = new List<XRDisplaySubsystemDescriptor>();
        SubsystemManager.GetSubsystemDescriptors(displays);
        Debug.Log("Number of display providers found: " + displays.Count);
        var sceneNameBase = SceneManager.GetActiveScene().name.Replace(" Loader", "");

        bool vrLoaded = false;
        foreach (var display in displays)
        {
            if (display.id.Contains("MockHMD") || !VREnabled) continue;

            Debug.Log("Press V to load into VR, or D to load into desktop mode");
            bool v = false;
            bool d = false;
            while (!d && !v)
            {
                v = Keyboard.current.vKey.wasReleasedThisFrame;
                d = Keyboard.current.dKey.wasReleasedThisFrame;
                yield return null;
            }

            if (d)
            {
                Debug.Log("Skipping into desktop mode");
                break;
            }

            Debug.Log("Initializing XR");
            yield return XRGeneralSettings.Instance.Manager.InitializeLoader();

            Debug.Log("Creating display " + display.id);
            XRDisplaySubsystem dispInst = display.Create();

            if (dispInst != null)
            {
                Debug.Log("Starting display " + display.id);
                dispInst.Start();
                SceneManager.LoadScene(sceneNameBase + " VR", LoadSceneMode.Single);
                vrLoaded = true;
                break;
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
    }
}
