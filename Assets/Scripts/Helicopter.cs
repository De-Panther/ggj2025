using System;
using UnityEngine;
using UnityEngine.InputSystem;
using Coherence.Toolkit;

namespace GGJGame
{
  public class Helicopter : MonoBehaviour
  {
    public static Action OnTotalChanged;
    public static int totalHelicopters { get; private set; } = 0;
    [SerializeField]
    private CoherenceSync coherenceSync;
    [SerializeField]
    private Transform propeller;
    [SerializeField]
    private Rigidbody _Rigidbody;

    private float heightSpeed = 14f;
    private float sidesSpeed = 14f;
    private float forwardSpeed = 20f;
    private float rotationSpeed = 45f;
    private float propellerSpeed = 900f;

    private Vector2 leftStick = Vector2.zero;
    private Vector2 rightStick = Vector2.zero;

    private void OnEnable()
    {
      totalHelicopters++;
      OnTotalChanged?.Invoke();
    }

    private void OnDisable()
    {
      totalHelicopters--;
      OnTotalChanged?.Invoke();
    }

    private void Update()
    {
      propeller.Rotate(0, propellerSpeed * Time.deltaTime * (1 + Mathf.Abs(leftStick.x) + Mathf.Abs(leftStick.y) + Mathf.Abs(rightStick.x) + Mathf.Clamp(rightStick.y, -0.25f, 1f)), 0, Space.Self);
      if (!coherenceSync.HasStateAuthority)
      {
        return;
      }
      Keyboard keyboard = Keyboard.current;
      Gamepad gamepad = Gamepad.current;
      if (keyboard != null)
      {
        leftStick.x = keyboard.aKey.isPressed ? -1 : 0;
        leftStick.x += keyboard.dKey.isPressed ? 1 : 0;
        leftStick.y = keyboard.wKey.isPressed ? 1 : 0;
        leftStick.y += keyboard.sKey.isPressed ? -1 : 0;
        rightStick.x = keyboard.jKey.isPressed ? -1 : 0;
        rightStick.x += keyboard.lKey.isPressed ? 1 : 0;
        rightStick.y = keyboard.iKey.isPressed ? 1 : 0;
        rightStick.y += keyboard.kKey.isPressed ? -1 : 0;
      }
      else
      {
        leftStick = Vector2.zero;
        rightStick = Vector2.zero;
      }
      if (gamepad == null)
      {
        return;
      }
      leftStick += gamepad.leftStick.ReadValue(); // Forward and Rotation
      rightStick += gamepad.rightStick.ReadValue(); // Height and Sides
      leftStick.x = Mathf.Clamp(leftStick.x, -1f, 1f);
      leftStick.y = Mathf.Clamp(leftStick.y, -1f, 1f);
      rightStick.x = Mathf.Clamp(rightStick.x, -1f, 1f);
      rightStick.y = Mathf.Clamp(rightStick.y, -1f, 1f);
    }

    private void FixedUpdate()
    {
      if (!coherenceSync.HasStateAuthority)
      {
        return;
      }

      Vector3 targetAngularVelocity = new Vector3(0, leftStick.x * rotationSpeed * Mathf.Deg2Rad, 0);
      Vector3 targetVelocity = new Vector3(rightStick.x * sidesSpeed, // Sides
        rightStick.y * heightSpeed, // Height
        leftStick.y * forwardSpeed); // Forward
      _Rigidbody.angularVelocity = Vector3.zero;
      _Rigidbody.linearVelocity = Vector3.zero;
      _Rigidbody.AddRelativeTorque(targetAngularVelocity, ForceMode.VelocityChange);
      _Rigidbody.AddRelativeForce(targetVelocity, ForceMode.VelocityChange);
      Quaternion currentRotation = _Rigidbody.rotation;
      Quaternion targetRotation = Quaternion.Euler(0, currentRotation.eulerAngles.y, 0);
      _Rigidbody.MoveRotation(Quaternion.RotateTowards(currentRotation, targetRotation, rotationSpeed * 0.1f));
    }
  }
}
