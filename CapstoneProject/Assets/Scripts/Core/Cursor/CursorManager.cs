using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CursorManager
{
    public static void EnableCursor()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
    public static void DisableCursor()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
}
