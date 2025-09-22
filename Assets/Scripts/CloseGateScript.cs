using UnityEngine;

public class CloseGateScript : MonoBehaviour
{
    [SerializeField] Collider2D _gateCollider;

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            _gateCollider.enabled = true;
        }
    }
}