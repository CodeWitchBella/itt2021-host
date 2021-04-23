using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Normal.Realtime;


public class Player : MonoBehaviour
{
    private Realtime _realtime;

    // Start is called before the first frame update
    void Start()
    {
        _realtime = FindObjectOfType<Realtime>();
        _realtime.didConnectToRoom += didConnectToRoom;
    }

    private void didConnectToRoom(Realtime realtime)
    {
        var player = Realtime.Instantiate("Player",
            position: Vector3.zero,
            rotation: Quaternion.identity,
            ownedByClient: true,
            preventOwnershipTakeover: false,
            destroyWhenOwnerOrLastClientLeaves: true,
            useInstance: realtime
        );
    }
}
