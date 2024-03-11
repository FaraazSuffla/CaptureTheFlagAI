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
        // Spawn blue flag at red base position
        GameObject blueFlag = Instantiate(blueFlagPrefab, redBasePosition.position, Quaternion.identity);

        // Set parent to this object for organization
        redFlag.transform.SetParent(transform);
        blueFlag.transform.SetParent(transform);
    }
}
