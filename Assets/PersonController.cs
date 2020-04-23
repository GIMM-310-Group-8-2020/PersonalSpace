using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleARCore;

public class PersonController : MonoBehaviour
{
    public Camera firstPersonCamera;
    private DetectedPlane detectedPlane;

    private void Update()
    {
        //the tracking state must be FrameTrackingState.Tracking in order to access the Frame
        if (Session.Status != SessionStatus.Tracking)
        {
            return;
        }

        // If there is no plane, then return
        if (detectedPlane == null)
        {
            return;
        }

        // Check for the plane being subsumed.
        // If the plane has been subsumed switch attachment to the subsuming plane.
        while (detectedPlane.SubsumedBy != null)
        {
            detectedPlane = detectedPlane.SubsumedBy;
        }

        // Make the scoreboard face the viewer.
        transform.LookAt(firstPersonCamera.transform);

        // Move the position to stay consistent with the plane.
        transform.position = new Vector3(detectedPlane.CenterPose.position.x, detectedPlane.CenterPose.position.y, detectedPlane.CenterPose.position.z);
    }
    public void SetSelectedPlane(DetectedPlane detectedPlane)
    {
        this.detectedPlane = detectedPlane;
        //CreateAnchor();
    }
}
