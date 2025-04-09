using UnityEngine;

public class ParticleAutoDestroyer : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private ParticleSystem particle;

    private void Awkae()
    {
        particle = GetComponent<ParticleSystem>();
    }

    private void Update()
    {
        if (particle.isPlaying == true)
        {
            Destroy(gameObject);
        }
    }
}
