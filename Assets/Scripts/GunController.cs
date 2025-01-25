using UnityEngine;

namespace GGJGame
{
  public class GunController : MonoBehaviour
  {
    private ParticleSystem bubbleParticles;

    void Awake()
    {
      bubbleParticles = GetComponentInChildren<ParticleSystem>();
      if (bubbleParticles == null)
      {
        Debug.LogError("No Particle System found in child objects!");
        enabled = false;
      }
    }

    public void StartFire()
    {
      if (!enabled)
      {
        return;
      }
      if (!bubbleParticles.isPlaying)
      {
        bubbleParticles.Play();
      }
    }

    public void StopFire()
    {
      if (!enabled)
      {
        return;
      }
      if (bubbleParticles.isPlaying)
      {
        bubbleParticles.Stop();
      }
    }
  }
}
