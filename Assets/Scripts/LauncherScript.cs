using UnityEngine;
using UnityEngine.InputSystem;

public class LauncherScript : MonoBehaviour
{
    [Header("Ball Setup")]
    [SerializeField] GameObject _ballPrefab;

    [Header("Launcher Settings")]
    [SerializeField] float _baseForce = 25f;
    [SerializeField] float _forceRange = 50f;
    [SerializeField] float _chargeTime = 3f;

    [Header("Internal Values")]
    [SerializeField] float _chargeTimeElapsed = 0f;
    [SerializeField] bool _isCharging = false;


    [ContextMenu("Launch Ball")]
    void LaunchBall(float? forceValue = null)
    {
        GameObject ballInstance = Instantiate(_ballPrefab, transform);
        Rigidbody2D ballRB = ballInstance.GetComponent<Rigidbody2D>();

        if (forceValue == null)
        {
            if (_isCharging) forceValue = _chargeTimeElapsed / _chargeTime;
            else forceValue = Random.value;
        }

        float force = _baseForce + forceValue.Value * _forceRange;

        ballRB.AddForce(force * Vector2.up, ForceMode2D.Impulse);


        _chargeTimeElapsed = 0f;
        _isCharging = false;
    }

    public void OnLaunch(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            _isCharging = true;
        }

        if (context.canceled && _isCharging)
        {
            LaunchBall();
        }
    }

    void Update()
    {
        if (!_isCharging) return;

        _chargeTimeElapsed += Time.deltaTime;
        if (_chargeTimeElapsed >= _chargeTime)
        {
            LaunchBall();
        }

    }

}
