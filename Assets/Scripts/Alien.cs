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
      Transform left = XRReferences.Instance.GetLeft();
      handLeft.SetPositionAndRotation(left.position, left.rotation);
      Transform right = XRReferences.Instance.GetRight();
      handRight.SetPositionAndRotation(right.position, right.rotation);
      // TODO: Start/Stop fire the gun when right controller trigger
    }
  }
}
