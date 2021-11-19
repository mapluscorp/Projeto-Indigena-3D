using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class PlayerAnimationManager : MonoBehaviour
{
    public Animator anim;
    public float AnimSpeed { get; set; }

    public void Jump()
    {
        anim.SetTrigger("Jump");
    }

    public void SetPlayerSpeed(float value)
    {
        AnimSpeed = value;
        anim.SetFloat("PlayerSpeed", value);
    }
}
