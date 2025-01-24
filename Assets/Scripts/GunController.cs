using UnityEngine;
using UnityEngine.InputSystem;

public class GunController : MonoBehaviour
{
    private ParticleSystem bubbleParticles;
    Keyboard keyboard;


    void Start()
    {
        bubbleParticles = GetComponentInChildren<ParticleSystem>();
        keyboard = Keyboard.current;
        if (bubbleParticles == null)
        {
            Debug.LogError("No Particle System found in child objects!");
        }
    }

    void Update()
    {

        if (keyboard.aKey.isPressed)
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
