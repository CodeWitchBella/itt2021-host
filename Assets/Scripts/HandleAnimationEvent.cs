using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class HandleAnimationEvent : MonoBehaviour
{
    public UnityAction OnDone;
    public string eventName = "Done";

    public ActionGroup[] ActionGroups;


    [System.Serializable]
    public class ActionGroup
    {
        public float DelaySeconds = 0;
        public bool LoadMultiplayer = false;
        public bool DestroySelf = true;
        public bool HideSelf = false;
        public GameObject[] activate;
    }


    private HandleAnimationEvent original = null;

    void Start()
    {
        OnDone += Handler;
    }

    void Destroy()
    {
        OnDone -= Handler;
    }


    private bool called;
    public bool wasCalled { get { return called; } }
    private void Handler()
    {
        if (called) return;
        called = true;

        foreach (var actionGroup in ActionGroups) {
            if (actionGroup.DelaySeconds > 0) {
                // make temporary object to handle delay so that we can just disable
                // self and not have to worry that it wont trigger
                var obj = new GameObject("Temp Animation Event Delay");
                var c = obj.AddComponent<HandleAnimationEvent>();
                c.eventName = "Temp";
                c.original = this;
                c.called = true;
                c.StartCoroutine(c.HandlerEnumerator(actionGroup));
            } else {
                PerformActions(actionGroup);
            }
        }
    }

    IEnumerator HandlerEnumerator(ActionGroup actionGroup)
    {
        yield return new WaitForSeconds(actionGroup.DelaySeconds / AnimatorDebugger.GetSpeed());
        PerformActions(actionGroup);
    }

    void PerformActions(ActionGroup actionGroup)
    {
        foreach (var o in actionGroup.activate) {
            o.SetActive(true);
        }
        var target = original == null ? this : original;
        if (actionGroup.DestroySelf) {
            Destroy(target.gameObject);
        }
        if (actionGroup.HideSelf) {
            foreach (var r in target.GetComponentsInChildren<Renderer>()) {
                r.enabled = false;
            }
        }
        if (actionGroup.LoadMultiplayer) {
            var mult = FindObjectOfType<SkipToMultiplayer>();
            if (mult) mult.LoadMultiplayer();
            else SceneManager.LoadScene("4 Multiplayer", LoadSceneMode.Single);
        }
        if (original != null) {
            Destroy(gameObject);
        }
    }
}
