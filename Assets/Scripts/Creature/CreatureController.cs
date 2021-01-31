using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CreatureController : MonoBehaviour
{
    public List<GameObject> possibleNodes;
    public List<GameObject> actualPossibleNodes;
    public List<GameObject> allNodes;
    public GameObject actualNode;

    public float WanderSpeed;
    public float FollowSpeed;

    public float startCheckingPlayerDistance;

    public float NodeDetectionRadius;

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
    }

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

    public bool PlayerDistanceChecked()
    {
        return (Vector3.Distance(transform.position, player.transform.position) <= startCheckingPlayerDistance);
    }

    void teleport()
    {
        stopAgent();
        //transform.position = allNodes[Random.Range(0, allNodes.Count)].transform.position;
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

    private void OnDrawGizmosSelected()
    {
     Gizmos.color = Color.red;
     //Use the same vars you use to draw your Overlap SPhere to draw your Wire Sphere.
     Gizmos.DrawWireSphere(transform.position, NodeDetectionRadius);
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
}
