using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AddObject : MonoBehaviour
{
    private Button button;
    private ProgrammManager ProgrammManagerScript;

    private bool showScrollView = false;
    
    // Start is called before the first frame update
    void Start()
    {
        ProgrammManagerScript = FindObjectOfType<ProgrammManager>();

        button = GetComponent<Button>();
        button.onClick.AddListener(() => { showScrollView = !showScrollView; });
    }

    // Update is called once per frame
    void Update()
    {
        ProgrammManagerScript.ScrollView.SetActive(showScrollView);
    }
}
