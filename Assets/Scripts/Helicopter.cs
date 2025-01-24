using UnityEngine;
using Coherence.Toolkit;

namespace GGJGame
{
  public class Helicopter : MonoBehaviour
  {
    [SerializeField]
    private CoherenceSync coherenceSync;
    [SerializeField]
    private Transform propeller;

    private void Update()
    {
      propeller.Rotate(0, 90*Time.deltaTime, 0, Space.Self);
      if (coherenceSync.HasStateAuthority)
      {
        return;
      }
      // Owner code
    }
  }
}
