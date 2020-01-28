using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GGPrompt : MonoBehaviour
{
    public GameObject GGPromptUI;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void GGPromptDisplay()
    {
        GGPromptUI.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void CloseGGPrompt()
    {
        GGPromptUI.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}
