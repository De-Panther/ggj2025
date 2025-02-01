using UnityEngine;
using System.Collections.Generic;

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
      Debug.Log($"{other.tag} {other.name}");
    }
}
