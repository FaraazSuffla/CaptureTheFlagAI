using UnityEngine;

public class FlagSpawner : MonoBehaviour
{
    public GameObject redFlagPrefab; // Reference to the red flag prefab
    public GameObject blueFlagPrefab; // Reference to the blue flag prefab
    public Transform redBasePosition; // Position for spawning red flag
    public Transform blueBasePosition; // Position for spawning blue flag

    void Start()
    {
        SpawnFlags();
    }

    void SpawnFlags()
    {
        // Spawn red flag at blue base position
        GameObject redFlag = Instantiate(redFlagPrefab, blueBasePosition.position, Quaternion.identity);
        // Attach the Flag script to the red flag GameObject if not already attached
        if (redFlag.GetComponent<Flag>() == null)
        {
            redFlag.AddComponent<Flag>();
        }
        // Ensure that the red flag is tagged as "Flag Red"
        redFlag.tag = "Flag Red";

        // Spawn blue flag at red base position
        GameObject blueFlag = Instantiate(blueFlagPrefab, redBasePosition.position, Quaternion.identity);
        // Attach the Flag script to the blue flag GameObject if not already attached
        if (blueFlag.GetComponent<Flag>() == null)
        {
            blueFlag.AddComponent<Flag>();
        }
        // Ensure that the blue flag is tagged as "Flag Blue"
        blueFlag.tag = "Flag Blue";

        // Set parent to this object for organization
        redFlag.transform.SetParent(transform);
        blueFlag.transform.SetParent(transform);
    }
}
