using System.Collections;
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
    [SerializeField] protected Animator _animator;

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



    public void OnSpawn(Vector2Int position)
    {
        _animator.Play("Spawn");
        StartCoroutine(SpawnRoutine(position));
    }

    public void OnMove(Vector2Int movement)
    {

        _position += movement;
        if (_position.y < 0)
        {
            StartCoroutine(EndRoutine());
            return;
        }

        _endPosistionY = transform.position.y + movement.y * moveDistance;

        _animator.Play("Move");
        StartCoroutine(MoveRoutine(movement));
    }

    public void OnEnd()
    {
        _animator.Play("End");
        StartCoroutine(EndRoutine());
    }

    public void OnAttack()
    {
        _animator.Play("Attack");
        StartCoroutine(AttackRoutine());
    }

    public void OnHurt(float damage)
    {
        _animator.Play("Hurt");
        StartCoroutine(HurtRoutine(damage));
    }

    public void OnDie()
    {
        _collider.enabled = false;
        _animator.Play("Die");
        StartCoroutine(DieRoutine());
    }


    virtual protected IEnumerator SpawnRoutine(Vector2Int position)
    {
        yield return new WaitForSeconds(_animator.GetCurrentAnimatorStateInfo(0).length);

        _hitPoints = _maxHitPoints;
        _position = position;
        _collider.enabled = true;
    }

    virtual protected IEnumerator MoveRoutine(Vector2Int movement)
    {
        yield return new WaitForSeconds(_animator.GetCurrentAnimatorStateInfo(0).length);

        _elapsedTime = 0f;
        _startPosistionY = _endPosistionY;
        if (_rigidbody != null) _rigidbody.MovePosition(new Vector2(transform.position.x, _endPosistionY));

    }

    virtual protected IEnumerator EndRoutine()
    {
        yield return new WaitForSeconds(_animator.GetCurrentAnimatorStateInfo(0).length);

        Destroy(gameObject);
    }

    virtual protected IEnumerator AttackRoutine()
    {
        yield return new WaitForSeconds(_animator.GetCurrentAnimatorStateInfo(0).length);
    }

    virtual protected IEnumerator HurtRoutine(float damage)
    {
        yield return new WaitForSeconds(_animator.GetCurrentAnimatorStateInfo(0).length);

        _hitPoints -= damage;
        if (_hitPoints <= 0) StartCoroutine(DieRoutine());
    }

    virtual protected IEnumerator DieRoutine()
    {
        yield return new WaitForSeconds(_animator.GetCurrentAnimatorStateInfo(0).length);

        GameManager.Instance.playerData.AddExperience(_xpValue);
        Destroy(gameObject);
    }

}