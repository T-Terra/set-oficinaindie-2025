using UnityEngine;

public class DestroyerScript : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            var ball = other.GetComponent<BallScript>();
            if (!ball.isClone) GameManager.Instance.LoseBall();

            Destroy(other.gameObject);
        }
    }
}
