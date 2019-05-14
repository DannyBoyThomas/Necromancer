using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackObject : MonoBehaviour
{
    public Transform Target;
    public bool TrackPosition;
    public bool TrackRotation;

    // Update is called once per frame
    void Update()
    {
        if (TrackPosition)
            transform.position = Target.position;
        
        if (TrackRotation)
            transform.rotation = Target.rotation;
    }
}
