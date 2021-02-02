using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : Interactible
{
    public Animator animator;

    public override void OnTrigger()
    {
        Debug.Log("Door Opened");
        animator.SetBool("Is Opened", true);
    }
}
