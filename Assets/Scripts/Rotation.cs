using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Rotation : MonoBehaviour
{
    private Button button;

    private ProgrammManager ProgrammManagerScript;

    // Start is called before the first frame update
    void Start()
    {
        ProgrammManagerScript = FindObjectOfType<ProgrammManager>();

        button = GetComponent<Button>();
        button.onClick.AddListener(RotationFunction);
    }

    // Update is called once per frame
    void RotationFunction()
    {
        GetComponent<Image>().color = ProgrammManagerScript.Rotation ? Color.red : Color.green;
        ProgrammManagerScript.Rotation = !ProgrammManagerScript.Rotation;
    }
}
