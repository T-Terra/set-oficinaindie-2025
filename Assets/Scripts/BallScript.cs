using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class BallScript : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] Rigidbody2D _body;

    [Header("Settings")]
    [SerializeField] float _targetSpeed = 10f;


    void FixedUpdate()
    {
        float currentSpeed = _body.linearVelocity.magnitude;

        if (currentSpeed < _targetSpeed)
        {
            // nunca pode ser menor → normaliza
            _body.linearVelocity = _body.linearVelocity.normalized * _targetSpeed;
        }

    }
}