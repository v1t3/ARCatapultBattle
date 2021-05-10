using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ThrowShell : MonoBehaviour
{
    private ProgrammManager ProgrammManagerScript;
    private TrajectoryRenderer TrajectoryRendererScript;

    [SerializeField]
    private GameObject ShellPrefab;
    private GameObject ShellObject;
    private Rigidbody ShellRigidbody;
    private Vector3 speed;

    private Scrollbar scrollbar;
    private Text speedInfo;

    private Rigidbody CollisionRigidbody;

    public AudioClip ThrowSound;
    private AudioSource CatapultAudio;

    private float force = 0f;

    // Start is called before the first frame update
    void Start()
    {
        ProgrammManagerScript = FindObjectOfType<ProgrammManager>();
        TrajectoryRendererScript = FindObjectOfType<TrajectoryRenderer>();

        GameObject SpeedInfoObject = GameObject.Find("SpeedInfo");
        speedInfo = SpeedInfoObject.GetComponent<Text>();

        GameObject scrollbarObject = GameObject.Find("Scrollbar");
        scrollbar = scrollbarObject.GetComponent<Scrollbar>();

        scrollbar.onValueChanged.AddListener((float val) => ScrollbarCallback(val));
    }

    void Update()
    {
        if (0 < force)
        {
            ProgrammManagerScript.fireForce = force;

            speed = transform.forward + transform.up * force;

            TrajectoryRendererScript.ShowTrajectory(transform.position + new Vector3(0f, 0.25f, 0f), speed);
        }
    }

    void ScrollbarCallback(float value)
    {
        if (0 < value)
        {
            force = value * 5;
        }

        speedInfo.text = string.Format("{0:0.0}", force);
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
