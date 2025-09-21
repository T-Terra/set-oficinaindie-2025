using UnityEngine;

public class CoinScript : MonoBehaviour
{
    [SerializeField] Collider2D _collider;
    [SerializeField] SpriteRenderer _spriteRenderer;

    void OnEnable()
    {
        WaveManager.OnWaveStart += Restock;
    }

    void OnDisable()
    {
        WaveManager.OnWaveStart -= Restock;
    }

    void Restock()
    {
        _collider.enabled = true;
        _spriteRenderer.enabled = true;
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.Instance.playerData.AddCoins(5);
            _collider.enabled = false;
            _spriteRenderer.enabled = false;
        }
    }
}
