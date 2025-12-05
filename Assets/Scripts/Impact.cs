using Unity.VisualScripting;
using UnityEngine;

public class Impact : MonoBehaviour
{
    [SerializeField] private ParticleSystem _particlesSystem;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (_particlesSystem.totalTime > _particlesSystem.main.duration)
        {
            Destroy(gameObject);
        }
        
    }
}
