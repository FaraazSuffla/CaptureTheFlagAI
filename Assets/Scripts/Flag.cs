using UnityEngine;

public class Flag : MonoBehaviour
{
    // Enum to represent the state of the flag
    public enum FlagState
    {
        AtBase,
        PickedUp
    }

    public FlagState currentState = FlagState.AtBase; // Default state is at base

    // Function to pick up the flag
    public void PickUp()
    {
        currentState = FlagState.PickedUp;
        gameObject.SetActive(false); // Disable the flag GameObject
    }

    // Function to return the flag to base
    public void ReturnToBase()
    {
        currentState = FlagState.AtBase;
        gameObject.SetActive(true); // Enable the flag GameObject
    }
}
