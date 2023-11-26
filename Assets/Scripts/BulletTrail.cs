using UnityEngine;

public class BulletTrail : MonoBehaviour
{
    private Vector3 targetPosition;

    public void SetTarget(Vector3 target)
    {
        targetPosition = target;
    }

    void Update()
    {
        // Move the bullet trail towards the target position
        transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * 10f);

        // You might want to destroy the trail when it reaches the target or after a certain time
        // For example, if the distance between the current position and the target is small enough
        if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
        {
            Destroy(gameObject);
        }
    }
}
