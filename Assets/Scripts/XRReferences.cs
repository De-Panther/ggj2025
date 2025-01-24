using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.UI;

namespace GGJGame
{
  public class XRReferences : MonoBehaviour
  {
    public static XRReferences Instance;

    public Transform head;
    public Transform left => controllerLeft.gameObject.activeSelf ? controllerLeft : handLeft;
    public Transform right => controllerRight.gameObject.activeSelf ? controllerRight : handRight;
    public Transform handLeft;
    public Transform handRight;
    public Transform controllerLeft;
    public Transform controllerRight;

    public LazyFollow nonXRCameraFollow;

    private void Awake()
    {
      if (Instance != null)
      {
        Destroy(this);
      }
      Instance = this;
    }
  }
}
