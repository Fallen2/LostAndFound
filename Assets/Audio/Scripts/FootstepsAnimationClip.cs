using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootstepsAnimationClip : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
        player = GetComponentInChildren<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckTerrain();
        playerSpeed.SetValue(gameObject, playerMovement.currentSpeed);
        Debug.Log(playerMovement.currentSpeed);
    }

    private Player player;
    public PlayerMovement playerMovement;

    private enum CURRENT_TERRAIN { CARPET, WOOD};

    [SerializeField]
    private CURRENT_TERRAIN currentTerrain;

    [SerializeField]
    private AK.Wwise.Event footstepsEvent_walk;

    [SerializeField]
    private AK.Wwise.Event footstepsEvent_run;

    [SerializeField]
    private AK.Wwise.Switch[] groundSwitch;

    [SerializeField]
    private AK.Wwise.RTPC playerSpeed;



    private void CheckTerrain()
    {
        
        RaycastHit hit;


        if (Physics.Raycast(player.playerEyes.position, Vector3.down, out hit, 10f, (1 << LayerMask.NameToLayer("Carpet") | (1 << LayerMask.NameToLayer("Wood")))))
        {
            Debug.Log(hit.transform.name);
            if (hit.transform.gameObject.layer == LayerMask.NameToLayer("Carpet"))
            {
                currentTerrain = CURRENT_TERRAIN.CARPET;
                
            }
            else if (hit.transform.gameObject.layer == LayerMask.NameToLayer("Wood"))
            {
                
                currentTerrain = CURRENT_TERRAIN.WOOD;
            }
        }
    }

    private void PlayFootstep_Walk(int terrain)
    {
        groundSwitch[terrain].SetValue(this.gameObject);
        AkSoundEngine.PostEvent(footstepsEvent_walk.Id, this.gameObject);
    }

    private void PlayFootstep_Run(int terrain)
    {
        groundSwitch[terrain].SetValue(this.gameObject);
        AkSoundEngine.PostEvent(footstepsEvent_run.Id, this.gameObject);
    }

    public void SelectAndPlayFootstep_Walk()
    {
        switch (currentTerrain)
        {
            case CURRENT_TERRAIN.WOOD:
                PlayFootstep_Walk(0);
                break;

            case CURRENT_TERRAIN.CARPET:
                PlayFootstep_Walk(1);
                break;

            default:
                PlayFootstep_Walk(0);
                break;
        }
    }

    public void SelectAndPlayFootstep_Run()
    {
        switch (currentTerrain)
        {
            case CURRENT_TERRAIN.WOOD:
                PlayFootstep_Run(0);
                break;

            case CURRENT_TERRAIN.CARPET:
                PlayFootstep_Run(1);
                break;

            default:
                PlayFootstep_Run(0);
                break;
        }
    }
}
