using UnityEngine;
using System.Collections.Generic;

namespace GGJGame
{
  public class BubbleCollisionDetection : MonoBehaviour
  {
    public List<ParticleCollisionEvent> collisionEvents;
    private ParticleSystem part;

    void Start()
    {
      part = GetComponent<ParticleSystem>();
      collisionEvents = new List<ParticleCollisionEvent>();
    }

    void OnParticleCollision(GameObject other)
    {
      if (other.tag == "UFO")
      {
        GameState.Instance.AddAirStolen();
      }
    }
  }
}
