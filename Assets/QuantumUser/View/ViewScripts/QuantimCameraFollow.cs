using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Quantum;

public class QuantimCameraFollow : QuantumEntityViewComponent<CustomViewContext>
{
    public Vector3 Offset;

    private bool localPlayer;

    public override void OnActivate(Frame frame)
    {
        var link = frame.Get<PlayerLink>(EntityRef);
        localPlayer = Game.PlayerIsLocal(link.Player);
    }


    public override void OnUpdateView()
    {
        if (!localPlayer)
        {
            return;
        }
        ViewContext.PlayerCamera.transform.position = transform.position + Offset;
        ViewContext.PlayerCamera.transform.LookAt(transform);
    }
}
