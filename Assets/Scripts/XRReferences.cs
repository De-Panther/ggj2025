using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.UI;

namespace GGJGame
{
  public class XRReferences : MonoBehaviour
  {
    public static XRReferences Instance;

    public Transform head;
    public Transform handLeft;
    public Transform handRight;
    public Transform controllerLeft;
    public Transform controllerRight;
    public Transform mobileLeft;
    public Transform mobileRight;

    public LazyFollow nonXRCameraFollow;

    private void Awake()
    {
      if (Instance != null)
      {
        Destroy(this);
      }
      Instance = this;
    }

    public Transform GetLeft()
    {
      if (controllerLeft.gameObject.activeInHierarchy)
      {
        return controllerLeft;
      }
      if (handLeft.gameObject.activeInHierarchy)
      {
        return handLeft;
      }
      return mobileLeft;
    }

    public Transform GetRight()
    {
      if (controllerRight.gameObject.activeInHierarchy)
      {
        return controllerRight;
      }
      if (handRight.gameObject.activeInHierarchy)
      {
        return handRight;
      }
      return mobileRight;
    }
  }
}
