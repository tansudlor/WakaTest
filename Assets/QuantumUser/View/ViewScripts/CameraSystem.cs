using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public interface ILookable
{
    void LookTo(Transform target);
}

public interface ILookTarget
{
    Transform targetTransform { get; set; }
}

public class CameraSystem
{
 
    private ILookTarget lookat;
    private ILookable looker;

    // Start is called before the first frame update
    public void SetCameraAndTarget(ILookable looker, ILookTarget lookAt)
    {
        this.lookat = lookAt;
        this.looker = looker;
    }
    // Update is called once per frame
    void Update()
    {
        if(lookat != null && looker != null)
        {
            looker.LookTo(lookat.targetTransform);
        }

    }
}





