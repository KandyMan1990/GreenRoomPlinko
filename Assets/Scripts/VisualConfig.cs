using UnityEngine;

[CreateAssetMenu(fileName = "Visual Config", menuName = "Visual Config")]
public class VisualConfig : ScriptableObject
{
    [SerializeField]
    Sprite ballSprite;
    [SerializeField]
    Sprite background;
    [SerializeField]
    AudioClip backgroundMusic;
    [SerializeField]
    GameObject objectToRandomlySpawn;
    [SerializeField]
    AudioClip victoryMusic;
    [SerializeField]
    AudioClip[] playerCollideSfx;

    public Sprite BallSprite
    {
        get { return ballSprite; }
    }

    public Sprite Background
    {
        get { return background; }
    }

    public AudioClip BackgroundMusic
    {
        get { return backgroundMusic; }
    }

    public GameObject ObjectToRandomlySpawn
    {
        get { return objectToRandomlySpawn; }
    }

    public AudioClip VictoryMusic
    {
        get { return victoryMusic; }
    }

    public AudioClip[] PlayerCollideSfx
    {
        get { return playerCollideSfx; }
    }
}