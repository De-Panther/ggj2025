using System;
using UnityEngine;
using Coherence.Toolkit;

namespace GGJGame
{
  public class Alien : MonoBehaviour
  {
    public static Action OnTotalChanged;
    public static int totalAliens { get; private set; } = 0;
    [SerializeField]
    private CoherenceSync coherenceSync;
    [SerializeField]
    private Transform head;
    [SerializeField]
    private Transform handLeft;
    [SerializeField]
    private Transform handRight;
    [SerializeField]
    private GunController gunController;

    private void Start()
    {
      gunController.StartFire();
    }

    private void OnEnable()
    {
      totalAliens++;
      OnTotalChanged?.Invoke();
    }

    private void OnDisable()
    {
      totalAliens--;
      OnTotalChanged?.Invoke();
    }

    private void Update()
    {
      if (!coherenceSync.HasStateAuthority)
      {
        return;
      }
      head.SetPositionAndRotation(XRReferences.Instance.head.position, XRReferences.Instance.head.rotation);
      handLeft.SetPositionAndRotation(XRReferences.Instance.left.position, XRReferences.Instance.left.rotation);
      handRight.SetPositionAndRotation(XRReferences.Instance.right.position, XRReferences.Instance.right.rotation);
      // TODO: Start/Stop fire the gun when right controller trigger
    }
  }
}
