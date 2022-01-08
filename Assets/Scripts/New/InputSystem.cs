using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputSystem : MonoBehaviour
{
    public static float Vertical()
    {
        return Input.GetAxis("Vertical");
    }

    public static float Horizontal()
    {
        return Input.GetAxis("Horizontal");
    }

    public static bool Jump()
    {
        return Input.GetKeyDown(KeyCode.Space);
    }
}
