using UnityEngine;
using UnityEngine.InputSystem;
using Coherence.Toolkit;

namespace GGJGame
{
  public class Helicopter : MonoBehaviour
  {
    [SerializeField]
    private CoherenceSync coherenceSync;
    [SerializeField]
    private Transform propeller;

    private float heightSpeed = 7f;
    private float sidesSpeed = 7f;
    private float forwardSpeed = 10f;
    private float rotationSpeed = 45f;

    private void Update()
    {
      propeller.Rotate(0, 90*Time.deltaTime, 0, Space.Self);
      if (!coherenceSync.HasStateAuthority)
      {
        return;
      }
      Gamepad gamepad = Gamepad.current;
      Vector2 leftStick = gamepad.leftStick.ReadValue(); // Height and Sides
      Vector2 rightStick = gamepad.rightStick.ReadValue(); // Forward and Rotation

      transform.Rotate(0, rightStick.x * rotationSpeed * Time.deltaTime, 0, Space.Self);
      transform.Translate(leftStick.x * sidesSpeed * Time.deltaTime,
        leftStick.y * heightSpeed * Time.deltaTime,
        rightStick.y * forwardSpeed * Time.deltaTime,
        Space.Self);
    }
  }
}
