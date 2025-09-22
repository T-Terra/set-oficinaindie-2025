using UnityEngine;

public class OpenGateScript : MonoBehaviour
{
    [SerializeField] Collider2D _gateCollider;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            _gateCollider.enabled = false;
        }
    }
}