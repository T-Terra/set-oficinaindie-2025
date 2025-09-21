using UnityEngine;

public class DestroyerScript : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // GameManager.Instance.LoseBall();
            Destroy(other.gameObject);
        }
    }
}
