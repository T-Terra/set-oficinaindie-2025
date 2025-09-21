using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class BallScript : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] Rigidbody2D _body;

    [Header("Settings")]
    [SerializeField] float _targetSpeed = 10f;

    [Header("Clones")]
    public bool isClone = false;
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
        if (isClone) return;
        foreach (var clone in _clones)
        {
            if (clone != null) Destroy(clone.gameObject);
        }
    }


    void OnCollisionEnter2D(Collision2D collision)
    {
        var data = GameManager.Instance.playerData;
        _clones = _clones.Where(c => c != null).ToList();
        if (!isClone && _clones.Count < data.clones)
        {
            var cloneObj = Instantiate(
                gameObject,
                transform.position + (Vector3)_body.linearVelocity.normalized * -0.2f,
                Quaternion.identity
            );
            var clone = cloneObj.GetComponent<BallScript>();
            var cloneBody = cloneObj.GetComponent<Rigidbody2D>();
            clone.isClone = true;
            _clones.Add(clone);
        }

        if (collision.gameObject.CompareTag("Enemy"))
        {
            float damage = isClone ? data.cloneDamage : data.damage;
            collision.gameObject.GetComponent<Enemy>().OnHurt(damage);
        }
    }
}