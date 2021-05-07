using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR;
using UnityEngine.XR.Management;
using UnityEngine.InputSystem;

// This component should be present once in every scene which should be dual
// VR/Desktop. Which is basically every scene except "1 Loader"
public class XRAutoLoader : MonoBehaviour
{
    // used to comunicate with future XRAutoLoader what this one decided to load
    protected bool VREnabled = false;

    // Used to determine whether to tear down VR on scene unload
    private bool First = false;

    public GameObject[] ActivateInVR;
    public GameObject[] ActivateForDesktop;


    // Called in first scene with XRAutoLoader to determine whether to load VR
    bool ShouldLoadVR()
    {
        ScenePicker picker = FindObjectOfType<ScenePicker>();
        if (picker != null) {
            Destroy(picker.gameObject);
            return picker.VREnabled;
        }
        if (Application.isEditor) {
            // If you want to load VR mode in editor by default create vr.txt in
            // assets folder containing true.
            // this code only runs when in unity editor AND not loading via picker scene
            var file = Application.dataPath + "/vr.txt";
            if (System.IO.File.Exists(file)) {
                var lines = System.IO.File.ReadAllLines(file);
                if (lines[0] == "true") return true;
            }
        }
        return false;
    }

    // Called in first scene with XRAutoLoader in XR mode
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

    // Called on every load
    void StartScene()
    {
        var activatable = VREnabled ? ActivateInVR : ActivateForDesktop;
        foreach (var o in activatable) {
            o?.SetActive(true);
        }
    }

    // Wires together everything described above
    void Start()
    {
        // not the first autoloader to be loaded
        foreach (var o in FindObjectsOfType<XRAutoLoader>()) {
            if (o != this) {
                Debug.Log("Another");
                VREnabled = o.VREnabled;
                StartScene();
                Destroy(this, 0.1f);
                return;
            }
        }

        // first autoloader
        DontDestroyOnLoad(this);
        VREnabled = ShouldLoadVR();
        StartScene();
        First = true;
        if (VREnabled) {
            StartCoroutine("InitXR");
        }
    }

    void OnDestroy()
    {
        if (XRGeneralSettings.Instance.Manager.isInitializationComplete && First)
            XRGeneralSettings.Instance.Manager.DeinitializeLoader();
    }
}
