using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;

public class AudioMixerManager : MonoBehaviour
{
    public static AudioMixerManager manager { private set; get; }

    public AudioMixer audioMixer;
    [SerializeField] private string volumeParameter = "Volume";

    private AudioSource audioSource;
    
    void Awake()
    {
        if (manager == null)
            manager = this;
        else
            Destroy(this.gameObject);
    }

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void SetVolume(float v)
    {
        audioMixer.SetFloat("Volume", v);
    }

    public void SetAudioSource(AudioSource a)
    {
        audioSource = a;
    }
    public void PlaySound(AudioClip clip)
    {
        audioSource.clip = clip;
        audioSource.Play();
    }
}