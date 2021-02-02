using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Transform playerEyes;
    public Animator playerAnimator;

    float detection_distance = 3.5f;
    bool eyes_covered = false;

    void Start()
    {
        //playerAnimator = GetComponentInParent<Animator>();
    }

    void Update()
    {
        CoverEyes();

        RaycastHit hit;

        if(Physics.Raycast(playerEyes.position, playerEyes.forward, out hit, detection_distance))
        {
            //hit objects
            //get type of interactible (Doors, Lamps, Frames and Paintings)
            if (hit.transform.gameObject.GetComponent<Interactible>() && Input.GetMouseButtonDown(0))
            {
                hit.transform.gameObject.GetComponent<Interactible>().OnTrigger();
            }
            Debug.DrawRay(playerEyes.position, playerEyes.forward * hit.distance, Color.yellow);
        }
    }

    public bool isVulnerable()
    {
        return !eyes_covered;
    }

    public void CoverEyes()
    {
        if (Input.GetMouseButton(1))
        {
            eyes_covered = true;
            playerAnimator.SetBool("Cover In", true);
            playerAnimator.SetFloat("direction", 1f);
        }
        else if (Input.GetMouseButtonUp(0))
        {
            eyes_covered = false;
            playerAnimator.SetBool("Cover In", false);
            playerAnimator.SetFloat("direction", -1f);
        }
    }

    public void CloseHands()
    {
        if (Input.GetMouseButton(0))
        {
            eyes_covered = true;

        }
        else if (Input.GetMouseButtonUp(0))
        {
            eyes_covered = false;
            
        }
    }
}
