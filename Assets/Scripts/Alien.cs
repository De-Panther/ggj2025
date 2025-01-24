using UnityEngine;
using Coherence.Toolkit;

namespace GGJGame
{
  public class Alien : MonoBehaviour
  {
    [SerializeField]
    private CoherenceSync coherenceSync;
    [SerializeField]
    private Transform deviceHead;
    [SerializeField]
    private Transform deviceHandLeft;
    [SerializeField]
    private Transform deviceHandRight;
    [SerializeField]
    private Transform head;
    [SerializeField]
    private Transform handLeft;
    [SerializeField]
    private Transform handRight;

    private void Update()
    {
      if (coherenceSync.HasStateAuthority)
      {
        return;
      }
      head.SetLocalPositionAndRotation(deviceHead.localPosition, deviceHead.localRotation);
      handLeft.SetLocalPositionAndRotation(deviceHandLeft.localPosition, deviceHandLeft.localRotation);
      handRight.SetLocalPositionAndRotation(handRight.localPosition, handRight.localRotation);
    }
  }
}
