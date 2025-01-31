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
      head.SetLocalPositionAndRotation(XRReferences.Instance.head.localPosition, XRReferences.Instance.head.localRotation);
      handLeft.SetLocalPositionAndRotation(XRReferences.Instance.left.localPosition, XRReferences.Instance.left.localRotation);
      handRight.SetLocalPositionAndRotation(XRReferences.Instance.right.localPosition, XRReferences.Instance.right.localRotation);
      // TODO: Start/Stop fire the gun when right controller trigger
    }
  }
}
