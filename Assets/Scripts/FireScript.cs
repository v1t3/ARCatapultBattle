using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FireScript : MonoBehaviour
{
    private ProgrammManager ProgrammManagerScript;

    private Button button;
    private Rigidbody BeamRigidBody;
    public float force;

    // Start is called before the first frame update
    void Start()
    {
        ProgrammManagerScript = FindObjectOfType<ProgrammManager>();

        button = GetComponent<Button>();
        button.onClick.AddListener(Fire);
    }

    void Fire()
    {
        if (0 < ProgrammManagerScript.fireForce)
        {
            GameObject Beam = GameObject.Find("Beam");

            if (Beam)
            {
                BeamRigidBody = Beam.GetComponent<Rigidbody>();

                if (!ProgrammManagerScript.Recharging && 0 < force)
                {
                    BeamRigidBody.AddForce(BeamRigidBody.transform.up * force, ForceMode.Impulse);
                    ProgrammManagerScript.Recharging = true;
                }
            }
        }
    }
}
