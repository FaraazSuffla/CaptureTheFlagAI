using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float rotationSpeed = 180f;

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
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
}
