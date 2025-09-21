using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class BallScript : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] Rigidbody2D _body;

    [Header("Settings")]
    [SerializeField] float _targetSpeed = 10f;

    [Header("Internal")]
    [SerializeField] bool _isClone = false;
    [SerializeField] List<BallScript> _clones = new ();

    void FixedUpdate()
    {
        float currentSpeed = _body.linearVelocity.magnitude;

        if (currentSpeed < _targetSpeed)
        {
            // nunca pode ser menor → normaliza
            _body.linearVelocity = _body.linearVelocity.normalized * _targetSpeed;
        }

    }

    void OnDestroy()
    {
        if (_isClone) return;
        foreach (var clone in _clones)
        {
            if (clone != null) Destroy(clone.gameObject);
        }
    }


    void OnCollisionEnter2D(Collision2D collision)
    {
        var data = GameManager.Instance.playerData;
        if (!_isClone && _clones.Count < data.clones) {
            var cloneObj = Instantiate(
                gameObject,
                transform.position + (Vector3)_body.linearVelocity.normalized * -0.1f,
                Quaternion.identity
            );
            var clone = cloneObj.GetComponent<BallScript>();
            clone._isClone = true;
            _clones.Add(clone);
        }

        if (collision.gameObject.CompareTag("Enemy"))
        {
            float damage = _isClone ? data.cloneDamage : data.damage;
            collision.gameObject.GetComponent<Enemy>().OnHurt(damage);
        }
    }
}