using UnityEngine;

public class Arrow : MonoBehaviour
{
    [SerializeField] float _speed = 2f;
    [SerializeField] Rigidbody2D _rigidbody;

    void Start()
    {
        _rigidbody.linearVelocity = Vector2.down * _speed;
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