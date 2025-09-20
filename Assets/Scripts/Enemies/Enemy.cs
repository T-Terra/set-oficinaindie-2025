using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField] protected float _maxHitPoints;
    [SerializeField] protected float _hitPoints;
    [SerializeField] protected int _damage;
    [SerializeField] protected int _range;

    [Header("Info")]
    [SerializeField] protected Vector2Int _position;

    [Header("Components")]
    [SerializeField] protected Collider2D _collider;


//! Estou desabilitando temporariamente um warning nas linhas "#pragma"
#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
    virtual public async void OnSpawn(Vector2Int position)
    {
        _hitPoints = _maxHitPoints;
        _position = position;
        _collider.enabled = true;
        // Spawn animation //! await ...
    }

    virtual public async void OnMove(Vector2Int movement)
    {
        // Move animation //! await ...
        // if (newPosition < 0) uhhh destroy?? (chegou na barrier)
        _position += movement;

        //? temp
        transform.position += (Vector3Int)movement;
    }

    virtual public async void OnAttack()
    {
        // Attack animation //! await ...
    }

    virtual public async void OnHurt(int damage)
    {
        // Hurt animation //! await ...
        if (damage > _hitPoints) OnDie();
    }

    virtual public async void OnDie()
    {
        _collider.enabled = false;
        // Death animation //! await ...
        // Free tile
        Destroy(gameObject);
    }
#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously

}