using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.UI;
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

    private GameObject player;
    private LazyFollow lazyFollow;

    private void Start()
    {
      if (!coherenceSync.HasStateAuthority)
      {
        return;
      }
      player = Instantiate(WebXRManager.Instance.XRState == WebXRState.NORMAL ?
        helicopterPrefab : alienPrefab);
      if (WebXRManager.Instance.XRState == WebXRState.NORMAL)
      {
        lazyFollow = FindFirstObjectByType<LazyFollow>(FindObjectsInactive.Include);
        lazyFollow.target = player.transform;
        lazyFollow.enabled = true;
      }
    }

    private void OnDestroy()
    {
      if (lazyFollow != null)
      {
        lazyFollow.enabled = false;
      }
      Destroy(player);
    }
  }
}
