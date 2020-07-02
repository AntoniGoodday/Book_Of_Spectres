using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class EscapeToQuit : MonoBehaviour
{
    private void Start()
    {
        Application.targetFrameRate = 60;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Escape"))
        {
            Application.Quit();
        }
    }
}
