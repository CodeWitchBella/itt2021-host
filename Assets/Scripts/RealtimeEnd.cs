using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Normal.Realtime;
using System;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class RealtimeEnd : RealtimeComponent<EndModel>
{
    public string TargetScene = "5 Credits";


    private IEnumerable HandleDisconnect()
    {
        var fadeout = GameObject.Find("Fadeout")?.GetComponentInChildren<FadeoutMultiplayer>();
        if (fadeout) {
            fadeout.TriggerEnd(timeToFade: 1);
            yield return new WaitForSeconds(1);
        }
        SceneManager.LoadScene(TargetScene, LoadSceneMode.Single);
        Destroy(this);
    }

    [ReadOnly, SerializeField] private EndModel _model = null;
    [ReadOnly, SerializeField] bool wasConnected = false;
    [ReadOnly, SerializeField] float startTime = -1;

    [ReadOnly, SerializeField] private double modelEndTime = -1;

    void Start()
    {
        startTime = Time.time;
    }

    void Update()
    {
        if (Keyboard.current.cKey.isPressed && Keyboard.current.altKey.isPressed && Keyboard.current.shiftKey.isPressed && Keyboard.current.ctrlKey.isPressed) {
            if (_model != null) {
                var timeDiff = _model.endTime - realtime.room.time;
                if (timeDiff > 60) {
                    // speedup end
                    Debug.Log("Speeding up end");
                    _model.endTime = realtime.room.time + 60;
                }
            }
        }

        if (!wasConnected) {
            if (realtime.connected) {
                wasConnected = true;
            } else if (Time.time - startTime > 20f) {
                // can't connect for 20 seconds
                HandleDisconnect();
            }
            return;
        }

        if (!realtime.connected) {
            HandleDisconnect();
            return;
        }

        if (_model == null) return;

        if (_model.endTime < 0) {
            // 20 minutes from now
            _model.endTime = realtime.room.time + 60 * 20;
        } else {
            var timeDiff = _model.endTime - realtime.room.time;
            if (timeDiff < 90) {
                var fadeout = GameObject.Find("Fadeout")?.GetComponentInChildren<FadeoutMultiplayer>();
                if (!fadeout.wasTriggered) {
                    fadeout?.TriggerEnd(timeToFade: (float)timeDiff);
                }
            }
            if (timeDiff < 0) {
                SceneManager.LoadScene(TargetScene, LoadSceneMode.Single);
            }
        }
    }

    protected override void OnRealtimeModelReplaced(EndModel previousModel, EndModel currentModel)
    {
        if (previousModel != null) {
            previousModel.endTimeDidChange -= HandleEndTimeChange;
        }
        if (currentModel != null) {
            currentModel.endTimeDidChange += HandleEndTimeChange;
        }
        _model = currentModel;
    }

    private void HandleEndTimeChange(EndModel model, double value)
    {
        modelEndTime = value;
    }
}
