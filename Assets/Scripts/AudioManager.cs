using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

[RequireComponent(typeof(AudioSource))]
public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }
    AudioSource source;

    [SerializeField]
    AudioClip playerTransition;
    [SerializeField]
    AudioClip spawnPlayers;
    [SerializeField]
    AudioClip[] collisions;
    [SerializeField]
    [Range(0f, 1f)]
    float transitionVolume = 0.7f;
    [SerializeField]
    [Range(0f, 1f)]
    float spawnVolume = 0.8f;
    [SerializeField]
    [Range(0f, 1f)]
    float collisionVolume = 0.2f;

    void Awake()
    {
        Instance = this;
        source = GetComponent<AudioSource>();
    }

    public void PlayPlayerTransition()
    {
        StartCoroutine(PlayClip(playerTransition, transitionVolume));
    }

    IEnumerator PlayClip(AudioClip clip, float volume)
    {
        source.clip = clip;
        source.volume = volume;
        source.Play();

        while (source.isPlaying)
        {
            yield return null;
        }

        source.clip = null;
        source.volume = 1f;
    }

    public void PlayPlayerSpawn()
    {
        StartCoroutine(PlayClip(spawnPlayers, spawnVolume));
    }

    public void PlayCollision()
    {
        if (source.clip == playerTransition && source.isPlaying)
            return;

        AudioClip clip = collisions[Random.Range(0, collisions.Length)];
        source.PlayOneShot(clip, collisionVolume);
    }
}