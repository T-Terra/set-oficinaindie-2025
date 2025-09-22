using System.Collections.Generic;
using UnityEngine;

public class BumperScript : MonoBehaviour
{

    [SerializeField] Collider2D _collider;
    [SerializeField] SpriteRenderer _spriteRenderer;
    [SerializeField] List<Sprite> _sprites;

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
        if (_collider.enabled) return; // already active

        _spriteRenderer.sprite = _sprites[0];
        
        _hitPoints = _maxHitPoints;
        _collider.enabled = true;
        _spriteRenderer.enabled = true;
    }

    [SerializeField] float _maxHitPoints = 3f;
    [SerializeField] float _hitPoints = 3f;

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (--_hitPoints <= 0)
            {
                GameManager.Instance.playerData.AddCoins(5);
                _collider.enabled = false;
                _spriteRenderer.enabled = false;
                return;
            }

            int spriteIndex = Mathf.Clamp(
                _sprites.Count - (int)(_hitPoints / _maxHitPoints * _sprites.Count),
                0, _sprites.Count - 1
            );

            _spriteRenderer.sprite = _sprites[spriteIndex];
        }
    }
}
