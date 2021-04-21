using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
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
        return Input.GetKey(KeyCode.Space);
    }
}
