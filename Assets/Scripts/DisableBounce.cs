using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class DisableBounce : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (other.TryGetComponent<Rigidbody2D>(out var ballBody))
                ballBody.sharedMaterial.bounciness = 0f;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (other.TryGetComponent<Rigidbody2D>(out var ballBody))
                ballBody.sharedMaterial.bounciness = 1f;
        }
    }
}