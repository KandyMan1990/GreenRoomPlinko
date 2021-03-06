﻿using UnityEngine;
using UnityEngine.UI;

public class PlayerGameobject : MonoBehaviour
{
    Text uiText;
    Rigidbody2D rb;
    bool toldGameManager;
    PlayerData pd;
    float timeInTrigger = 0f;
    Color col;

    void Awake()
    {
        uiText = GetComponentInChildren<Text>();
        rb = GetComponent<Rigidbody2D>();

        col = ColourPool.Instance.GetColour;

        GetComponent<SpriteRenderer>().color = col;

        GradientColorKey gradientColorKey = new GradientColorKey(col, 0f);
        GradientColorKey[] colourKeys = new GradientColorKey[2];
        colourKeys[0] = gradientColorKey;
        colourKeys[1] = gradientColorKey;

        GradientAlphaKey alphaKey0 = new GradientAlphaKey(1f, 0f);
        GradientAlphaKey alphaKey1 = new GradientAlphaKey(0f, 1f);
        GradientAlphaKey[] alphaKeys = new GradientAlphaKey[2];
        alphaKeys[0] = alphaKey0;
        alphaKeys[1] = alphaKey1;

        Gradient gradient = new Gradient();
        gradient.SetKeys(colourKeys, alphaKeys);

        GetComponent<TrailRenderer>().colorGradient = gradient;
    }

    public void SetName(string n)
    {
        uiText.text = n;
    }

    public void SetPlayerData(PlayerData playerData)
    {
        pd = playerData;
    }

    public PlayerData GetPlayerData
    {
        get { return pd; }
    }

    public Color GetColour
    {
        get { return col; }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("LandingZone"))
        {
            timeInTrigger = 0f;
        }
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("LandingZone"))
        {
            timeInTrigger += Time.deltaTime;

            if (timeInTrigger > 3f && rb.velocity != Vector2.zero)
            {
                rb.velocity = Vector2.zero;
            }

            if (rb.velocity == Vector2.zero && !toldGameManager)
            {
                toldGameManager = true;
                LandingZone ld = collision.gameObject.GetComponent<LandingZone>();
                bool winZone = ld.WinZone;
                bool instantWinZone = ld.InstantWinZone;
                GameManager.Instance.PlayerFinished(new FinishedPlayer(this, transform, winZone, instantWinZone));
            }
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall") || 
            collision.gameObject.CompareTag("Obsticle") || 
            collision.gameObject.CompareTag("Player") || 
            collision.gameObject.CompareTag("Floor"))
        {
            AudioManager.Instance.PlayCollision();
        }
    }

    public void ResetObj()
    {
        toldGameManager = false;
        timeInTrigger = 0f;
    }
}