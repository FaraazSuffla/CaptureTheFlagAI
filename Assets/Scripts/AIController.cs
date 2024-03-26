using UnityEngine;
using UnityEngine.AI;

public enum AIState
{
    FetchingFlag,
    ReturningFlag,
    ChasingPlayer
}

public class AIController : MonoBehaviour
{
    public GameObject player;
    public GameObject aiBase;
    public string playerFlagTag = "Flag Blue";
    public string aiFlagTag = "Flag Red";

    public AIState currentState = AIState.FetchingFlag;

    private NavMeshAgent agent;
    private GameObject currentFlag;
    private Flag flagScript;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        Invoke("FindFlag", 0.1f);
    }

    void FindFlag()
    {
        currentFlag = GameObject.FindGameObjectWithTag(aiFlagTag);
        if (currentFlag == null)
        {
            Debug.LogError("AI flag GameObject not found with tag: " + aiFlagTag);
        }
        else
        {
            flagScript = currentFlag.GetComponent<Flag>();
        }
    }

    void Update()
    {
        switch (currentState)
        {
            case AIState.FetchingFlag:
                FetchingFlagBehavior();
                break;
            case AIState.ReturningFlag:
                ReturningFlagBehavior();
                break;
            case AIState.ChasingPlayer:
                ChasingPlayerBehavior();
                break;
        }
    }

    void FetchingFlagBehavior()
    {
        if (currentFlag != null)
        {
            agent.SetDestination(currentFlag.transform.position);
            if (Vector3.Distance(transform.position, currentFlag.transform.position) < 1.5f)
            {
                PickUpFlag();
                currentState = AIState.ReturningFlag;
            }
        }
    }

    void ReturningFlagBehavior()
    {
        if (currentFlag != null)
        {
            GameObject targetBase = null;
            if (currentFlag.CompareTag(playerFlagTag))
            {
                targetBase = GameObject.FindGameObjectWithTag("PlayerBase");
            }
            else if (currentFlag.CompareTag(aiFlagTag))
            {
                targetBase = GameObject.FindGameObjectWithTag("AIBase");
            }
            if (targetBase != null)
            {
                agent.SetDestination(targetBase.transform.position);
                if (Vector3.Distance(transform.position, targetBase.transform.position) < 1.5f)
                {
                    ReturnFlag();
                    currentState = AIState.FetchingFlag;
                }
            }
        }
    }

    void ChasingPlayerBehavior()
    {
        if (player != null)
        {
            agent.SetDestination(player.transform.position);
        }
    }

    private void PickUpFlag()
    {
        flagScript.PickUp();
        currentFlag = null; // Reset currentFlag after picking it up
    }

    private void ReturnFlag()
    {
        flagScript.ReturnToBase();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag(playerFlagTag) || collision.gameObject.CompareTag(aiFlagTag))
        {
            currentFlag = collision.gameObject;
            agent.isStopped = false; // Ensure the agent is not stopped
        }
    }
}
