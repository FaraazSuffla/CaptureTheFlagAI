using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float rotationSpeed = 180f;
    public LayerMask flagLayer;
    public Transform flagHolder;
    public GameObject bulletPrefab;
    public Transform firePoint;

    private Rigidbody rb;
    private bool hasFlag = false;

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

        // Shooting
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }

        // Check for flag interaction
        if (Input.GetKeyDown(KeyCode.E))
        {
            InteractWithFlag();
        }
    }

    void Shoot()
    {
        if (bulletPrefab != null && firePoint != null)
        {
            Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        }
    }

    void InteractWithFlag()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, 1f, flagLayer);
        foreach (Collider collider in hitColliders)
        {
            if (collider.CompareTag("Flag"))
            {
                if (!hasFlag)
                {
                    collider.transform.parent = flagHolder;
                    collider.transform.localPosition = Vector3.zero;
                    hasFlag = true;
                    Debug.Log("Flag picked up!");
                }
                else
                {
                    collider.transform.parent = null;
                    hasFlag = false;
                    Debug.Log("Flag dropped!");
                }
            }
        }
    }
}
