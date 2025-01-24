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
      Vector2 leftStick = gamepad.leftStick.ReadValue(); // Forward and Rotation
      Vector2 rightStick = gamepad.rightStick.ReadValue(); // Height and Sides

      transform.Rotate(0, leftStick.x * rotationSpeed * Time.deltaTime, 0, Space.Self);
      transform.Translate(rightStick.x * sidesSpeed * Time.deltaTime, // Sides
        rightStick.y * heightSpeed * Time.deltaTime, // Height
        leftStick.y * forwardSpeed * Time.deltaTime, // Forward
        Space.Self);
    }
  }
}
