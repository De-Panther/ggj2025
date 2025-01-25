using UnityEngine;
using UnityEngine.UI;
using WebXR;

namespace GGJGame
{
  public class EnterXRButtons : MonoBehaviour
  {
    public Button enterARButton;
    public Button enterVRButton;

    private void OnEnable()
    {
      if (enterARButton != null)
      {
        enterARButton.interactable = WebXRManager.Instance.isSupportedAR;
      }
      if (enterVRButton != null)
      {
        enterVRButton.interactable = WebXRManager.Instance.isSupportedVR;
      }
      WebXRManager.OnXRCapabilitiesUpdate += OnXRCapabilitiesUpdate;
    }

    private void OnDisable()
    {
      WebXRManager.OnXRCapabilitiesUpdate -= OnXRCapabilitiesUpdate;
    }

    private void OnXRCapabilitiesUpdate(WebXRDisplayCapabilities capabilities)
    {
      if (enterARButton != null)
      {
        enterARButton.interactable = capabilities.canPresentAR;
      }
      if (enterVRButton != null)
      {
        enterVRButton.interactable = capabilities.canPresentVR;
      }
    }

    public void ToggleAR()
    {
      WebXRManager.Instance.ToggleAR();
    }

    public void ToggleVR()
    {
      WebXRManager.Instance.ToggleVR();
    }
  }
}
