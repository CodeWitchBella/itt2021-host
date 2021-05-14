using UnityEngine;
using UnityEngine.Animations;
using Normal.Realtime;

public class DesktopPlayerManager : MonoBehaviour
{
    [SerializeField] private GameObject _camera = default;

    private Realtime _realtime;

    private void Awake()
    {
        // Get the Realtime component on this game object
        _realtime = GetComponent<Realtime>();

        // Notify us when Realtime successfully connects to the room
        _realtime.didConnectToRoom += DidConnectToRoom;
    }

    private void DidConnectToRoom(Realtime realtime)
    {
        // Instantiate the Player for this client once we've successfully connected to the room
        GameObject playerGameObject = Realtime.Instantiate(prefabName: "Desktop Player", // Prefab name
                                                                      ownedByClient: true,               // Make sure the RealtimeView on this prefab is owned by this client
                                                           preventOwnershipTakeover: true,               // Prevent other clients from calling RequestOwnership() on the root RealtimeView.
                                                                        useInstance: realtime);          // Use the instance of Realtime that fired the didConnectToRoom event.

        // Get a reference to the player
        var dataHolder = playerGameObject.GetComponent<MultiplayerDesktopPlayerDataHolder>();

        foreach (var r in playerGameObject.GetComponentsInChildren<SkinnedMeshRenderer>()) {
            if (r) r.enabled = false;
        }

        if (dataHolder.MouseHandler) dataHolder.MouseHandler.enabled = true;

        dataHolder.MouseHandler.transform.position = _camera.transform.position;
        dataHolder.MouseHandler.transform.rotation = _camera.transform.rotation;
        _camera.transform.parent = dataHolder.CameraTarget.transform;
        _camera.transform.localPosition = Vector3.zero;
        _camera.transform.localRotation = Quaternion.identity;

    }
}
