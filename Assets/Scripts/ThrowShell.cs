using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ThrowShell : MonoBehaviour
{
    private ProgrammManager ProgrammManagerScript;
    private TrajectoryRenderer TrajectoryRendererScript;

    [SerializeField] private GameObject ShellPrefab;
    private GameObject ShellObject;
    private Rigidbody ShellRigidbody;
    [SerializeField] private Vector3 speed;

    private GameObject inputObject;
    private InputField input;
    private GameObject scrollbarObject;
    private Scrollbar scrollbar;
    private string ForceString;
    private float force = 0f;
    private Rigidbody CollisionRigidbody;

    public AudioClip ThrowSound;
    private AudioSource CatapultAudio;

    // Start is called before the first frame update
    void Start()
    {
        ProgrammManagerScript = FindObjectOfType<ProgrammManager>();
        TrajectoryRendererScript = FindObjectOfType<TrajectoryRenderer>();
        inputObject = GameObject.Find("InputField");
        input = inputObject.GetComponent<InputField>();

        scrollbarObject = GameObject.Find("Scrollbar");
        scrollbar = scrollbarObject.GetComponent<Scrollbar>();

        scrollbar.onValueChanged.AddListener((float val) => ScrollbarCallback(val));
    }

    // Update is called once per frame
    void Update()
    {
        ForceString = input.text;
        if (ForceString.Length > 0)
        {
            force = float.Parse(ForceString, System.Globalization.CultureInfo.InvariantCulture);
            
            ProgrammManagerScript.fireForce = force;

            speed = transform.forward * 2 + transform.up * force;

            TrajectoryRendererScript.ShowTrajectory(transform.position + new Vector3(0f, 0.25f, 0f), speed);
        }
    }

    void ScrollbarCallback(float value)
    {
        input.text = string.Format("{0:0.0}", value);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (0 < force)
        {
            ShellObject = Instantiate(
                ShellPrefab,
                transform.position + new Vector3(0, 0.25f, -0.05f),
                ShellPrefab.transform.rotation
            );

            ShellRigidbody = ShellObject.GetComponent<Rigidbody>();
            ShellRigidbody.AddForce(speed, ForceMode.Impulse);

            CollisionRigidbody = collision.rigidbody;
            CollisionRigidbody.AddForce(CollisionRigidbody.transform.up * (-1), ForceMode.Impulse);

            ProgrammManagerScript.Recharging = true;

            CatapultAudio = GetComponent<AudioSource>();
            CatapultAudio.PlayOneShot(ThrowSound, 1.0f);
        }
    }
}
