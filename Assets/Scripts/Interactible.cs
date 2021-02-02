using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactible : MonoBehaviour
{
    public virtual void OnTrigger()
    {
        Debug.Log("On trigger");
    }

    public virtual void OnUse()
    {

    }
}
