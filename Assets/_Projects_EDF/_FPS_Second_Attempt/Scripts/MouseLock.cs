using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// This is to lock cursor while in the game scene
/// </summary>
public class MouseLock : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("1"))
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        if (Input.GetKeyDown("2"))
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        
    }
}
