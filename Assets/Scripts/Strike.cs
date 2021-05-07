using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Strike : MonoBehaviour
{
    private ProgrammManager ProgrammManagerScript;
    
    private bool killed = false;

    // Start is called before the first frame update
    void Start()
    {
        ProgrammManagerScript = FindObjectOfType<ProgrammManager>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!killed && collision.gameObject.name == "Shell(Clone)")
        {
            ProgrammManagerScript.Strikes++;
            killed = true;
        }
    }
}
