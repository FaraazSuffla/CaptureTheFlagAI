using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float rotationSpeed = 180f;

    private Rigidbody rb;
    private GameManager gameManager; // Reference to the GameManager
    private Flag currentFlag; // Reference to the currently held flag

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        gameManager = FindObjectOfType<GameManager>(); // Find the GameManager in the scene
    }

    void Update()
    {
        // Player movement
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        Vector3 movement = new Vector3(horizontalInput, 0f, verticalInput) * moveSpeed * Time.deltaTime;
        rb.MovePosition(rb.position + transform.TransformDirection(movement));

        // Player rotation
        float mouseX = Input.GetAxis("Mouse X") * rotationSpeed * Time.deltaTime;
        transform.Rotate(Vector3.up * mouseX);
    }

    // Detect when the player collides with the flag or base
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Flag Blue") && currentFlag == null)
        {
            // Pick up the flag
            Flag flag = other.GetComponent<Flag>();
            PickUpFlag(flag);
        }
        else if (other.CompareTag("PlayerBase") && currentFlag != null)
        {
            // Return the flag to base and reset the game
            ReturnFlag();
            gameManager.ResetGame(); // Reset the game without affecting the scores
            ResetPosition(); // Reset the player's position
        }
    }

    // Function to pick up the flag
    private void PickUpFlag(Flag flag)
    {
        if (flag != null)
        {
            currentFlag = flag;
            flag.PickUp();
        }
        else
        {
            Debug.LogError("Trying to pick up a null flag.");
        }
    }

    // Function to return the flag to base
    private void ReturnFlag()
    {
        currentFlag.ReturnToBase();
        currentFlag = null;
    }

    // Public method to reset the player's position
    public void ResetPosition()
    {
        // Set the player's position to the desired initial position
        transform.position = new Vector3(-9.5f, 2f, 10f);
    }

}
