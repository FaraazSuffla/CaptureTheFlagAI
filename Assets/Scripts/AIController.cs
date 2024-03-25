using UnityEngine;

public class AIController : MonoBehaviour
{
    public float moveSpeed = 3f;
    public Transform target;

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (target != null)
        {
            // Calculate direction to the target
            Vector3 direction = (target.position - transform.position).normalized;

            // Move towards the target
            rb.MovePosition(rb.position + direction * moveSpeed * Time.deltaTime);

            // Rotate towards the target (optional)
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, lookRotation, Time.deltaTime * 180f);
        }
    }
}
