using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class DisableBounce : MonoBehaviour
{

    [SerializeField] PhysicsMaterial2D _notBouncy;
    [SerializeField] PhysicsMaterial2D _bouncy;
    


    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (other.TryGetComponent<Rigidbody2D>(out var ballBody))
                ballBody.sharedMaterial = _notBouncy;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (other.TryGetComponent<Rigidbody2D>(out var ballBody))
                ballBody.sharedMaterial = _bouncy;
        }
    }
}