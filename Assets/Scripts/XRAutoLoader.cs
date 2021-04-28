using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR;
using UnityEngine.XR.Management;
using UnityEngine.InputSystem;

public class XRAutoLoader : MonoBehaviour
{
    public IEnumerator InitXR()
    {
        // fix multiple loadings from editor
        if (XRGeneralSettings.Instance.Manager.isInitializationComplete)
            XRGeneralSettings.Instance.Manager.DeinitializeLoader();
        while (XRGeneralSettings.Instance.Manager.isInitializationComplete) yield return null;

        List<XRDisplaySubsystemDescriptor> displays = new List<XRDisplaySubsystemDescriptor>();
        SubsystemManager.GetSubsystemDescriptors(displays);
        Debug.Log("Number of display providers found: " + displays.Count);

        Debug.Log("AutoInitializing XR");
        yield return XRGeneralSettings.Instance.Manager.InitializeLoader();
        while (!XRGeneralSettings.Instance.Manager.isInitializationComplete) yield return null;

        Debug.Log("Starting XR subsystems");
        XRGeneralSettings.Instance.Manager.StartSubsystems();

        foreach (var display in displays) {
            if (display.id.Contains("MockHMD")) continue;

            Debug.Log("Creating display " + display.id);
            XRDisplaySubsystem dispInst = display.Create();

            if (dispInst != null) {
                Debug.Log("Starting display " + display.id);
                dispInst.Start();
                break;
            }
        }

    }

    void Start()
    {
        // not the first autoloader to be loaded
        foreach (var o in FindObjectsOfType<XRAutoLoader>()) {
            if (o != this) {
                Debug.Log("Another");
                Destroy(this, 0.1f);
                return;
            }
        }

        // first autoloader
        DontDestroyOnLoad(this);
        StartCoroutine("InitXR");
    }

    void OnDestroy()
    {
        if (XRGeneralSettings.Instance.Manager.isInitializationComplete)
            XRGeneralSettings.Instance.Manager.DeinitializeLoader();
    }
}
