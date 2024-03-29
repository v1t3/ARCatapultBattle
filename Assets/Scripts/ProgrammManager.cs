﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class ProgrammManager : MonoBehaviour
{
    private ARRaycastManager ARRaycastManagerScript;
    List<ARRaycastHit> hits = new List<ARRaycastHit>();

    public GameObject ObjectToSpawn;
    [Header("Put ScrollView here")]
    public GameObject ScrollView;
    private GameObject SelectedObject;
    private GameObject MaketShell;
    [SerializeField]
    private GameObject EndText;

    [SerializeField]
    private Camera ARCamera;

    private Touch touch;

    private Quaternion YRotation;

    public bool ChooseObject = false;
    public bool Rotation;
    public bool Recharging;

    public int Strikes;

    public float fireForce;

    void Start()
    {
        ARRaycastManagerScript = FindObjectOfType<ARRaycastManager>();

        ScrollView.SetActive(false);
        EndText.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0)
        {
            touch = Input.GetTouch(0);
        }

        if (ChooseObject)
        {
            SetObject();
        }

        if (Strikes > 2)
        {
            EndText.SetActive(true);
        }

        MoveObjectAndRotation();

        if (MaketShell)
        {
            MaketShell.SetActive(!Recharging);
        }

        //Quit app on Back
        if (Input.GetKey("escape"))
        {
            Application.Quit();
        }
    }

    void SetObject()
    {
        List<ARRaycastHit> hits = new List<ARRaycastHit>();

        ARRaycastManagerScript.Raycast(touch.position, hits, TrackableType.Planes);

        // set object
        if (Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began)
        {
            Instantiate(ObjectToSpawn, hits[0].pose.position, ObjectToSpawn.transform.rotation);
            MaketShell = GameObject.Find("Shell");
            ChooseObject = false;
        }
    }

    void MoveObjectAndRotation()
    {

        //select object
        if (touch.phase == TouchPhase.Began)
        {
            Ray ray = ARCamera.ScreenPointToRay(touch.position);
            RaycastHit hitObject;

            if (Physics.Raycast(ray, out hitObject))
            {
                if (hitObject.collider.CompareTag("UnSelected"))
                {
                    hitObject.collider.gameObject.tag = "Selected";
                }
            }
        }

        SelectedObject = GameObject.FindWithTag("Selected");

        //Rotate object with 1 finger
        if (touch.phase == TouchPhase.Moved && Input.touchCount == 1)
        {
            if (Rotation)
            {
                YRotation = Quaternion.Euler(0f, -touch.deltaPosition.x * 0.1f, 0f);
                SelectedObject.transform.rotation = YRotation * SelectedObject.transform.rotation;
            }
            else //move object
            {
                ARRaycastManagerScript.Raycast(touch.position, hits, TrackableType.Planes);
                SelectedObject.transform.position = hits[0].pose.position;
            }
        }

        //Rotate object with 2 fingers
        if (Input.touchCount == 2)
        {
            Touch touch1 = Input.touches[0];
            Touch touch2 = Input.touches[1];

            if (touch1.phase == TouchPhase.Moved || touch2.phase == TouchPhase.Moved)
            {
                float distanceBetweenTouches = Vector2.Distance(touch1.position, touch2.position);
                float prevDistanceBetweenTouches = Vector2.Distance(
                    touch1.position - touch1.deltaPosition,
                    touch2.position - touch2.deltaPosition
                );
                float delta = distanceBetweenTouches - prevDistanceBetweenTouches;

                if (Mathf.Abs(delta) > 0)
                {
                    delta *= 0.1f;
                }
                else
                {
                    distanceBetweenTouches = 0;
                    delta = 0;
                }

                YRotation = Quaternion.Euler(0f, -touch1.deltaPosition.x * delta, 0f);
                SelectedObject.transform.rotation = YRotation * SelectedObject.transform.rotation;
            }
        }

        //Deselect object
        if (touch.phase == TouchPhase.Ended)
        {
            if (SelectedObject.CompareTag("Selected"))
            {
                SelectedObject.tag = "UnSelected";
            }
        }
    }
}
