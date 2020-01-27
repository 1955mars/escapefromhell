using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatePrompt : MonoBehaviour
{
    public GameObject RotatePromptUI;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    
    }

    public void RotatePromptDisplay()
    {
        RotatePromptUI.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void CloseRotatePrompt()
    {
        RotatePromptUI.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}
