using UnityEngine;
using Coherence.Toolkit;

namespace GGJGame
{
  public class Alien : MonoBehaviour
  {
    [SerializeField]
    private CoherenceSync coherenceSync;
    [SerializeField]
    private Transform head;
    [SerializeField]
    private Transform handLeft;
    [SerializeField]
    private Transform handRight;

    private void Update()
    {
      if (!coherenceSync.HasStateAuthority)
      {
        return;
      }
      head.SetLocalPositionAndRotation(XRReferences.Instance.head.localPosition, XRReferences.Instance.head.localRotation);
      handLeft.SetLocalPositionAndRotation(XRReferences.Instance.left.localPosition, XRReferences.Instance.left.localRotation);
      handRight.SetLocalPositionAndRotation(XRReferences.Instance.right.localPosition, XRReferences.Instance.right.localRotation);
    }
  }
}
