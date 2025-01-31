using System;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.UI;
using Coherence.Toolkit;
using WebXR;

namespace GGJGame
{
  public class ClientInstantiator : MonoBehaviour
  {
    public static Action OnInstanceCreated;
    public static ClientInstantiator Instance;
    [SerializeField]
    private CoherenceSync coherenceSync;
    [SerializeField]
    private GameObject alienPrefab;
    [SerializeField]
    private GameObject helicopterPrefab;

    private GameObject player;
    private LazyFollow lazyFollow;
    private bool isHelicopter = false;

    private void Start()
    {
      if (!coherenceSync.HasStateAuthority)
      {
        return;
      }
      Instance = this;
      isHelicopter = WebXRManager.Instance.XRState == WebXRState.NORMAL;
      player = Instantiate(isHelicopter ?
        helicopterPrefab : alienPrefab);
      if (isHelicopter)
      {
        lazyFollow = XRReferences.Instance.nonXRCameraFollow;
        lazyFollow.target = player.transform;
        lazyFollow.enabled = true;
      }
      OnInstanceCreated?.Invoke();
    }

    private void OnDestroy()
    {
      if (lazyFollow != null)
      {
        lazyFollow.enabled = false;
      }
      Destroy(player);
    }

    public bool GetIsHelicopter()
    {
      return isHelicopter;
    }
  }
}
