using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Quantum;

public class QuantimCameraFollow : QuantumEntityViewComponent<CustomViewContext>
{
    public Vector3 Offset;

    private bool localPlayer;

    private bool isFollowCameraPlayer = false;
    
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

        if (UnityEngine.Input.GetKeyDown(KeyCode.K))
        {
            isFollowCameraPlayer = !isFollowCameraPlayer;
            if (isFollowCameraPlayer)
            {
                ViewContext.WorldCam.gameObject.SetActive(false);
                ViewContext.FollowCam.gameObject.SetActive(true);
            }
            else
            {
                ViewContext.WorldCam.gameObject.SetActive(true);
                ViewContext.FollowCam.gameObject.SetActive(false);
            }
            
        }

        if (isFollowCameraPlayer)
        {
            ViewContext.FollowCam.transform.position = transform.position + Offset;
            ViewContext.FollowCam.transform.LookAt(transform);
        }
        


        
    }
}
