using UnityEngine;
using Coherence.Toolkit;
using WebXR;

namespace GGJGame
{
  public class ClientInstantiator : MonoBehaviour
  {
    [SerializeField]
    private CoherenceSync coherenceSync;
    [SerializeField]
    private GameObject alienPrefab;
    [SerializeField]
    private GameObject helicopterPrefab;

    private void Start()
    {
      if (!coherenceSync.HasStateAuthority)
      {
        return;
      }
      Instantiate(WebXRManager.Instance.XRState == WebXRState.NORMAL ?
        helicopterPrefab : alienPrefab);
    }
  }
}
