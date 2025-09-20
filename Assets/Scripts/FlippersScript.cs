using UnityEngine;
using UnityEngine.InputSystem;

public class FlippersScript : MonoBehaviour
{
    [Header("Flipper Objects")]
    public HingeJoint2D leftFlipperJoint;
    public HingeJoint2D rightFlipperJoint;

    [Header("Motor Variables")]
    [SerializeField] float _motorSpeed = 1500f;
    [SerializeField] float _motorForce = 10000f;
    JointMotor2D _motor = new();

    void Awake()
    {
        _motor.maxMotorTorque = _motorForce;
    }

    public void OnLeft(InputAction.CallbackContext context)
    {
        Debug.Log(context.action);

        if (context.started)
        {
            _motor.motorSpeed = -_motorSpeed;
        }

        if (context.canceled)
        {
            _motor.motorSpeed = _motorSpeed;
        }

        leftFlipperJoint.motor = _motor;
    }

    public void OnRight(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            _motor.motorSpeed = _motorSpeed;
        }

        if (context.canceled)
        {
            _motor.motorSpeed = -_motorSpeed;
        }

        rightFlipperJoint.motor = _motor;
    }
}
