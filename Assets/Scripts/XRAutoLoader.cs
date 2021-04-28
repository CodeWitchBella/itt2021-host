using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR;
using UnityEngine.XR.Management;
using UnityEngine.InputSystem;

public class XRAutoLoader : MonoBehaviour
{


    private GameObject FindRootObject(string name)
    {
        var rgos = UnityEngine.SceneManagement.SceneManager.GetActiveScene().GetRootGameObjects();
        foreach (var o in rgos) {
            if (o.name == name) return o;
        }
        return null;
    }


    public IEnumerator ConditionallyInitXR()
    {
        ScenePicker picker = FindObjectOfType<ScenePicker>();
        bool loadVR = picker == null ? false : picker.VREnabled;
        if (picker != null) Destroy(picker.gameObject);
        else if (Application.isEditor) {
            // If you want to load VR mode in editor by default create vr.txt in
            // assets folder containing true.
            // this code only runs when in unity editor AND not loading via picker scene
            var file = Application.dataPath + "/vr.txt";
            if (System.IO.File.Exists(file)) {
                var lines = System.IO.File.ReadAllLines(file);
                if (lines[0] == "true") loadVR = true;
            }
        }

        var camera = FindRootObject(loadVR ? "XRRig" : "Camera");
        Debug.Log(camera);
        camera.SetActive(true);

        if (loadVR) {
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
        StartCoroutine("ConditionallyInitXR");
    }

    void OnDestroy()
    {
        if (XRGeneralSettings.Instance.Manager.isInitializationComplete)
            XRGeneralSettings.Instance.Manager.DeinitializeLoader();
    }
}
