using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System.Linq;

public class CreatureController : MonoBehaviour
{
    public List<GameObject> possibleNodes;
    public List<GameObject> actualPossibleNodes;
    public List<GameObject> allNodes;
    public GameObject actualNode;

    public float WanderSpeed;
    public float FollowSpeed;

    public bool playerSeen = false;

    public float startCheckingPlayerDistance;

    public bool playerSafe = false;

    public float NodeDetectionRadius;
    public float AfterFollowNodeDetectionRadius;

    public bool willTeleport = false;
    public float timeBeforeTeleportation;
    private float elapsedTimeBeforeTeleportation = 0;

    private NavMeshAgent agent;
    private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        //Component Getters
        player = GameObject.FindGameObjectWithTag("Player");
        agent = GetComponent<NavMeshAgent>();

        //AgentSetup
        agent.speed = WanderSpeed;


        //NodeSetup
        GameObject[] nodes = GameObject.FindGameObjectsWithTag("Node");
        foreach (GameObject node in nodes)
        {
            allNodes.Add(node);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            teleport();
        }

        if (willTeleport)
        {
            elapsedTimeBeforeTeleportation += Time.deltaTime;
            if(elapsedTimeBeforeTeleportation >= timeBeforeTeleportation)
            {
                playerSafe = false;
                elapsedTimeBeforeTeleportation = 0;
                willTeleport = false;
                teleport();
            }
        }

    }

    //Checks if the player can be seen
    public bool PlayerOnSight()
    {
        RaycastHit hit;
        Debug.DrawRay(transform.position, player.transform.position - transform.position);
        if (Physics.Raycast(transform.position, player.transform.position - transform.position, out hit))
        {
            if (hit.collider.gameObject.CompareTag("Player"))
            {
                return true;
            }
        }
        return false;
    }

    //Checks if the player is near enough to start checking if he can be seen
    public bool PlayerDistanceChecked()
    {
        return (Vector3.Distance(transform.position, player.transform.position) <= startCheckingPlayerDistance);
    }

    void teleport()
    {
        stopAgent();
        transform.position = allNodes[Random.Range(0, allNodes.Count)].transform.position;
        addPossibleNodes();
    }

    public void refreshPlayerPosition()
    {
        agent.destination = player.transform.position;
    }

    public void stopAgent()
    {
        GetComponent<NavMeshAgent>().isStopped = true;
        GetComponent<NavMeshAgent>().ResetPath();
    }


    public void startFollowingPlayer()
    {
        stopAgent();
        agent.speed = FollowSpeed;
    }

    public void startWandering()
    {
        stopAgent();
        agent.speed = WanderSpeed;
        newDestination();
    }

    void addPossibleNodes()
    {
        if (possibleNodes.Count != 0)
        {
            foreach (GameObject node in possibleNodes)
            {
                node.GetComponent<MeshRenderer>().material.color = Color.red;
            }
            actualNode.GetComponent<MeshRenderer>().material.color = Color.red;
        }
        possibleNodes.Clear();
        actualPossibleNodes.Clear();
        actualNode = null;
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, NodeDetectionRadius);
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.CompareTag("Node"))
            {
                possibleNodes.Add(hitCollider.gameObject);
                actualPossibleNodes.Add(hitCollider.gameObject);
                hitCollider.gameObject.GetComponent<MeshRenderer>().material.color = Color.green;
            }
        }
        GetComponent<Animator>().SetTrigger("startWandering");
        SelectNextNode();
    }

    public void SelectNextNode()
    {
        if (actualNode)
        {
            actualNode.GetComponent<MeshRenderer>().material.color = Color.green;
        }
        int tempIndex = Random.Range(0, actualPossibleNodes.Count);
        if (actualNode)
        {
            actualPossibleNodes.Add(actualNode);
        }
        actualNode = actualPossibleNodes[tempIndex];
        actualNode.GetComponent<MeshRenderer>().material.color = Color.blue;
        actualPossibleNodes.RemoveAt(tempIndex);
        newDestination();
    }

    void newDestination()
    {
        agent.destination = actualNode.transform.position;
    }

    public void AfterFollowDestination()
    {
        GetComponent<Animator>().SetTrigger("stopFollowing");
        Collider[] hitCollidersNear = Physics.OverlapSphere(transform.position, NodeDetectionRadius);
        Collider[] hitCollidersFar = Physics.OverlapSphere(transform.position, AfterFollowNodeDetectionRadius);

        List<Collider> hitCollidersNearNodes = new List<Collider>();
        List<Collider> hitCollidersFarNodes = new List<Collider>() ;

        foreach (var hitCollider in hitCollidersNear)
        {
            if (hitCollider.CompareTag("Node"))
            {
                hitCollidersNearNodes.Add(hitCollider);
            }
        }

        foreach (var hitCollider in hitCollidersFar)
        {
            if (hitCollider.CompareTag("Node"))
            {
                hitCollidersFarNodes.Add(hitCollider);
            }
        }

        IEnumerable<Collider> colliderIenumerable = hitCollidersFarNodes.Except<Collider>(hitCollidersNearNodes);
        Collider[] possibleColliders = colliderIenumerable.ToArray();

        actualNode = possibleColliders[Random.Range(0, possibleColliders.Length)].gameObject;
        
    }

    public GameObject GetPlayer()
    {
        return player;
    }

    private void OnDrawGizmosSelected()
    {
        // Wander Nodes detection sphere
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, NodeDetectionRadius);

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, AfterFollowNodeDetectionRadius);
    }
}
