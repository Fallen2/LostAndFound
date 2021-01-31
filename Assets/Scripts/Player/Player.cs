using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Transform playerEyes;

    float detection_distance = 3.5f;

    void Update()
    {
        RaycastHit hit;

        if(Physics.Raycast(playerEyes.position, playerEyes.forward, out hit, detection_distance))
        {
            //hit objects
            //get type of interactible (Doors, Lamps, Frames and Paintings)
            if (hit.transform.gameObject.GetComponent<Interactible>())
            {
                Debug.Log("Interactible");
            }
            Debug.DrawRay(playerEyes.position, playerEyes.forward * hit.distance, Color.yellow);
        }
    }
}
