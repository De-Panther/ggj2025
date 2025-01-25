using UnityEngine;
using System.Collections;
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
        if(other.tag == "Helicopter")
        {
            Debug.Log("Helicopter");
        }
        else if (other.tag == "UFO")
        {
            Debug.Log("UFO");
        }
        else
        {
            Debug.Log(other.tag);
        }
    }
}
