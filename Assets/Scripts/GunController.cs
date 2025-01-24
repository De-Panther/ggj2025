using UnityEngine;

public class GunController : MonoBehaviour
{
    private ParticleSystem bubbleParticles;

    void Start()
    {
        bubbleParticles = GetComponentInChildren<ParticleSystem>();
        
        if (bubbleParticles == null)
        {
            Debug.LogError("No Particle System found in child objects!");
        }
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.A))
        {
            if (!bubbleParticles.isPlaying)
                bubbleParticles.Play(); 
        }
        else
        {
            if (bubbleParticles.isPlaying)
                bubbleParticles.Stop();
        }
    }
}
