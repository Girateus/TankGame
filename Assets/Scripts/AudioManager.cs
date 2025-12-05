using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] AudioSource _Music;
    [SerializeField] AudioSource _SoundEffects;
    
    public AudioClip BackgroundMusic;
    public AudioClip TankShoot;
    public AudioClip TurretsShoot;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _Music.clip = BackgroundMusic;
        _Music.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlaySound(AudioClip clip)
    {
        _SoundEffects.PlayOneShot(clip);
    }
}
