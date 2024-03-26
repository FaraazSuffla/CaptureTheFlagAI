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
        // Display the AI state in the console
        Debug.Log("AI State: " + currentState);

        // Check if the flag is picked up and the AI is not already returning the flag
        if (flagScript.currentState == Flag.FlagState.PickedUp && currentState != AIState.ReturningFlag)
        {
            currentState = AIState.ReturningFlag; // Transition to ReturningFlag state
            agent.SetDestination(aiBase.transform.position); // Set destination to AI base
        }

        // Handle AI behavior based on the current state
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
                // Set destination to AI base after picking up the flag
                agent.SetDestination(aiBase.transform.position);
            }
        }
    }

    void ReturningFlagBehavior()
    {
        Debug.Log("ReturningFlagBehavior");
        // Return the flag to base
        if (currentFlag != null)
        {
            if (Vector3.Distance(transform.position, aiBase.transform.position) < 1.5f)
            {
                ReturnFlag();
                // Set visibility of the flag back to true
                flagScript.gameObject.SetActive(true);
                currentState = AIState.FetchingFlag; // Transition back to FetchingFlag state
                currentFlag = null; // Reset currentFlag after returning it to base
                Debug.Log("Flag returned, currentState set to FetchingFlag");
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

    // OnTriggerEnter is called when the Collider other enters the trigger
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("AIBase"))
        {
            // Set visibility of the flag back to true
            flagScript.gameObject.SetActive(true);
        }
    }
}
