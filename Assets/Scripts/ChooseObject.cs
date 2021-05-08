using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChooseObject : MonoBehaviour
{
    private ProgrammManager ProgrammManagerScript;

    private Button button;

    public GameObject ChoosedObject;

    private int objectCount;
    [Header("Максимальное число элементов данного типа")]
    public int objectMax;

    void Start()
    {
        ProgrammManagerScript = FindObjectOfType<ProgrammManager>();

        button = GetComponent<Button>();
        button.onClick.AddListener(ChooseObjectFunction);
    }

    void ChooseObjectFunction()
    {
        if (0 == objectMax || (0 < objectMax && objectCount < objectMax))
        {
            ProgrammManagerScript.ObjectToSpawn = ChoosedObject;
            ProgrammManagerScript.ChooseObject = true;
            ProgrammManagerScript.ScrollView.SetActive(false);
            objectCount++;
        }
    }
}
