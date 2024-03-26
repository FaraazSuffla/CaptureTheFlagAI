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
    public GameObject aiBase;
    public string playerFlagTag = "Flag Blue"; // Tag of the player's flag GameObject
    public string aiFlagTag = "Flag Red"; // Tag of the AI agent's flag GameObject

    public AIState currentState = AIState.FetchingFlag; // Default to fetching flag

    private NavMeshAgent agent; // Reference to the NavMeshAgent component
    private GameObject currentFlag; // Reference to the currently held flag

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        Invoke("FindFlag", 0.1f); // Call FindFlag method after a short delay
    }

    // Method to find the flag GameObject
    void FindFlag()
    {
        // Find the flag GameObject with the specified tag
        currentFlag = GameObject.FindGameObjectWithTag(aiFlagTag);

        // Check if flag GameObject is found
        if (currentFlag == null)
        {
            Debug.LogError("AI flag GameObject not found with tag: " + aiFlagTag);
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

            if (Vector3.Distance(transform.position, currentFlag.transform.position) < 1f)
            {
                // AI reached the flag
                // Pick up the flag
                PickUpFlag();
                // Transition to ReturningFlag state
                currentState = AIState.ReturningFlag;
            }
        }
    }

    void ReturningFlagBehavior()
    {
        if (aiBase != null && currentFlag != null)
        {
            agent.SetDestination(aiBase.transform.position);

            if (Vector3.Distance(transform.position, aiBase.transform.position) < 1f)
            {
                // AI reached its base
                // Return the flag to base
                ReturnFlag();
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

    // Function to pick up the flag
    private void PickUpFlag()
    {
        currentFlag.GetComponent<Flag>().PickUp();
    }

    // Function to return the flag to base
    private void ReturnFlag()
    {
        currentFlag.GetComponent<Flag>().ReturnToBase();
    }

    // This method is called when the player captures the flag
    public void PlayerCapturedFlag()
    {
        // Transition to ChasingPlayer state
        currentState = AIState.ChasingPlayer;
    }
}
