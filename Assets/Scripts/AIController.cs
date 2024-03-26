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
    // Public fields for references
    public GameObject player;
    public GameObject ownBase;
    public string flagTag = "Flag Red"; // Tag of the flag GameObject

    public AIState currentState = AIState.FetchingFlag; // Default to fetching flag

    private NavMeshAgent agent; // Reference to the NavMeshAgent component
    private GameObject flag; // Reference to the flag GameObject

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        // Call FindFlag method after a short delay
        Invoke("FindFlag", 0.1f);
    }

    // Method to find the flag GameObject
    void FindFlag()
    {
        // Find the flag GameObject with the specified tag
        flag = GameObject.FindWithTag(flagTag);

        // Check if flag GameObject is found
        if (flag == null)
        {
            Debug.LogError("Flag GameObject not found with tag: " + flagTag);
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
        if (flag != null)
        {
            agent.SetDestination(flag.transform.position);

            if (Vector3.Distance(transform.position, flag.transform.position) < 1f)
            {
                // AI reached the flag
                // Transition to ReturningFlag state
                currentState = AIState.ReturningFlag;
            }
        }
    }

    void ReturningFlagBehavior()
    {
        if (ownBase != null)
        {
            agent.SetDestination(ownBase.transform.position);

            if (Vector3.Distance(transform.position, ownBase.transform.position) < 1f)
            {
                // AI reached its own base
                // Transition back to fetching flag
                currentState = AIState.FetchingFlag;
            }
        }
    }

    void ChasingPlayerBehavior()
    {
        if (player != null)
        {
            agent.SetDestination(player.transform.position);
            // You might want to add more sophisticated logic for chasing the player
        }
    }

    // This method is called when the player captures the flag
    public void PlayerCapturedFlag()
    {
        // Transition to ChasingPlayer state
        currentState = AIState.ChasingPlayer;
    }
}
