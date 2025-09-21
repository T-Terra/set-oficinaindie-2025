using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField] protected float _maxHitPoints;
    [SerializeField] protected float _hitPoints;
    [SerializeField] protected int _damage;
    [SerializeField] protected int _range;
    [SerializeField] protected int _xpValue;

    [Header("Info")]
    [SerializeField] protected Vector2Int _position;

    [Header("Components")]
    [SerializeField] protected Collider2D _collider;
    [SerializeField] protected Rigidbody2D _rigidbody;

    [Header("Movement")]
    [SerializeField] protected float _startPosistionY = 0f;
    [SerializeField] protected float _endPosistionY = 0f;
    [SerializeField] protected float _moveDuration = 0.8f;
    [SerializeField] protected float _elapsedTime = 0f;
    public float moveDistance = 1.0f;

    void Awake()
    {
        _startPosistionY = transform.position.y;
        _endPosistionY = transform.position.y;
    }


    void FixedUpdate()
    {
        if (_startPosistionY <= _endPosistionY) return;

        _elapsedTime += Time.fixedDeltaTime;
        if (_elapsedTime < _moveDuration)
        {
            float newY = Mathf.Lerp(_startPosistionY, _endPosistionY, _elapsedTime / _moveDuration);
            _rigidbody.MovePosition(new Vector2(transform.position.x, newY));
            return;
        }

        _elapsedTime = 0f;
        _startPosistionY = _endPosistionY;
        _rigidbody.MovePosition(new Vector2(transform.position.x, _endPosistionY));
    }


    //! Estou desabilitando temporariamente um warning nas linhas "#pragma"
#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
    virtual public async void OnSpawn(Vector2Int position)
    {
        // Spawn animation //! await ...
        _hitPoints = _maxHitPoints;
        _position = position;
        _collider.enabled = true;
    }

    virtual public async void OnMove(Vector2Int movement)
    {

        _position += movement;
        if (_position.y < 0)
        {
            OnDie(); // na real é algo diferente de só morrer, ele não vai dar XP nem nada e ainda vai dar dano na barreira
            return;
        }

        _endPosistionY = transform.position.y + movement.y * moveDistance;

        // Move animation //! await ...
        // o movimento é baseado em física, a animação é só visual
        OnAttack();
    }

    virtual public async void OnAttack()
    {
        // Attack animation //! await ...
    }

    virtual public async void OnHurt(float damage)
    {
        // Hurt animation //! await ...
        _hitPoints -= damage;
        if (_hitPoints <= 0) OnDie();
    }

    virtual public async void OnDie()
    {
        _collider.enabled = false;
        // Death animation //! await ...
        GameManager.Instance.playerData.AddExperience(_xpValue);
        Destroy(gameObject);
    }
#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously

}