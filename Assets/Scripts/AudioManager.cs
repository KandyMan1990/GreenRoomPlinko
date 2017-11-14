using System.Collections;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }
    [SerializeField]
    AudioSource sfxSource;
    [SerializeField]
    AudioSource musicSource;
    [SerializeField]
    AudioSource voiceSource;

    [SerializeField]
    AudioClip playerTransition;
    [SerializeField]
    AudioClip spawnPlayers;
    [SerializeField]
    AudioClip winner;
    [SerializeField]
    AudioClip[] collisions;
    [SerializeField]
    AudioClip[] instantWins;
    [SerializeField]
    [Range(0f, 1f)]
    float transitionVolume = 0.7f;
    [SerializeField]
    [Range(0f, 1f)]
    float spawnVolume = 0.8f;
    [SerializeField]
    [Range(0f, 1f)]
    float collisionVolume = 0.2f;
    [SerializeField]
    [Range(0f, 1f)]
    float winnerVolume = 0.5f;
    [SerializeField]
    [Range(0f, 1f)]
    float instantWinVolume = 0.5f;

    void Awake()
    {
        Instance = this;
    }

    public void PlayPlayerTransition()
    {
        StartCoroutine(PlayClip(playerTransition, transitionVolume, sfxSource));
    }

    IEnumerator PlayClip(AudioClip clip, float volume, AudioSource s)
    {
        s.clip = clip;
        s.volume = volume;
        s.Play();

        while (s.isPlaying)
        {
            yield return null;
        }

        s.clip = null;
        s.volume = 1f;
    }

    public void PlayPlayerSpawn()
    {
        StartCoroutine(PlayClip(spawnPlayers, spawnVolume, sfxSource));
    }

    public void PlayCollision()
    {
        if (sfxSource.clip == playerTransition && sfxSource.isPlaying)
            return;

        AudioClip clip = collisions[Random.Range(0, collisions.Length)];
        sfxSource.PlayOneShot(clip, collisionVolume);
    }

    public void PlayWinner()
    {
        musicSource.PlayOneShot(winner, winnerVolume);
    }

    public void PlayInstantWin()
    {
        AudioClip clip = instantWins[Random.Range(0, instantWins.Length)];
        voiceSource.PlayOneShot(clip, instantWinVolume);
    }

    public float GetWinnerLength
    {
        get { return winner.length; }
    }
}