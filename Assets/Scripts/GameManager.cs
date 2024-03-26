using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int playerScore = 0;
    public int aiScore = 0;

    public void PlayerReturnedFlag()
    {
        playerScore++;
        CheckEndCondition();
    }

    public void AIReturnedFlag()
    {
        aiScore++;
        CheckEndCondition();
    }

    private void CheckEndCondition()
    {
        if (playerScore >= 5)
        {
            Debug.Log("Player wins the game!");
            ResetGame();
        }
        else if (aiScore >= 5)
        {
            Debug.Log("AI wins the game!");
            ResetGame();
        }
        else
        {
            Debug.Log("Round ended.");
            ResetRound();
        }
    }

    private void ResetRound()
    {
        // Reset flag positions, player positions, AI positions, etc.
        Flag[] flags = FindObjectsOfType<Flag>();
        foreach (Flag flag in flags)
        {
            flag.ReturnToBase();
        }

        PlayerController player = FindObjectOfType<PlayerController>();
        if (player != null)
        {
            player.ResetPosition();
        }

        AIController[] aiControllers = FindObjectsOfType<AIController>();
        foreach (AIController aiController in aiControllers)
        {
            aiController.ResetPosition();
        }
    }

    public void ResetGame()
    {
        playerScore = 0;
        aiScore = 0;

        ResetRound();
    }
}
