using UnityEngine;

public class Arrow : MonoBehaviour
{
    [SerializeField] float _speed = 2f;
    [SerializeField] Rigidbody2D _rigidbody;

    void Start()
    {
        // go in the direction the arrow is facing
        _rigidbody.linearVelocity = -transform.up * _speed;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            GameManager.Instance.playerData.TakeDamage(1);
            Destroy(gameObject);
        }
    }
}