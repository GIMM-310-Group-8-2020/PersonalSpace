using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GoogleARCore;

public class GameManager : MonoBehaviour
{
    public Camera firstPersonCamera;
    public ScoreboardController scoreboard;
    public SnakeController snakeController;
    public PersonController personController;
    public GameObject center;
    public GameObject person;
    public Text mainText;

    private bool planeSelected = false;

    void Start()
    {
        QuitOnConnectionErrors();
        center.SetActive(false);
        person.SetActive(false);
    }

    void Update()
    {
        // The session status must be Tracking in order to access the Frame.
        if (Session.Status != SessionStatus.Tracking)
        {
            int lostTrackingSleepTimeout = 15;
            Screen.sleepTimeout = lostTrackingSleepTimeout;
            return;
        }
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
        /* if (Input.touchCount == 1)
         {
             Debug.Log("We touchin");
         }*/
        ProcessTouches();
        //scoreboard.SetScore(snakeController.GetLength());
        
        if (planeSelected)
        {
            center.transform.position = new Vector3(firstPersonCamera.transform.position.x, person.transform.position.y, firstPersonCamera.transform.position.z);
            CalculateDistance();
        }
        
    }

    void QuitOnConnectionErrors()
    {
        if (Session.Status == SessionStatus.ErrorPermissionNotGranted)
        {
            StartCoroutine(CodelabUtils.ToastAndExit("Camera permission is needed to run this application.", 5));
        }
        else if (Session.Status.IsError())
        {
            // This covers a variety of errors.  See reference for details
            // https://developers.google.com/ar/reference/unity/namespace/GoogleARCore
            StartCoroutine(CodelabUtils.ToastAndExit("ARCore encountered a problem connecting. Please restart the app.", 5));
        }
        
    }

    void ProcessTouches()
    {
        Touch touch;
        if (Input.touchCount != 1 ||
            (touch = Input.GetTouch(0)).phase != TouchPhase.Began)
        {
            return;
        }

        TrackableHit hit;
        TrackableHitFlags raycastFilter = TrackableHitFlags.PlaneWithinBounds | TrackableHitFlags.PlaneWithinPolygon;
        Debug.Log(touch.position.x + " + " + touch.position.y);

        if (Frame.Raycast(touch.position.x, touch.position.y, raycastFilter, out hit))
        {
            //Debug.Log("are we here??????");
            SetSelectedPlane(hit.Trackable as DetectedPlane);
        }
    }

    void SetSelectedPlane(DetectedPlane selectedPlane)
    {
        Debug.Log("Selected plane centered at " + selectedPlane.CenterPose.position);
        //scoreboard.SetSelectedPlane(selectedPlane);
        //snakeController.SetPlane(selectedPlane);
        GetComponent<FoodController>().SetSelectedPlane(selectedPlane);
        personController.SetSelectedPlane(selectedPlane);
        center.SetActive(true);
        person.SetActive(true);
        planeSelected = true;
    }

    void CalculateDistance()
    {
        float dist = Vector3.Distance(person.transform.position, center.transform.position);
        if (dist < 0.5)
        {
            mainText.text = "Close";
        }
        else if (dist > 0.5 && dist < 1.0)
        {
            mainText.text = "Near";
        }
        else if (dist > 1.0 && dist < 1.5)
        {
            mainText.text = "Far";
        }
        else
        {
            mainText.text = "Very Far";
        }
    }
}
